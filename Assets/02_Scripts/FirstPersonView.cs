using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class FirstPersonView : MonoBehaviour
{
    [Header("Movement Setting")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("Camera Setting")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float rotateSensitivity;

    [Header("Ground Check")]
    [SerializeField] private bool useGroundCheck;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private LayerMask groundCheckLayer;
    [SerializeField] private float groundCheckRadious;

    private Transform camTransform;
    private Rigidbody rb;


    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        camTransform = playerCamera.transform;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (CheckGround() && Input.GetKeyDown(KeyCode.Space))
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        float mouseX = Input.GetAxis("Mouse X") * rotateSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * rotateSensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -80.0f, 80.0f);

        camTransform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }

    private void FixedUpdate()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        if (v == 0 && h == 0)
            return;

        Vector3 camForward = camTransform.forward;
        Vector3 camRight = camTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        Vector3 moveDir = camForward * v + camRight * h;

        transform.forward = camForward;
        rb.MovePosition(transform.position + Time.fixedDeltaTime * moveSpeed * moveDir.normalized);
    }

    private bool CheckGround()
    {
        if (!useGroundCheck)
            return true;

        return Physics.CheckSphere(groundCheckPoint.position, groundCheckRadious, groundCheckLayer);
    }
}
