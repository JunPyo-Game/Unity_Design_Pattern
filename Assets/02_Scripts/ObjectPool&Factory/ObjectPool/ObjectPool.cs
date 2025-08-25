using System;
using System.Collections.Generic;

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
    /// <param name="collectionCheck">중복 반환 검사 여부(기본값 true)</param>
    /// <param name="defaultCapacity">초기 풀 크기(기본값 10)</param>
    /// <param name="maxSize">최대 풀 크기(기본값 100)</param>
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
    /// ObjectPool에 저장된 전체 객체 수(활성+비활성).
    /// </summary>
    public int CountAll { get; private set; }
    /// <summary>
    /// 현재 풀에 남아있는(비활성) 객체 수.
    /// </summary>
    public int CountInactive => elements.Count;
    /// <summary>
    /// 현재 사용 중(활성) 객체 수.
    /// </summary>
    public int CountActive => CountAll - CountInactive;

    /// <summary>
    /// 풀에서 객체를 하나 가져옵니다. 남은 객체가 없으면 새로 생성합니다.
    /// </summary>
    /// <returns>풀에서 꺼낸 객체</returns>
    /// <exception cref="Exception">createFunc 또는 onGet에서 예외가 발생할 수 있습니다.</exception>
    public T Get()
    {
        T el;

        if (CountInactive == 0)
        {
            el = createFunc();
            CountAll++;
        }
        else
        {
            el = elements.Pop();
        }

        onGet?.Invoke(el);
        return el;
    }

    /// <summary>
    /// 객체 반환을 시도합니다. 실패 사유는 반환값으로 알 수 있습니다.
    /// </summary>
    /// <param name="element">반환할 객체</param>
    /// <returns>성공 여부</returns>
    public void Release(T element)
    {
        if (collectionCheck && CountInactive != 0)
        {
            foreach (T el in elements)
            {
                if (el == element)
                    throw new InvalidOperationException($"[ObjectPool] Duplicate release attempt: The object ({element}) is already in the pool and cannot be released again.");
            }
        }

        onRelease?.Invoke(element);

        if (CountInactive < maxSize)
        {
            elements.Push(element);
        }

        CountAll--;
        onDestroy?.Invoke(element);
    }

    /// <summary>
    /// 풀에 남은 모든 객체를 정리하고 비웁니다.
    /// </summary>
    /// <remarks>onDestroy 콜백 또는 IDisposable.Dispose()가 호출됩니다.</remarks>
    public void Clear()
    {
        if (onDestroy is not null)
        {
            foreach (T el in elements)
                onDestroy.Invoke(el);
        }

        CountAll = 0;
        elements.Clear();
    }

    /// <summary>
    /// 해당 객체가 현재 풀에 포함되어 있는지 확인합니다.
    /// </summary>
    /// <param name="element">확인할 객체</param>
    /// <returns>포함 여부</returns>
    public bool HasElement(T element)
    {
        return elements.Contains(element);
    }

    /// <summary>
    /// 풀의 모든 객체를 정리합니다. (IDisposable 구현 시 Dispose 패턴 지원)
    /// </summary>
    public void Dispose()
    {
        Clear();
    }
}