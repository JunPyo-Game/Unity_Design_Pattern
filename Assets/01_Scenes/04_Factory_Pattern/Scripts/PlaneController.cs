using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlaneController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private Transform firePos;

    private Factory missileFactory;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Start()
    {
        missileFactory = FactoryManager.Instance[FactoryType.Missile];    
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            missileFactory.GetProduct(firePos.position);
        }
    }

    private void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Vector3 moveDir = new Vector3(h, v, 0).normalized;
        rb.MovePosition(transform.position + Time.fixedDeltaTime * moveSpeed * moveDir);
    }
}
