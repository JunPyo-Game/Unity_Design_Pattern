using TMPro;
using UnityEngine;
using State;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private LayerMask groundLayer;

    private readonly float sphereRadius = 0.25f;

    private Rigidbody rb;
    private MeshRenderer mr;
    private TMP_Text stateText;
    private StateMachine stateMachine;

    private Vector3 moveDir;
    private bool isGround;

    public StateMachine StateMachine => stateMachine;
    public float Velocity => moveDir.sqrMagnitude;
    public bool IsGround => isGround;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
        stateText = GetComponentInChildren<TMP_Text>();

        IState[] states =
        {
            new IdleState(this),
            new MoveState(this),
            new JumpState(this),
        };

        stateMachine = new(states, StateType.Idle);
    }

    private void Update()
    {
        CheckGround();

        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        moveDir = new Vector3(h, 0, v).normalized;
        rb.MovePosition(transform.position + moveSpeed * Time.fixedDeltaTime * moveDir);
    }

    private void CheckGround()
    {
        Vector3 spherePos = transform.position - new Vector3(0.0f, 1.0f, 0.0f);
        isGround = Physics.CheckSphere(spherePos, sphereRadius, groundLayer);
    }

    public void SetMaterialColor(Color color)
    {
        mr.material.color = color;
    }

    public void SetStateText(string stateText)
    {
        this.stateText.text = stateText;
    }
}
