using UnityEngine;

/// <summary>
/// Missile 객체를 생성하고 풀링하는 MissileFactory.
/// PooledFactory<Missile>를 상속하며, ScriptableObject로 사용됩니다.
/// </summary>
[CreateAssetMenu(fileName = "MissileFactory", menuName = "MissileFactory", order = 0)]
public class MissileFactory : PooledFactory<Missile>
{
    public override FactoryType Type => FactoryType.Missile;
}
