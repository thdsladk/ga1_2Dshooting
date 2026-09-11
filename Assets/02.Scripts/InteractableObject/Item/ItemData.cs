using UnityEngine;

[System.Serializable]
public class ItemData
{
    [Header("# ItemType ")] public ItemType itemType;

    public string itemName;
    [TextArea] public string description;

    [Header("# Item BuffScale ")] public int BuffScale;

    public int SpawnWeight;
}