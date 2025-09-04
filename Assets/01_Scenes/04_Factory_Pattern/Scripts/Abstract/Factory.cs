using UnityEngine;

/// <summary>
/// 팩토리 타입을 구분하는 enum입니다.
/// Bullet, Enemy 등 각 팩토리의 고유 타입을 정의합니다.
/// </summary>
public enum FactoryType
{
    Missile,
    Enemy,
    Explosion
}

/// <summary>
/// 팩토리 패턴의 추상 기반 클래스입니다.
/// ScriptableObject로 구현되며, 구체 팩토리는 이 클래스를 상속받아 객체 생성 및 타입 정보를 제공합니다.
/// </summary>
public abstract class Factory : ScriptableObject
{
    /// <summary>
    /// 지정한 위치에 객체를 생성하여 반환합니다.
    /// </summary>
    /// <param name="position">생성 위치</param>
    /// <returns>생성된 객체(IProduct)</returns>
    public abstract IProduct GetProduct(Vector3 position);

    /// <summary>
    /// 팩토리의 고유 타입(enum) 정보를 반환합니다.
    /// </summary>
    public abstract FactoryType Type { get; }
}
