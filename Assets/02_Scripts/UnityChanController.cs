
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class UnityChanController : MonoBehaviour
{
    [Header("Movement Setting")]
    [SerializeField] private float walkSpeed = 2.0f;
    [SerializeField] private float runSpeed = 5.0f;
    [SerializeField] private float jumpForce;


    [Header("Camera Settings")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float rotateSensitivity;
    [SerializeField] private bool alwaysRotateCamera;


    [Header("Ground Check")]
    [SerializeField] private bool useGroundCheck;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private LayerMask groundCheckLayer;
    [SerializeField] private float groundCheckRadious;

    private Rigidbody rb;
    private Animator anim;
    private Transform camTransform;

    private float moveSpeed;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public bool IsMoving { get; set; } = true;
    public bool IsSliding { get; set; } = false;

    public float Velocity { get; set; } = 0.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        camTransform = playerCamera.transform;
        moveSpeed = walkSpeed;
    }

    public void Start()
    {
        camTransform.position = transform.position + offset;
        camTransform.LookAt(transform);
    }

    private void LateUpdate()
    {
        if (alwaysRotateCamera || Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotateSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * rotateSensitivity;

            yaw += mouseX;
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -80.0f, 80.0f);
        }

        camTransform.position = transform.position + Quaternion.Euler(pitch, yaw, 0) * offset;
        camTransform.LookAt(transform);
    }

    private void FixedUpdate()
    {
        if (!IsMoving)
            return;

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        if (v == 0 && h == 0)
        {
            Velocity = 0;

            return;
        }

        // 카메라의 y축만 추출
        Vector3 camForward = camTransform.forward;
        Vector3 camRight = camTransform.right;
        camForward.y = 0;
        camRight.y = 0;

        // 이동 방향 (카메라 기준)
        float speed = IsSliding ? moveSpeed * 2 : moveSpeed;
        Vector3 moveDir = (camForward.normalized * v + camRight.normalized * h).normalized;

        rb.MovePosition(transform.position + Time.fixedDeltaTime * speed * moveDir);
        transform.forward = moveDir;

        Velocity = moveDir.sqrMagnitude * moveSpeed;
    }

    public void JumpImpulse()
    {
        rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
    }

    public void ToggleRun()
    {
        moveSpeed = moveSpeed == walkSpeed ? runSpeed : walkSpeed;
    }

    public bool CheckGround()
    {
        if (!useGroundCheck)
            return true;

        return Physics.CheckSphere(groundCheckPoint.position, groundCheckRadious, groundCheckLayer);
    }
}
