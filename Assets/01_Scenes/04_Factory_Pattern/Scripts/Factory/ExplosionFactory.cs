using UnityEngine;

[CreateAssetMenu(fileName = "ExplosionFactory", menuName = "ExplosionFactory", order = 0)]
public class ExplosionFactory : PooledFactory<Explosion>
{
    public override FactoryType Type => FactoryType.Explosion;
}
