using System;
using UnityEngine;

public enum Attributes
{
    AttackDamage,
    AttackSpeed,
    Defence,
    MovementSpeed,
    HealthRegenerationValue,
    HealthRegenerationSpeed,
}

[CreateAssetMenu(fileName = "New Item Object", menuName = "Inventory System/Item")]
public class ItemObject : ScriptableObject
{
    public int id;
    public GameObject prefab;
    public Sprite sprite;
    public new string name;
    [TextArea(15, 20)]
    public string description;
    public int maxStack;
    public ItemBuff[] buffs;
}

[Serializable]
public class ItemBuff
{
    public Attributes attribute;
    public int value;
}
