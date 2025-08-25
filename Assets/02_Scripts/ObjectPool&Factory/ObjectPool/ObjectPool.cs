using System;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// 제네릭 오브젝트 풀 클래스.
/// 객체의 재사용을 통해 생성/파괴 비용을 줄이고, 중복 반환 방지 및 안전한 풀링을 지원합니다.
/// IPool<T> 인터페이스를 구현한 타입만 사용할 수 있습니다.
/// </summary>
public class ObjectPool<T> where T : class, IPool<T>
{
    private readonly Stack<T> elements;
    private readonly Func<T> createFunc;
    private readonly Action<T> onGet;
    private readonly Action<T> onRelease;
    private readonly Action<T> onDestroy;
    private readonly int maxSize;

    /// <summary>
    /// ObjectPool을 생성합니다.
    /// </summary>
    /// <param name="createFunc">객체 생성 델리게이트(필수)</param>
    /// <param name="onGet">객체 할당 시 호출되는 콜백(선택)</param>
    /// <param name="onRelease">객체 반환 시 호출되는 콜백(선택)</param>
    /// <param name="onDestroy">풀에서 파기될 때 호출되는 콜백(선택)</param>
    /// <param name="defaultCapacity">초기 풀 크기(기본값 10)</param>
    /// <param name="maxSize">최대 풀 크기(기본값 100, 초과 시 반환 객체는 파기)</param>
    /// <exception cref="ArgumentNullException">createFunc가 null인 경우</exception>
    /// <exception cref="ArgumentOutOfRangeException">defaultCapacity 또는 maxSize가 음수인 경우</exception>
    public ObjectPool(
        Func<T> createFunc,
        Action<T> onGet = null,
        Action<T> onRelease = null,
        Action<T> onDestroy = null,
        int defaultCapacity = 10,
        int maxSize = 100)
    {
        this.createFunc = createFunc;
        this.onGet = onGet;
        this.onRelease = onRelease;
        this.onDestroy = onDestroy;
        this.maxSize = defaultCapacity > maxSize ? defaultCapacity : maxSize;
        this.elements = new Stack<T>(defaultCapacity);
    }

    /// <summary>
    /// ObjectPool이 관리하는 전체 객체 수(활성+비활성).
    /// </summary>
    public int CountAll { get; private set; }
    /// <summary>
    /// 현재 풀에 남아있는(비활성) 객체 수. (스택에 쌓여있는 개수)
    /// </summary>
    public int CountInactive => elements.Count;
    /// <summary>
    /// 현재 사용 중(활성) 객체 수. (전체 - 비활성)
    /// </summary>
    public int CountActive => CountAll - CountInactive;

    /// <summary>
    /// 풀에서 객체를 하나 가져옵니다. 남은 객체가 없으면 새로 생성합니다.
    /// </summary>
    /// <returns>풀에서 꺼낸 객체</returns>
    /// <exception cref="Exception">createFunc 또는 onGet에서 예외가 발생할 수 있습니다.</exception>
    /// <remarks>
    /// 풀에서 꺼낸 객체는 반드시 IsReleased=true 상태여야 하며, 할당 후 IsReleased=false로 전환됩니다.
    /// </remarks>
    public T Get()
    {
        // 비활성 객체가 없으면 새로 생성
        T el;
        if (CountInactive == 0)
        {
            el = createFunc();
            el.Pool = this;
            CountAll++;
        }
        else
        {
            el = elements.Pop();
        }

        // 풀에서 꺼낸 객체는 반드시 반환된 상태여야 함
        Debug.Assert(el.IsReleased);

        // 할당: 사용 중 상태로 전환
        el.IsReleased = false;
        onGet?.Invoke(el);
        return el;
    }

    
    /// <summary>
    /// 객체를 풀에 반환합니다. 반환 시 유효성 검사 및 중복 반환 검사를 수행하며, 잘못된 반환 시 예외를 던집니다.
    /// </summary>
    /// <param name="element">반환할 객체</param>
    /// <exception cref="InvalidOperationException">다른 풀에 속한 객체이거나, 이미 반환된 객체인 경우</exception>
    public void Release(T element)
    {
        switch (ValidateRelease(element))
        {
            case ReleaseValidationResult.NotFromThisPool:
                throw new InvalidOperationException($"[ObjectPool] Invalid release attempt: The object ({element}) does not belong to this pool.");

            case ReleaseValidationResult.AlreadyReleased:
                throw new InvalidOperationException($"[ObjectPool] Duplicate release attempt: The object ({element}) is already in the pool and cannot be released again.");
        }

        InternalRelease(element);
    }

    /// <summary>
    /// 객체를 풀에 반환합니다. 반환 시 유효성 검사 및 중복 반환 검사를 수행하며, 예외를 던지지 않고 성공 여부를 반환합니다.
    /// </summary>
    /// <param name="element">반환할 객체</param>
    /// <returns>정상 반환 시 true, 잘못된 반환(중복/풀 불일치)이면 false</returns>
    public bool TryRelease(T element)
    {
        if (ValidateRelease(element) != ReleaseValidationResult.Valid)
            return false;

        InternalRelease(element);
        return true;
    }

    /// <summary>
    /// 반환 유효성 검사 결과를 나타내는 열거형입니다.
    /// </summary>
    private enum ReleaseValidationResult { Valid, NotFromThisPool, AlreadyReleased }

    /// <summary>
    /// 반환 유효성 검사를 수행합니다. 예외를 던지지 않고 상태만 반환합니다.
    /// </summary>
    /// <param name="element">반환할 객체</param>
    /// <returns>유효성 검사 결과</returns>
    private ReleaseValidationResult ValidateRelease(T element)
    {
        if (element.Pool != this)
            return ReleaseValidationResult.NotFromThisPool;

        if (element.IsReleased)
            return ReleaseValidationResult.AlreadyReleased;

        return ReleaseValidationResult.Valid;
    }

    /// <summary>
    /// 실제 반환 로직을 수행하는 내부 메서드입니다.
    /// </summary>
    /// <param name="element">반환할 객체</param>
    private void InternalRelease(T element)
    {
        onRelease?.Invoke(element);
        if (CountInactive < maxSize)
        {
            elements.Push(element);
        }
        else
        {
            CountAll--;
            onDestroy?.Invoke(element);
        }
        element.IsReleased = true;
    }

    /// <summary>
    /// 풀에 남아있는 모든 객체를 정리하고 비웁니다. (onDestroy 콜백이 있으면 각 객체마다 호출)
    /// </summary>
    public void Clear()
    {
        if (onDestroy != null)
        {
            foreach (T el in elements)
            {
                onDestroy.Invoke(el);
            }
        }
        CountAll = 0;
        elements.Clear();
    }

    /// <summary>
    /// 전달한 객체가 현재 풀(비활성 스택)에 포함되어 있는지 확인합니다.
    /// </summary>
    /// <param name="element">확인할 객체</param>
    /// <returns>풀에 포함되어 있으면 true, 아니면 false</returns>
    public bool HasElement(T element)
    {
        return elements.Contains(element);
    }
}