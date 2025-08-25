using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] BulletPool bulletPool;
    [SerializeField] Transform firePos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bulletPool.GetBullet(firePos.position);
        }
    }
}
