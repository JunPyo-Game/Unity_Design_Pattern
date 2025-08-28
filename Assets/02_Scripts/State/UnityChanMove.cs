using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
public class UnityChanMove : MonoBehaviour
{
    static private readonly int HashVertical = Animator.StringToHash("Vertical");
    static private readonly int HashHorizontal = Animator.StringToHash("Horizontal");
    static private readonly int HashJumpTrigger = Animator.StringToHash("JumpTrigger");
    static private readonly int HashJumpHeight = Animator.StringToHash("JumpHeight");

    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private float walkSpeed = 1.0f;
    [SerializeField] private float runSpeed = 5.0f;
    [SerializeField] private float rotateSpeed = 120.0f;
    [SerializeField] private float jumpForce = 4.5f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody rb;
    private CapsuleCollider col;
    private Animator animator;
    private Vector3 orginColCenter;

    private float moveSpeed;

    public StateMachine StateMachine => stateMachine;
    public Rigidbody Rigidbody => rb;

    public bool IsGround { get; private set; }
    public bool IsJump { get; private set; }

    public float Vertical => Input.GetAxisRaw("Vertical");
    public float Horizontal => Input.GetAxisRaw("Horizontal");
    public float JumpHeight => animator.GetFloat(HashJumpHeight);

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        orginColCenter = col.center;
        moveSpeed = walkSpeed;

        IState[] states = new IState[]
        {
            new WaitState(this),
            new MoveState(this),
            new JumpStartState(this),
            new JumpAirState(this),
            new JumpLandState(this)
        };

        stateMachine = new(states, StateType.Wait);
    }

    private void Update()
    {
        Vector3 spherePos = new(transform.position.x, transform.position.y + JumpHeight, transform.position.z);
        IsGround = Physics.CheckSphere(spherePos, 0.1f, groundLayer);

        stateMachine.Update();
    }

    public void Move(float v)
    {
        float speed = v >= 0 ? moveSpeed : walkSpeed;

        transform.localPosition += speed * Time.deltaTime * v * transform.forward;
        animator.SetInteger(HashVertical, (int)v);
    }

    public void Rotate(float h)
    {
        transform.localRotation *= Quaternion.Euler(0, h * rotateSpeed * Time.deltaTime, 0);
        animator.SetInteger(HashHorizontal, (int)h);
    }

    public void ToggleRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            bool isRun = animator.GetBool("IsRun");
            animator.SetBool("IsRun", !isRun);
            moveSpeed = isRun ? walkSpeed : runSpeed;
        }

    }

    public void JumpStart()
    {
        if (!IsJump && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger(HashJumpTrigger);
            IsJump = true;
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }

    public void UpdateCollider()
    {
        col.center = new Vector3(0, orginColCenter.y + JumpHeight, 0);
    }

    public void ResetCollider()
    {
        col.center = orginColCenter;
    }

    public void OnJumpEnd()
    {
        IsJump = false;
    }
}
