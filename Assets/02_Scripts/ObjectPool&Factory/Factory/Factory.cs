using UnityEngine;

public interface IProduct
{
    public string Name { get; set; }
    public void Init();
}

public interface IFactory
{
    public IProduct GetProduct(Vector3 position);
}
