using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 프로젝트 내 모든 팩토리를 통합 관리하는 매니저 클래스입니다.
/// 싱글톤으로 동작하며, 팩토리 등록/조회/전체 반환 기능을 제공합니다.
/// </summary>
public class FactoryManager : Singleton<FactoryManager>
{
    /// <summary>
    /// 인스펙터에서 할당할 팩토리 배열입니다.
    /// </summary>
    [SerializeField] private Factory[] factorys;

    /// <summary>
    /// 팩토리 타입별로 등록된 팩토리 인스턴스를 관리하는 딕셔너리입니다.
    /// </summary>
    private readonly Dictionary<FactoryType, Factory> factoryList = new();

    /// <summary>
    /// enum 타입으로 팩토리를 조회할 수 있는 인덱서입니다.
    /// </summary>
    public Factory this[FactoryType type]
    {
        get
        {
            factoryList.TryGetValue(type, out Factory factory);
            return factory;
        }
    }

    /// <summary>
    /// 현재 등록된 모든 팩토리 인스턴스를 반환합니다.
    /// </summary>
    public IEnumerable<Factory> Factorys => factoryList.Values;

    /// <summary>
    /// 팩토리 배열을 순회하며 타입별로 딕셔너리에 등록합니다.
    /// </summary>
    public override void Awake()
    {
        base.Awake();
        foreach (var factory in factorys)
        {
            if (!factoryList.ContainsKey(factory.Type))
                factoryList[factory.Type] = factory;
        }
    }

    /// <summary>
    /// 외부에서 팩토리를 동적으로 등록할 때 사용하는 메서드입니다.
    /// 이미 등록된 타입이면 false, 성공 시 true 반환.
    /// </summary>
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
