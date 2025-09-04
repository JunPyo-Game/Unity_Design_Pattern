using System.Collections;
using UnityEngine;

public class EnenySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 1.0f;
    private EnemyFactory enemyFactory;

    private void Start()
    {
        enemyFactory = FactoryManager.Instance[FactoryType.Enemy] as EnemyFactory;
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            int idx = Random.Range(0, spawnPoints.Length);
            enemyFactory.GetProduct(spawnPoints[idx].position);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
