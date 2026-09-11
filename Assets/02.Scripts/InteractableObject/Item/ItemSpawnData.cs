using UnityEngine;

[System.Serializable]
public class ItemSpawnData
{
    public ItemType Type;

    //public GameObject ItemPrefab;
    [IntPercent] public int Weight;
}