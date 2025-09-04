using UnityEngine;

public class TurretFire : MonoBehaviour
{
    [SerializeField] private BulletController bulletPrefab;
    [SerializeField] private BulletParticle particlePrefab;
    [SerializeField] private Transform firePos1;
    [SerializeField] private Transform firePos2;
    private ObjectPool<BulletController> bulletPool;
    private ObjectPool<BulletParticle> particlePool;

    private void Awake()
    {
        bulletPool = new(CreateBullet, OnGetBullet, OnReleaseBullet);
        particlePool = new(CreateParticle, OnGetParticle, OnReleaseParticle);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
        BulletParticle particle = particlePool.Get();
        particle.transform.position = bullet.transform.position;
        particle.transform.forward = -bullet.transform.forward;

        bullet.gameObject.SetActive(false);
    }

    private BulletParticle CreateParticle()
    {
        BulletParticle particle = Instantiate(particlePrefab);
        particle.gameObject.SetActive(false);

        return particle;
    }

    private void OnGetParticle(BulletParticle particle)
    {
        particle.gameObject.SetActive(true);
    }

    private void OnReleaseParticle(BulletParticle particle)
    {
        particle.gameObject.SetActive(false);
    }
}
