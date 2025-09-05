using UnityEngine;

public class TurretFire : MonoBehaviour
{
    [SerializeField] private BulletController bulletPrefab;
    [SerializeField] private Transform firePos1;
    [SerializeField] private Transform firePos2;
    private ObjectPool<BulletController> bulletPool;

    private void Awake()
    {
        bulletPool = new(CreateBullet, OnGetBullet, OnReleaseBullet, maxSize: 300);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            FireBullet(firePos1);
            FireBullet(firePos2);
        }
    }

    private void FireBullet(Transform firePos)
    {
        BulletController bullet = bulletPool.Get();
        bullet.transform.forward = firePos.forward;
        bullet.transform.position = firePos.position;
    }

    private BulletController CreateBullet()
    {
        BulletController bullet = Instantiate(bulletPrefab);
        bullet.gameObject.SetActive(false);

        return bullet;
    }

    private void OnGetBullet(BulletController bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(BulletController bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.Reset();
    }

    public void GetCount(out int allCount, out int inActiveCount, out int activeCount)
    {
        allCount = bulletPool.CountAll;
        inActiveCount = bulletPool.CountInactive;
        activeCount = bulletPool.CountActive;
    }
}
