namespace UnityChanDemo
{
    using UnityEngine;

    public class FollowCam : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new(0, 3, -4);

        private void LateUpdate()
        {
            transform.position = target.position + target.rotation * offset;
            transform.LookAt(target.position);
        }
    }
}
