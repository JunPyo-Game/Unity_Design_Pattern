using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] BulletFactory bulletFactory;
    [SerializeField] Transform firePos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bulletFactory.GetProduct(firePos.position);
        }
    }
}
