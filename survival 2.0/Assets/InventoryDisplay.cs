using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private List<Slot> slots;

    private void OnEnable()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            slots.Add(transform.GetChild(0).GetChild(i).GetComponent<Slot>());
            slots[i].id = i;
        }
        
        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            slots.Add(transform.GetChild(1).GetChild(i).GetComponent<Slot>());
            slots[i].id = i;
        }
    }

    public bool IsSlotUsed(int _slotId) => slots[_slotId].used;

    public void UpdateSlot(int _slotId) => slots[_slotId].UpdateUI();
    
    public void SetItemToSlot(int _slotId, ItemObject _item) => slots[_slotId].SetItem(_item);
}
