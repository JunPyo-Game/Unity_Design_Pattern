using UnityEngine;

[CreateAssetMenu(fileName = "BulletPool", menuName = "BulletPool", order = 0)]
public class BulletPool : ScriptableObject
{
    ObjectPool<Bullet> bulletPool;
    [SerializeField] Bullet bulletPrefab;

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.Pool = this;
        bullet.gameObject.SetActive(false);

        return bullet;
    }

    public void GetBullet(Vector3 position)
    {
        if (bulletPool == null)
            bulletPool = new(createFunc: CreateBullet);

        Bullet bullet = bulletPool.Get();
        bullet.transform.position = position;
        bullet.gameObject.SetActive(true);
    }

    public void ReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bulletPool.Release(bullet);
    }
}
