using UnityEngine;

/// <summary>
/// 팩토리 패턴에서 객체를 생성하는 팩토리 인터페이스입니다.
/// </summary>
public interface IFactory
{
    /// <summary>
    /// 지정한 위치에 객체를 생성하여 반환합니다.
    /// </summary>
    /// <param name="position">생성 위치</param>
    /// <returns>생성된 객체(IProduct)</returns>
    public IProduct GetProduct(Vector3 position);
}