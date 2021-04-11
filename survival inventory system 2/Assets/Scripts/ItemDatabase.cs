using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Database Object", menuName = "Inventory System/Database")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] items;
    public Dictionary<int, ItemObject> GetItem;

    public void OnBeforeSerialize()
    {
        
    }

    public void OnAfterDeserialize()
    {
        GetItem = new Dictionary<int, ItemObject>();
        for (int i = 1; i < items.Length + 1; i++)
        {
            GetItem.Add(i, items[i - 1]);
        }
    }
}