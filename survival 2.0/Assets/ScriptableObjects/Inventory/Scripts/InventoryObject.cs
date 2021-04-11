using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    [SerializeField] private string savePath;
    public Inventory container;

    public int AddItem(ItemObject _itemObject, int _amount)
    {
        Item _item = new Item(_itemObject);
        
        if (_item.buffs.Length > 0)
        {
            //container.items.Add(new InventorySlot(_item.Id, _item, _amount));
            return 0; // container.items.Count - 1;
        }
        
        for (int i = 0; i < container.items.Length; i++)
        {
            if (container.items[i].item.Id == _item.Id)
            {
                container.items[i].AddAmount(_amount);
                return i;
            }
        }
        
        //container.items.Add(new InventorySlot(_item.Id, _item, _amount));
        return 0; //container.items.Count - 1;
    }

    public void PickNewItem(ItemObject _item, int _amount)
    {
        for (int i = 0; i < container.items.Length; i++)
        {
            if (container.items[i] != null)
            {
                Debug.Log(string.Concat("Free slot on: ", i));
                break;
            }
        }
    }
    
    // Loading, saving and claering
    [ContextMenu("Save")]
    public void Save()
    {
        string _saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter _bf = new BinaryFormatter();
        FileStream _file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        _bf.Serialize(_file, _saveData);
        _file.Close();
    }

    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter _bf = new BinaryFormatter();
            FileStream _file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(_bf.Deserialize(_file).ToString(), this);
            _file.Close();
        }
    }
    
    [ContextMenu("Clear")]
    public void Clear()
    {
        container = new Inventory();
    }
}

[System.Serializable]
public class Inventory
{
    public InventorySlot[] items = new InventorySlot[20];
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

    public void AddAmount(int _value)
    {
        amount += _value;
    }
}