using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private InventoryObject inventoryObject;
    
    public void OnDrop(PointerEventData _eventData)
    {
        if (_eventData.pointerDrag.CompareTag("ItemSlot"))
        {
            var _slot = _eventData.pointerDrag.GetComponent<Slot>();
            _slot.Clear();
            inventoryObject.ClearItem(_slot.id);
        }
    }

    public void OnBeginDrag(PointerEventData _eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData _eventData)
    {
        
    }

    public void OnDrag(PointerEventData _eventData)
    {
        
    }
}
