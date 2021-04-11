// using System;
// using UnityEngine;
//
// [CreateAssetMenu(fileName = "New Inventory Object", menuName = "Inventory System/Inventory")]
// public class InventoryObject : ScriptableObject
// {
//     public ItemContainer container;
//
//     public void AddItem(GroundItem _groundItem, InventoryDisplay _inventoryDisplay)
//     {
//         var _item = _groundItem.item;
//         var _amount = _groundItem.amount;
//         
//         for (int i = 0; i < container.slots.Length; i++)
//         {
//             var _slot = container.slots[i];
//             if (_slot.used)
//             {
//                 if (_slot.item.id == _item.id)
//                 {
//                     if (_slot.AddAmount(_groundItem, _amount))
//                     {
//                         _inventoryDisplay.UpdateSlot(i);
//                         return;
//                     }
//                 }
//             }
//         }
//
//         int _slotId = FindEmptySlot();
//         if (_slotId == -1)
//         {
//             return;
//         }
//         
//         var _itemSlot = container.slots[_slotId];
//         _itemSlot.used = true;
//         _itemSlot.item = _item;
//         _itemSlot.amount = _amount;
//         _inventoryDisplay.UpdateSlot(_slotId);
//     }
//
//     private int FindEmptySlot()
//     {
//         for (int i = 0; i < container.slots.Length; i++)
//         {
//             if (!container.slots[i].used)
//             {
//                 return i;
//             }
//         }
//
//         return -1;
//     }
//
//     public void MoveItem(int _fromSlotId, int _toSlotId)
//     {
//         ItemSlot _fromSlot = container.slots[_fromSlotId];
//         ItemSlot _toSlot = container.slots[_toSlotId];
//
//         container.slots[_fromSlotId] = null;
//         container.slots[_toSlotId] = null;
//
//         container.slots[_fromSlotId] = _toSlot;
//         container.slots[_toSlotId] = _fromSlot;
//     }
//
//     public void ClearItem(int _slotId)
//     {
//         container.slots[_slotId] = new ItemSlot();
//     }
//
//     [ContextMenu("Clear")]
//     public void Clear()
//     {
//         container = new ItemContainer();
//     }
// }
//
// [Serializable]
// public class ItemContainer
// {
//     public ItemSlot[] slots = new ItemSlot[20];
// }
//
// [Serializable]
// public class ItemSlot
// {
//     public bool used;
//     public ItemObject item;
//     public int amount;
//
//     public bool AddAmount(GroundItem _groundItem, int _amount)
//     {
//         if (amount >= item.maxStack)
//             return false;
//
//         amount += _amount; 
//         float _leftOver = Mathf.Clamp(amount, 0, item.maxStack);
//         _groundItem.SetAmount(_leftOver);
//         return true;
//     }
// }
