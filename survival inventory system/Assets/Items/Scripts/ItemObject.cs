using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Object", menuName = "Inventory System/Item")]
public class ItemObject : ScriptableObject
{
    public int id;
    public Sprite sprite;
    public string itemName;
    [TextArea(15, 20)]
    public string description;
    public int maxStack;
    public ItemBuff[] buffs;
}

public enum Attributes
{
    AttackDamage,
    AttackSpeed,
    Defence,
    MovementSpeed,
    HealthRegenerationValue,
    HealthRegenerationSpeed,
}

[Serializable]
public class ItemBuff
{
    public Attributes attributes;
    public int value;
}
