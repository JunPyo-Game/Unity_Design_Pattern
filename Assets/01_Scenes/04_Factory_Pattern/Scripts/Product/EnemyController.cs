using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour, IProduct, IPool<Enemy>
{
    [SerializeField] private float moveSpeed = 3.0f;

    private Rigidbody rb;
    private Vector3 moveDir;

    public ObjectPool<Enemy> Pool { get; set; }
    public bool IsReleased { get; set; }
    public Transform Player { get; set; }
    public string Name { get => gameObject.name; set => gameObject.name = value; }

    public void Init()
    {
        rb.linearVelocity = Vector3.zero;

        float rand = Random.Range(0.0f, 10.0f);

        if (rand <= 3.0f)
        {
            moveDir = (Player.position - transform.position).normalized;
        }
        else
        {
            moveDir = new Vector3(0, -1, 0);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        GetComponent<Collider>().isTrigger = true;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + Time.fixedDeltaTime * moveSpeed * moveDir);
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsReleased)
            return;

        if (other.gameObject.CompareTag("Boundary") || other.gameObject.CompareTag("Bullet"))
        {
            FactoryManager.Instance[FactoryType.Explosion].GetProduct(other.transform.position);
            Pool.TryRelease(this);
        }
    }
}

