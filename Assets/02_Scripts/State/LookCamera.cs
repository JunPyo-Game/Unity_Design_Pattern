using UnityEngine;

public class LookCamera : MonoBehaviour
{
    private void Update()
    {
        transform.forward = (transform.position - Camera.main.transform.position).normalized;
    }
}
