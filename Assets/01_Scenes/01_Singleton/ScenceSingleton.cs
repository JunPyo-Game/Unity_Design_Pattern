using UnityEngine;

public class ScenceSingleton<T> : MonoBehaviour where T : Component
{
    private static T instance;

    public static T Instance => instance;

    public virtual void Awake()
    {
        if (instance == null)
        {
            Debug.Log("create");
            instance = this as T;
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Destory");
        }
    }
}
