using UnityEngine;

[CreateAssetMenu(fileName = "EnemyFactory", menuName = "EnemyFactory", order = 0)]
public class EnemyFactory : ScriptableObject, IFactory
{
    private ObjectPool<Enemy> enemyPool;
    [SerializeField] private Enemy enemyPrefab;

    public IProduct GetProduct(Vector3 position)
    {
        enemyPool ??= new(
            createFunc: Create,
            onGet: OnGet,
            onRelease: OnRelease
        );

        Enemy enemy = enemyPool.Get();
        enemy.transform.position = position;
        enemy.Init();

        return enemy;
    }

    private Enemy Create()
    {
        Enemy enemy = Instantiate(enemyPrefab);
        enemy.Pool = enemyPool;
        enemy.gameObject.SetActive(false);

        return enemy;
    }

    private void OnGet(Enemy bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnRelease(Enemy bullet)
    {
        bullet.gameObject.SetActive(false);
    }

}
