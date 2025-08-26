using UnityEngine;

public enum FactoryType
{
    Bullet,
    Enemy
}

public abstract class Factory : ScriptableObject
{
    public abstract IProduct GetProduct(Vector3 position);
    public abstract FactoryType Type { get; }
}
