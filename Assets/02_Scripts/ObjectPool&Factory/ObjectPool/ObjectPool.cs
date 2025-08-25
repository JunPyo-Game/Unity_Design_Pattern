using System;
using System.Collections.Generic;

/// <summary>
/// 풀에 의해 관리되는 객체가 반드시 구현해야 하는 인터페이스입니다.
/// 객체는 자신이 속한 풀(ObjectPool<T>)의 참조를 저장합니다.
/// </summary>
public interface IPool<T> where T : class, IPool<T>
{
    public ObjectPool<T> Pool { get; set; }
}

public class ObjectPool<T> where T : class, IPool<T>
{
    private readonly Stack<T> elements;
    private readonly Func<T> createFunc;
    private readonly Action<T> onGet;
    private readonly Action<T> onRelease;
    private readonly Action<T> onDestroy;
    private readonly bool collectionCheck;
    private readonly int maxSize;

    /// <summary>
    /// ObjectPool을 생성합니다.
    /// </summary>
    /// <param name="createFunc">객체 생성 델리게이트(필수)</param>
    /// <param name="onGet">객체 할당 시 호출되는 콜백(선택)</param>
    /// <param name="onRelease">객체 반환 시 호출되는 콜백(선택)</param>
    /// <param name="onDestroy">풀에서 파기될 때 호출되는 콜백(선택)</param>
    /// <param name="collectionCheck">중복 반환 검사 여부(기본값 true, 중복 반환 시 예외 발생)</param>
    /// <param name="defaultCapacity">초기 풀 크기(기본값 10)</param>
    /// <param name="maxSize">최대 풀 크기(기본값 100, 초과 시 반환 객체는 파기)</param>
    /// <exception cref="ArgumentNullException">createFunc가 null인 경우</exception>
    /// <exception cref="ArgumentOutOfRangeException">defaultCapacity 또는 maxSize가 음수인 경우</exception>
    public ObjectPool(
        Func<T> createFunc,
        Action<T> onGet = null,
        Action<T> onRelease = null,
        Action<T> onDestroy = null,
        bool collectionCheck = true,
        int defaultCapacity = 10,
        int maxSize = 100)
    {
        this.createFunc = createFunc;
        this.onGet = onGet;
        this.onRelease = onRelease;
        this.onDestroy = onDestroy;
        this.collectionCheck = collectionCheck;
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
        // 할당 콜백 호출
        onGet?.Invoke(el);
        return el;
    }

    /// <summary>
    /// 객체를 풀에 반환합니다. 반환 시 유효성 검사 및 중복 반환 검사를 수행합니다.
    /// </summary>
    /// <param name="element">반환할 객체</param>
    /// <exception cref="InvalidOperationException">다른 풀에 속한 객체이거나, 이미 반환된 객체인 경우</exception>
    public void Release(T element)
    {
        // 올바른 풀에 속한 객체인지 확인
        if (element.Pool != this)
            throw new InvalidOperationException($"[ObjectPool] Invalid release attempt: The object ({element}) does not belong to this pool.");

        // 중복 반환 검사 (collectionCheck가 true일 때)
        if (collectionCheck && CountInactive != 0)
        {
            foreach (T el in elements)
            {
                if (el == element)
                    throw new InvalidOperationException($"[ObjectPool] Duplicate release attempt: The object ({element}) is already in the pool and cannot be released again.");
            }
        }

        // 반환 콜백 호출
        onRelease?.Invoke(element);

        // 최대 크기 이하일 때만 스택에 저장, 초과 시 파기
        if (CountInactive < maxSize)
        {
            elements.Push(element);
            return;
        }

        CountAll--;
        onDestroy?.Invoke(element);
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