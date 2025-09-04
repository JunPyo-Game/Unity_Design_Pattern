using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class BulletParticle : MonoBehaviour, IPool<BulletParticle>
{
    private static readonly WaitForSeconds waitForSeconds = new(0.5f);

    public ObjectPool<BulletParticle> Pool { get; set; }
    public bool IsReleased { get; set; } = false;

    [SerializeField] private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        particle.Play();
        StartCoroutine(DisableRoutine());
    }

    private IEnumerator DisableRoutine()
    {
        yield return waitForSeconds;
        Pool.Release(this);
    }
}
