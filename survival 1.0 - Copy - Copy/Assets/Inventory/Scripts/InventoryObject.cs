using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public ItemDatabaseObject database;
    public DisplayInventory displayInventory;
    public Inventory Container;

    public InventoryObject(DisplayInventory _displayInventory)
    {
        displayInventory = _displayInventory;
    }

    private void OnEnable()
    {
        database = Resources.Load<ItemDatabaseObject>("ItemDatabase");
    }

    private void Awake()
    {
        Container = new Inventory();
    }

    public void AddItem(Item _item, int _amount)
    {
        int index = 0;
        for (int i = 0; i < Container.Items.Count; i++)
        {
            if (Container.Items[i].item == _item)
            {
                if (Container.Items[i].amount >= Container.Items[i].item.MaxStack)
                    break;
                index = i;
                Container.Items[i].AddAmount(_amount);
                displayInventory.UpdateSlot(index);
                return;
            }
        }
        if (Container.Items.Count <= 24)
        {
            index = Container.Items.Count;
            Container.Items.Add(new InventorySlot(_item.Id, _item, _amount));
            displayInventory.UpdateSlot(index);
        }
    }
}

[System.Serializable]
public class Inventory
{
    public List<InventorySlot> Items = new List<InventorySlot>();
}

[System.Serializable]
public class InventorySlot
{
    public int ID;
    public Item item;
    public int amount;

    public InventorySlot(int _id, Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}