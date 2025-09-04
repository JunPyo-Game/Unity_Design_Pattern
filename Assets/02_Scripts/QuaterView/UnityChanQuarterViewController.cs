using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnityChanQuarterViewController : MonoBehaviour
{
    [Header("Movement Setting")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float rotateSpeed = 360.0f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Camera Setting")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Vector3 offset;

    private Rigidbody rb;
    private Vector3 destination;
    private Vector3 moveDir;
    private float rotateDir;

    public float Velocity => moveDir.sqrMagnitude == 0 ? 0 : moveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hitInfo, groundLayer);

            destination = hitInfo.point;
            moveDir = (destination - transform.position).normalized;
            rotateDir = Vector3.Cross(transform.forward, moveDir).y;
        }
    }

    private void FixedUpdate()
    {
        if (moveDir.sqrMagnitude != 0)
        {
            Vector3 move = moveSpeed * Time.fixedDeltaTime * moveDir;
            rb.MovePosition(transform.position + move);

            if ((transform.position - destination).sqrMagnitude < 0.01f)
                moveDir = Vector3.zero;
        }

        if (rotateDir != 0)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            float angle = Quaternion.Angle(transform.rotation, targetRot);

            Quaternion rot = Quaternion.RotateTowards(transform.rotation, targetRot, Time.fixedDeltaTime * rotateSpeed);
            rb.MoveRotation(rot);

            if (angle < 0.5f)
                rotateDir = 0;
        }
    }

    private void LateUpdate()
    {
        playerCamera.transform.position = transform.position + offset;
        playerCamera.transform.LookAt(transform); 
    }
}
