using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Player player;

    public InventoryObject inventoryObject;
    public InventoryDisplay inventoryDisplay;
    private void OnTriggerEnter(Collider _other)
    {
        var _item = _other.GetComponent<GroundItem>();
        if (_item)
        {
            inventoryObject.AddItem(_item, inventoryDisplay);
        }
    }
}
