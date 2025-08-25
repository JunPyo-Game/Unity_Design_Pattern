using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new(h, v, 0);

        if (dir.magnitude != 0)
            transform.position += moveSpeed * Time.deltaTime * dir.normalized;
    }
}
