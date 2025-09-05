using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class BulletController : MonoBehaviour, IPool<BulletController>
{
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private TrailRenderer trailRenderer;
    private Rigidbody rb;

    public ObjectPool<BulletController> Pool { get; set ; }
    public bool IsReleased { get; set; } = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 0.1f;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.localPosition + moveSpeed * Time.fixedDeltaTime * transform.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsReleased)
            return;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Pool.Release(this);
        trailRenderer.Clear();
    }
}
