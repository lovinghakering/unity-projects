using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropZone : MonoBehaviour, IDropHandler
{
    [SerializeField] private InventoryObject inventoryObject;
    
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.CompareTag("ItemSlot"))
        {
            var slot = eventData.pointerDrag.GetComponent<Slot>();
            slot.Clear();
            inventoryObject.ClearItem(slot.id);
        }
    }
}
