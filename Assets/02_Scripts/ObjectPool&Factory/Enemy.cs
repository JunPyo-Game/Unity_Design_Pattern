using UnityEngine;

public class Enemy : MonoBehaviour, IProduct, IPool<Enemy>
{
    [SerializeField] private float moveSpeed = 3.0f;
    private Transform player;
    private Vector3 dir;

    public ObjectPool<Enemy> Pool { get; set; }
    public string Name { get => gameObject.name; set => gameObject.name = value; }

    public void Init()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform; 

        float rand = Random.Range(0.0f, 10.0f);

        if (rand <= 3.0f)
        {
            dir = (player.position - transform.position).normalized;
        }
        else
        {
            dir = new Vector3(0, -1, 0);
        }
    }

    private void Update()
    {
        transform.position += Time.deltaTime * moveSpeed * dir;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall") ||
            other.gameObject.CompareTag("Bullet"))
        {
            Pool.Release(this);
        }
    }
}

