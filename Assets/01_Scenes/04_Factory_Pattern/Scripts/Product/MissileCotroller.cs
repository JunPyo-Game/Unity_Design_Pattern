using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Missile : MonoBehaviour, IProduct, IPool<Missile>
{
    [SerializeField] private float moveSpeed = 8.0f;

    private Rigidbody rb;

    public ObjectPool<Missile> Pool { get; set; }
    public bool IsReleased { get; set; } = false;
    public string Name { get => gameObject.name; set => gameObject.name = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        GetComponent<Collider>().isTrigger = true;
    }

    public void Init() { }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + Time.fixedDeltaTime * moveSpeed * Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boundary") ||
            other.gameObject.CompareTag("Enemy"))
        {
            Pool.TryRelease(this);
        }
    }
}
