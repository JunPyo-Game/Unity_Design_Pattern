/// <summary>
/// 풀에 의해 관리되는 객체가 반드시 구현해야 하는 인터페이스입니다.
/// 객체는 자신이 속한 풀(ObjectPool<T>)의 참조를 저장합니다.
/// </summary>
public interface IPool<T> where T : class, IPool<T>
{
    public ObjectPool<T> Pool { get; set; }
}