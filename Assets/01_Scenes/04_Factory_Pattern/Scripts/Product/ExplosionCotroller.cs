using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour, IProduct, IPool<Explosion>
{
    private static WaitForSeconds _waitForSeconds0_5 = new WaitForSeconds(0.5f);

    public string Name { get => gameObject.name; set => gameObject.name = value; }
    public ObjectPool<Explosion> Pool { get; set; }
    public bool IsReleased { get; set; } = false;

    public void Init()
    {
        StartCoroutine(ReleaseRoutine());
    }

    IEnumerator ReleaseRoutine()
    {
        yield return _waitForSeconds0_5;
        Pool.Release(this);
    }
}