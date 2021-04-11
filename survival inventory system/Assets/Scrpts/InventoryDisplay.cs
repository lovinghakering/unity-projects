using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private List<Slot> slots;

    public void Start()
    {
        UpdateAll();
    }

    private void UpdateAll()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].UpdateUI();
        }
    }

    public bool IsSlotUsed(int _slotId) => slots[_slotId].used;

    public void UpdateSlot(int _slotId) => slots[_slotId].UpdateUI();
}
