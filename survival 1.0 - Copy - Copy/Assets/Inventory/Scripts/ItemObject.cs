using UnityEngine;

public enum ItemType
{ 
    Food,
    Equipment,
    Default
}
public abstract class ItemObject : ScriptableObject
{
    public int Id;
    public Sprite sprite;
    public ItemType type;
    public int maxStack;
    [TextArea(15, 20)]
    public string description;

}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public int MaxStack;

    public Item (ItemObject item)
    {
        Name = item.name;
        Id = item.Id;
    }
}


