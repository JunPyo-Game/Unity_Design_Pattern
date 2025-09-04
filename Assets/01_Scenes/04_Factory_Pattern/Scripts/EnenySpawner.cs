using System.Collections;
using UnityEngine;

public class EnenySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 1.0f;
    [SerializeField] private int maxSpawn = 4;

    [SerializeField] private Transform player;

    private EnemyFactory enemyFactory;
    private  int[] indices;

    private void Start()
    {
        enemyFactory = FactoryManager.Instance[FactoryType.Enemy] as EnemyFactory;

        indices = new int[spawnPoints.Length];
        for (int i = 0; i < indices.Length; i++) indices[i] = i;

        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            int spawnCount = Random.Range(1, maxSpawn + 1);
           
            for (int i = indices.Length - 1; i > indices.Length - spawnCount; i--)
            {
                int j = Random.Range(0, i + 1);
                (indices[j], indices[i]) = (indices[i], indices[j]);
            }

            for (int i = 0; i < spawnCount; i++)
            {
                int idx = indices[i];
                enemyFactory.GetProduct(spawnPoints[idx].position, player);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
