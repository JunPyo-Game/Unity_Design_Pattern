using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
public class UnityChanMove : MonoBehaviour
{
    static public readonly int HashVertical = Animator.StringToHash("Vertical");
    static public readonly int HashHorizontal = Animator.StringToHash("Horizontal");
    static public readonly int HashJumpTrigger = Animator.StringToHash("JumpTrigger");
    static public readonly int HashJumpHeight = Animator.StringToHash("JumpHeight");
    static public readonly int HashIsRun = Animator.StringToHash("IsRun");

    private readonly Vector3 slideColCenter = new(0.0f, 0.4f, -0.3f);
    private readonly float slideColHeight = 1.0f;

    private readonly Vector3 umatobiColCenter = new(0.0f, 1.2f, 0.0f);
    private readonly float umatobiColHeight = 1.0f;

    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private float walkSpeed = 1.0f;
    [SerializeField] private float runSpeed = 5.0f;
    [SerializeField] private float rotateSpeed = 120.0f;
    [SerializeField] private float jumpForce = 4.5f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody rb;
    private CapsuleCollider col;
    private Animator animator;

    private Vector3 originColCenter;
    private float origiColHeight;

    private Transform umatobiTarget;

    public bool IsGround { get; private set; }
    public float JumpHeight => animator.GetFloat(HashJumpHeight);
    public float WalkSpeed => walkSpeed;
    public float RunSpeed => runSpeed;
    public float RotateSpeed => rotateSpeed;
    public Transform UmatobiTarget => umatobiTarget;
    public Rigidbody Rigidbody => rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        originColCenter = col.center;
        origiColHeight = col.height;
    }

    public void CheckGround()
    {
        Vector3 spherePos = new(transform.position.x, transform.position.y + JumpHeight, transform.position.z);
        IsGround = Physics.CheckSphere(spherePos, 0.1f, groundLayer);
    }

    public void Move(float v, float speed)
    {
        animator.SetInteger(HashVertical, (int)v);

        if (v != 0)
            transform.localPosition += speed * Time.deltaTime * v * transform.forward;
    }

    public void Rotate(float h, float speed)
    {
        animator.SetInteger(HashHorizontal, (int)h);

        if (h != 0)
            transform.localRotation *= Quaternion.Euler(0, h * speed * Time.deltaTime, 0);
    }

    public void Jump()
    {
        animator.SetTrigger(HashJumpTrigger);
    }

    public void Slide()
    {
        animator.SetTrigger("SlideTrigger");
    }

    public void Umatobi()
    {
        if (umatobiTarget != null)
        {
            animator.SetTrigger("UmatobiTrigger");
        }
    }

    public void SwitchToRun()
    {
        animator.SetBool(HashIsRun, true);
    }

    public void SwitchToWalk()
    {
        animator.SetBool(HashIsRun, false);
    }

    public void ToggleRun()
    {
        bool isRun = animator.GetBool(HashIsRun);
        animator.SetBool(HashIsRun, !isRun);
    }

    public void JumpImpulse()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }

    public void SetSlideCollider()
    {
        col.center = slideColCenter;
        col.height = slideColHeight;
    }

    public void SetUmatobiCollider()
    {
        col.center = umatobiColCenter;
        col.height = umatobiColHeight;
    }

    public void UpdateCollider()
    {
        col.center = new Vector3(0, originColCenter.y + JumpHeight, 0);
    }

    public void ResetCollider()
    {
        col.center = originColCenter;
        col.height = origiColHeight;
    }

    private void OnTriggerEnter(Collider other)
    {
        umatobiTarget = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        umatobiTarget = null;
    }
}
