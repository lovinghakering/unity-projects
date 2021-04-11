using System.Collections.Generic;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    public Transform quickBar;
    public Transform inventoryUI;
    public InventoryObject inventory;

    public List<Slot> slots = new List<Slot>();

    private void OnValidate()
    {
        if (quickBar && inventoryUI)
        {
            slots.Clear();
            for (int i = 0; i < quickBar.childCount; i++)
            {
                slots.Add(quickBar.GetChild(i).GetComponent<Slot>());
            }

            for (int i = 0; i < inventoryUI.childCount; i++)
            {
                slots.Add(inventoryUI.GetChild(i).GetComponent<Slot>());
            }
        }
    }
    
    //private void Start()
    //{
    //    for (int i = 0; i < inventory.Container.Count; i++)
    //    {
    //        if (!inventoryUI.gameObject.activeSelf && i == 6)
    //            return;
    //        slots[i].UpdateSlot(inventory.Container[i].item, inventory.Container[i].amount);
    //    }
    //}

    public void UpdateInventory()
    {
        for (int i = 6; i < inventory.Container.Items.Count; i++)
        {
            //slots[i].UpdateSlot(inventory.Container.Items[i]., inventory.Container.Items[i].amount);
        }
    }

    public void UpdateSlot(int index)
    {
        //slots[index].UpdateSlot(inventory.Container.Items[index].item, inventory.Container.Items[index].amount);
    }
}
