using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new(0, 3, -4);
    [SerializeField] private float sensitivity = 5.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private Quaternion rot;

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            yaw += mouseX;
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -80.0f, 80.0f);

            rot = Quaternion.Euler(pitch, yaw, 0);
        }

        transform.position = target.position + rot * offset;
        transform.LookAt(target.position);
    }
}
