using UnityEngine;

/// <summary>
/// Bullet 객체를 생성하고 풀링하는 BulletFactory.
/// PooledFactory<Bullet>를 상속하며, ScriptableObject로 사용됩니다.
/// </summary>
[CreateAssetMenu(fileName = "BulletFactory", menuName = "BulletFactory", order = 0)]
public class BulletFactory : PooledFactory<Bullet>
{
    public override FactoryType Type => FactoryType.Bullet;
}
