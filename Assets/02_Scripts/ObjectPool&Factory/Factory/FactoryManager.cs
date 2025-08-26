using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : Singleton<FactoryManager>
{
    [SerializeField] private Factory[] factorys;
    private readonly Dictionary<FactoryType, Factory> factoryList = new();

    public Factory this[FactoryType type]
    {
        get
        {
            factoryList.TryGetValue(type, out Factory factory);

            return factory;
        }
    }

    public IEnumerable<Factory> Factorys => factoryList.Values;

    public override void Awake()
    {
        base.Awake();
        foreach (var factory in factorys)
        {
            if (!factoryList.ContainsKey(factory.Type))
                factoryList[factory.Type] = factory;
        }
    }

    public bool Resister(FactoryType type, Factory factory)
    {
        if (factoryList.ContainsKey(type))
            return false;

        factoryList[type] = factory;

        return true;
    }

    public bool UnResister(FactoryType type)
    {
        return factoryList.Remove(type);
    }

    public bool ContainsFactoryType(FactoryType type)
    {
        return factoryList.ContainsKey(type);
    }
}
