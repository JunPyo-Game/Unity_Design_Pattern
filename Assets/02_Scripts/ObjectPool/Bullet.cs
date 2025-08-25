using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletPool Pool { get; set; }
    [SerializeField] private float moveSpeed = 8.0f;
    
    private void Update()
    {
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Pool.ReleaseBullet(this);
        }
    }
}
