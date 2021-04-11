using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryDisplay inventoryDisplay;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
        }
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            inventory.Load();
        }
    }

    private void OnTriggerEnter(Collider _collider)
    {
        var _groundItem = _collider.GetComponent<GroundItem>();
        if (_groundItem)
        {
            inventory.PickNewItem(_groundItem.item, 1);
            int _slotId = inventory.AddItem(_groundItem.item, 1);
            if (inventoryDisplay.IsSlotUsed(_slotId))
            {
                inventoryDisplay.UpdateSlot(_slotId);
            }
            else
            {
                inventoryDisplay.SetItemToSlot(_slotId, _groundItem.item);
            }
            
            Destroy(_collider.gameObject);
        }
    }
}
