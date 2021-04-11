using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipable Object", menuName = "Inventory System/Items/Equipable")]
public class EquipmentObject : ItemObject
{
    public float attackBonus;
    public float defenceBonus;

    private void Awake()
    {
        type = ItemType.Equipment;
    }
}
