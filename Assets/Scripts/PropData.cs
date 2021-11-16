using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PropData
{
    public GameObject PropPrefab;
    public GameObject PropPrefabNoFun;
    public int cost;
    public int CD;
    public float duration;
    public PropType type;
}

public enum PropType
{
    FrozenProp,
    TransportationProp
}