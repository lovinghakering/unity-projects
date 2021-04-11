using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Player player;
        
    [SerializeField] private InventoryDisplay inventoryDisplay;
    [SerializeField] private InventoryObject inventory;
    
    public void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Item"))
        {
            var _groundItem = _other.GetComponent<GroundItem>();
            if (!_groundItem.pickedUp)
            {
                _groundItem.pickedUp = true;
                Destroy(_other.gameObject);
                inventory.AddItem(_groundItem, inventoryDisplay);
            }
        }
    }
}