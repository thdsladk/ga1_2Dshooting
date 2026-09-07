using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("# Main Info")] public ItemType itemType;

    public string itemName;
    [TextArea] public string description;

    [Header("# Level Data")] public int BuffScale;
}