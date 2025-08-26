using UnityEngine;

/// <summary>
/// Bullet 객체를 생성하고 풀링하는 팩토리 ScriptableObject입니다.
/// </summary>
[CreateAssetMenu(fileName = "BulletFactory", menuName = "BulletFactory", order = 0)]
public class BulletFactory : PooledFactory<Bullet>
{
    public override FactoryType Type => FactoryType.Bullet;
}
