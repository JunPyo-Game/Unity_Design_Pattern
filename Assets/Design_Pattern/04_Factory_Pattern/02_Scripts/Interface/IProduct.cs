/// <summary>
/// 팩토리(Factory) 패턴에서 생성되는 객체가 반드시 구현해야 하는 인터페이스입니다.
/// <para>객체의 이름(식별자)과 초기화(Init) 등 생성/초기화 관련 기능을 정의합니다.</para>
/// <summary>
/// 팩토리(Factory) 패턴에서 생성되는 객체가 반드시 구현해야 하는 인터페이스입니다.
/// Name 프로퍼티로 객체의 식별자(이름)를 제공하고,
/// Init 메서드로 객체의 상태를 초기화합니다.
/// </summary>
public interface IProduct
{
    /// <summary>
    /// 객체의 이름(식별자). 팩토리/게임 내에서 오브젝트를 구분하는 데 사용됩니다.
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// 객체의 상태를 초기화하는 메서드. 팩토리에서 생성 또는 풀에서 꺼낼 때 호출됩니다.
    /// </summary>
    void Init();
}
