using UnityEngine;

[CreateAssetMenu(fileName = "BulletFactory", menuName = "BulletFactory", order = 0)]
public class BulletFactory : ScriptableObject, IFactory
{
    private ObjectPool<Bullet> bulletPool;
    [SerializeField] private Bullet bulletPrefab;

    public IProduct GetProduct(Vector3 position)
    {
        bulletPool ??= new(
            createFunc: Create,
            onGet: OnGet,
            onRelease: OnRelease
        );

        Bullet bullet = bulletPool.Get();
        bullet.transform.position = position;
        bullet.Init();

        return bullet;
    }

    private Bullet Create()
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.Pool = bulletPool;
        bullet.gameObject.SetActive(false);

        return bullet;
    }

    private void OnGet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnRelease(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
}
