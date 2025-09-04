using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnityChanQuarterViewController : MonoBehaviour
{
    [Header("Movement Setting")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float rotateSpeed = 360.0f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject outline;

    [Header("Camera Setting")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Vector3 offset;

    private Rigidbody rb;
    private Vector3 destination;
    private Vector3 moveDir;
    private Quaternion rotateDir;

    public float Velocity => moveDir.sqrMagnitude == 0 ? 0 : moveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rotateDir = Quaternion.LookRotation(transform.forward);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100.0f, groundLayer))
            {
                destination = hitInfo.point;

                moveDir = (destination - transform.position).normalized;
                rotateDir = Quaternion.LookRotation(moveDir);
            }
        }
    }

    private void FixedUpdate()
    {
        if (moveDir != Vector3.zero)
        {
            Vector3 move = moveSpeed * Time.fixedDeltaTime * moveDir;
            rb.MovePosition(transform.position + move);

            if ((transform.position - destination).sqrMagnitude < 0.01f)
                moveDir = Vector3.zero;
        }

        if (Quaternion.Angle(transform.rotation, rotateDir) > 0.1f)
        {
            Quaternion rot = Quaternion.RotateTowards(transform.rotation, rotateDir, Time.fixedDeltaTime * rotateSpeed);
            rb.MoveRotation(rot);
        }
    }

    private void LateUpdate()
    {
        playerCamera.transform.position = transform.position + offset;
        playerCamera.transform.LookAt(transform);
    }
}
