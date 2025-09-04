using UnityEngine;
namespace Observer
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5.0f;

        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            float v = Input.GetAxisRaw("Vertical");
            float h = Input.GetAxisRaw("Horizontal");

            Transform cam = Camera.main.transform;
            Vector3 camForward = new Vector3(cam.forward.x, 0, cam.forward.z).normalized;
            Vector3 moveDir = camForward * v + cam.right * h;

            rb.MovePosition(transform.position + moveSpeed * Time.fixedDeltaTime * moveDir);
            transform.forward = camForward;
        }
    }
}

