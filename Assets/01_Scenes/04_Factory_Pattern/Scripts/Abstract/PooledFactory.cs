using UnityEngine;

/// <summary>
/// 제네릭 추상 팩토리 클래스. ScriptableObject로 구현되며, 오브젝트 풀링을 통해 객체를 효율적으로 관리합니다.
/// 각 구체 팩토리(BulletFactory, EnemyFactory 등)는 이 클래스를 상속받아 사용합니다.
/// </summary>
/// <typeparam name="T">생성 및 풀링할 객체 타입(MonoBehaviour, IPool<T>, IProduct 구현 필요)</typeparam>
public abstract class PooledFactory<T> 
    : Factory where T : MonoBehaviour, IProduct, IPool<T>
{
    /// <summary>
    /// 생성에 사용할 프리팹(에디터에서 할당)
    /// </summary>
    [SerializeField] protected T prefab;

    /// <summary>
    /// 내부 오브젝트 풀. 최초 사용 시 생성됨.
    /// </summary>
    protected ObjectPool<T> pool;

    /// <summary>
    /// 지정한 위치에 객체를 생성(또는 풀에서 꺼내서) 반환합니다.
    /// </summary>
    /// <param name="position">생성 위치</param>
    /// <returns>생성된 객체(IProduct)</returns>
    public override IProduct GetProduct(Vector3 position)
    {
        // 풀 미생성 시 초기화
        pool ??= new(
            createFunc: Create,
            onGet: OnGet,
            onRelease: OnRelease
        );

        // 풀에서 객체 할당 및 위치/초기화
        T obj = pool.Get();
        obj.transform.position = position;
        obj.Init();
        return obj;
    }

    /// <summary>
    /// 객체를 새로 생성합니다. 풀에 객체가 없을 때 호출됩니다.
    /// </summary>
    /// <returns>비활성화된 새 객체</returns>
    protected virtual T Create()
    {
        T obj = Instantiate(prefab);
        obj.gameObject.SetActive(false);
        return obj;
    }

    /// <summary>
    /// 객체가 풀에서 할당될 때 호출됩니다. (활성화 등)
    /// </summary>
    /// <param name="obj">할당된 객체</param>
    protected virtual void OnGet(T obj) => obj.gameObject.SetActive(true);

    /// <summary>
    /// 객체가 풀에 반환될 때 호출됩니다. (비활성화 등)
    /// </summary>
    /// <param name="obj">반환된 객체</param>
    protected virtual void OnRelease(T obj) => obj.gameObject.SetActive(false);

    /// <summary>
    /// 풀에 남아있는 모든 객체를 정리하고 비웁니다.
    /// </summary>
    public virtual void Clear() => pool?.Clear();
}