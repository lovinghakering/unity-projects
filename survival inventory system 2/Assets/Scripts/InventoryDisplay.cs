using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private InventoryObject inventory;
    [SerializeField] private ItemDatabase database;
    [SerializeField] private List<Slot> slots;

    private void Awake()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].id = i;
        }
    }

    public void UpdateSlot(int _slotId)
    {
        if (inventory.container.slots[_slotId].id == 0 || inventory.container.slots[_slotId].amount <= 0)
        {
            slots[_slotId].UpdateUI(null, "");
        }
        else
        {
            Sprite _sprite = database.GetItem[inventory.container.slots[_slotId].id].sprite;
            int _amount = inventory.container.slots[_slotId].amount;
            slots[_slotId].UpdateUI(_sprite, _amount);
        }
    }

    public void SwapSlots(int _firstSlotId, int _secondSlotId)
    {
        inventory.DragItem(_firstSlotId, _secondSlotId);

        UpdateSlot(_firstSlotId);
        UpdateSlot(_secondSlotId);
    }
}
