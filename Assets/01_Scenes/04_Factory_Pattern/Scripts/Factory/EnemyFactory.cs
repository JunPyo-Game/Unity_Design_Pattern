using UnityEngine;

/// <summary>
/// Enemy 객체를 생성하고 풀링하는 EnemyFactory.
/// PooledFactory<Enemy>를 상속하며, ScriptableObject로 사용됩니다.
/// </summary>
[CreateAssetMenu(fileName = "EnemyFactory", menuName = "EnemyFactory", order = 0)]
public class EnemyFactory : PooledFactory<Enemy>
{
    public override FactoryType Type => FactoryType.Enemy;

    public IProduct GetProduct(Vector3 position, Transform player)
    {
        pool ??= new(
            createFunc: Create,
            onGet: OnGet,
            onRelease: OnRelease
        );

        // 풀에서 객체 할당 및 위치/초기화
        Enemy enemy = pool.Get();
        enemy.Player = player;
        enemy.transform.position = position;
        enemy.Init();

        return enemy;
    }
}
