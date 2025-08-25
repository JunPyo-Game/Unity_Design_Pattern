using UnityEngine;

public abstract class Factory<T> : ScriptableObject, IFactory where T : MonoBehaviour, IProduct
{
    [SerializeField] protected T prefab;

    public IProduct GetProduct(Vector3 position)
    {
        IProduct product = Instantiate(prefab, position, Quaternion.identity);
        product.Init();

        return product;
    }
}
