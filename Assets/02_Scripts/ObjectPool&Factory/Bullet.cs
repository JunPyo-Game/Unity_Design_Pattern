using UnityEngine;

public class Bullet : MonoBehaviour, IProduct, IPool<Bullet>
{
    int enemyCount = 0;
    int wallCount = 0;
    [SerializeField] private float moveSpeed = 8.0f;
    public ObjectPool<Bullet> Pool { get; set; }
    public bool IsReleased { get; set; } = false;
    public string Name { get => gameObject.name; set => gameObject.name = value; }

    public void Init() { }

    private void Update()
    {
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall") ||
            other.gameObject.CompareTag("Enemy"))
        {
            Pool.TryRelease(this);
        }
    }
}
