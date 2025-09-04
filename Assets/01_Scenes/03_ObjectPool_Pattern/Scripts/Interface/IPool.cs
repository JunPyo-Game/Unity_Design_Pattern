/// <summary>
/// 오브젝트 풀링(ObjectPool) 시스템에서 풀에 의해 관리되는 객체가 반드시 구현해야 하는 인터페이스입니다.
/// <para>객체가 자신이 속한 풀(ObjectPool<T>)의 참조와 반환 상태(IsReleased)를 관리합니다.</para>
/// </summary>
public interface IPool<T> where T : class, IPool<T>
{
    /// <summary>
    /// 객체가 속한 풀의 참조. 올바른 풀 반환 및 안전성 검증에 사용됩니다.
    /// </summary>
    public ObjectPool<T> Pool { get; set; }
    /// <summary>
    /// 객체가 현재 풀에 반환된 상태인지 여부. 중복 반환 방지에 사용됩니다.
    /// </summary>
    public bool IsReleased { get; set; }
}