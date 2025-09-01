using UnityEngine;

public class Fire : MonoBehaviour
{
    BulletFactory bulletFactory;
    [SerializeField] Transform firePos;

    private void Start()
    {
        bulletFactory = FactoryManager.Instance[FactoryType.Bullet] as BulletFactory;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bulletFactory.GetProduct(firePos.position);
        }
    }
}
