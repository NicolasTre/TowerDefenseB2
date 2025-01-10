using System;
using UnityEngine;

[Serializable]
public class TD_Tower
{
    public string name;
    public int cost;
    public GameObject prefab;

    public TD_Tower (string _name, int _cost, GameObject _prefab)
    {
        name = _name;
        cost = _cost;
        prefab = _prefab;
    }
}
