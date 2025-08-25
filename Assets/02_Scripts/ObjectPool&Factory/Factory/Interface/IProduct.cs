using UnityEngine;

/// <summary>
/// 팩토리에서 생성되는 객체가 반드시 구현해야 하는 인터페이스입니다.
/// </summary>
public interface IProduct
{
    /// <summary>
    /// 객체의 이름(식별자)
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 객체 초기화 메서드
    /// </summary>
    public void Init();
}
