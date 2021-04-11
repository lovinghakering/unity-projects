using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Object", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public ItemDatabase database;
    public ItemContainer container;
    
    public void AddItem(GroundItem _groundItem, InventoryDisplay _inventoryDisplay)
    {
        for (int i = 0; i < container.slots.Length; i++)
        {
            if (container.slots[i].id == _groundItem.itemId)
            {
                if (container.slots[i].CanAddAmount(database.GetItem[_groundItem.itemId].maxStack))
                {
                    _groundItem.SetAmount(container.slots[i].AddAmount(_groundItem.amount, database.GetItem[_groundItem.itemId].maxStack));
                    _inventoryDisplay.UpdateSlot(i);
                    return;
                }
            }

            if (!container.slots[i].used)
            {
                container.slots[i].amount = _groundItem.amount;
                container.slots[i].id = _groundItem.itemId;
                _groundItem.SetAmount(0);
                _inventoryDisplay.UpdateSlot(i);
                return;
            }
            
        }
    }

    public void AddItemFromCursor(int _slotId, int _itemId, int _amount)
    {
        if (container.slots[_slotId].id == _itemId)
        {
            if (container.slots[_slotId].CanAddAmount(database.GetItem[_itemId].maxStack))
            {
                 container.slots[_slotId].AddAmount(_amount, database.GetItem[_itemId].maxStack);
            }
        }
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        container = new ItemContainer();
    }

    public void ClearItem(int _slotID)
    {
        container.slots[_slotID] = new ItemSlot();
    }

    public void DragItem(int _fromSlotId, int _toSlotId)
    { 
        //  container.slots[_fromSlotId]
        //  container.slots[_toSlotId]
        
        if(container.slots[_toSlotId].id == 0) return;

        if (container.slots[_fromSlotId].id == container.slots[_toSlotId].id)
        {
            //Same item
            if(container.slots[_toSlotId].CanAddAmount(database.GetItem[container.slots[_fromSlotId].id].maxStack))
            {
                //Can add more items to the stack
                int _leftOver = container.slots[_fromSlotId]
                    .AddAmount(container.slots[_toSlotId].amount,
                        database.GetItem[container.slots[_toSlotId].id].maxStack);

                if (_leftOver <= 0)
                {
                    container.slots[_toSlotId].amount = 0;
                    container.slots[_toSlotId].id = 0;
                }
            }
            else
            {
                SwapSlots(_fromSlotId, _toSlotId);
            }
        }
        else
        {
            //Different items
            SwapSlots(_fromSlotId, _toSlotId);
        }
    }

    private void SwapSlots(int _fromSlotId, int _toSlotId)
    {
        var _fromSlot = container.slots[_fromSlotId];
        var _toSlot = container.slots[_toSlotId];

        container.slots[_fromSlotId] = new ItemSlot();
        container.slots[_toSlotId] = new ItemSlot();

        container.slots[_fromSlotId] = _toSlot;
        container.slots[_toSlotId] = _fromSlot;
    }
}

[Serializable]
public class ItemContainer
{
    public ItemSlot[] slots = new ItemSlot[20];
}

[Serializable]
public class ItemSlot
{
    public bool used
    {
        get
        {
            if (id > 0)
                return true;
            else 
                return false;
        }
    }
    public int id;
    public int amount;

    public bool CanAddAmount(int _maxStack)
    {
        if (amount >= _maxStack)
        {
            return false;
        }

        return true;
    }
    
    public int AddAmount(int _amount, int _maxStack)
    {
        if (amount + _amount <= _maxStack)
        {
            amount += _amount;
            return 0;
        }

        int _leftOver = _maxStack - (amount + _amount);
        amount = _maxStack;
        return _leftOver;
    }

    public int RemoveAmount(int _amount, int _maxStack)
    {
        if (amount >= _amount)
        {
            
        }
        else
        {
            
        }
    }
}