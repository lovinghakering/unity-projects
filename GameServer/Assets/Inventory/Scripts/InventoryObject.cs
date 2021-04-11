using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemDatabaseObject database;
    public List<InventorySlot> Container = new List<InventorySlot>();

    private void OnEnable()
    {
        database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Inventory/ItemDatabase.asset", typeof(ItemDatabaseObject));
    }

    public bool AddItem(int _id, ItemObject _item, int _amount)
    {
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                if (Container[i].amount >= Container[i].item.maxStack)
                    break;
                Container[i].AddAmount(_amount);
                ServerSend.AddItem(_id, database.GetId[_item]);
                return true;
            }
        }
        if (Container.Count <= 24)
        {
            Container.Add(new InventorySlot(database.GetId[_item], _item, _amount));
            ServerSend.AddItem(_id, database.GetId[_item]);
            return true;
        }
        return false;
    }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Container.Count; i++)
            Container[i].item = database.GetItem[Container[i].ID];
    }

    public void OnBeforeSerialize()
    {
    }

    public void Load(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}

[System.Serializable]
public class InventorySlot
{
    public int ID;
    public ItemObject item;
    public int amount;

    public InventorySlot(int _id, ItemObject _item, int _amount)
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