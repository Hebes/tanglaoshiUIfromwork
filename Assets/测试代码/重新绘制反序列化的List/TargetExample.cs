using System.Collections.Generic;
using UnityEngine;

public class TargetExample : MonoBehaviour
{
    [SerializeField]
    public List<PlayerItem> playerItemArray = new List<PlayerItem>();
}

[System.Serializable]
public class PlayerItem
{
    [SerializeField]
    public Texture icon;
    [SerializeField]
    public GameObject prefab;
    [SerializeField]
    public string name;
    [SerializeField]
    public int attack;
}
