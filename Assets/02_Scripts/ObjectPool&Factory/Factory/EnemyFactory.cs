using UnityEngine;

/// <summary>
/// Enemy 객체를 생성하고 풀링하는 팩토리 ScriptableObject입니다.
/// </summary>
[CreateAssetMenu(fileName = "EnemyFactory", menuName = "EnemyFactory", order = 0)]
public class EnemyFactory : PooledFactory<Enemy>
{
    public override FactoryType Type => FactoryType.Enemy;
}
