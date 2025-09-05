using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class BulletController : MonoBehaviour, IPool<BulletController>
{
    [SerializeField] private float moveSpeed = 10.0f;

    private TrailRenderer trailRenderer;
    private Rigidbody rb;

    public ObjectPool<BulletController> Pool { get; set; }
    public bool IsReleased { get; set; } = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.localPosition + moveSpeed * Time.fixedDeltaTime * transform.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsReleased)
            return;

        Pool.Release(this);
    }

    public void Reset()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        trailRenderer.Clear();
    }
}
