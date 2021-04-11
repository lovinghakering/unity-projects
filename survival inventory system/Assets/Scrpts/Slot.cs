using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public int id;
    public bool used;
    
    [SerializeField] private InventoryObject inventory;
    [SerializeField] private InventoryDisplay inventoryDisplay;

    private Image image;
    private TextMeshProUGUI amountText;

    private void Awake()
    {
        image = GetComponent<Image>();
        amountText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateUI()
    {
        if (inventory.container.slots[id].used)
        {
            used = inventory.container.slots[id].used;
            image.sprite = inventory.container.slots[id].item.sprite;
            amountText.text = inventory.container.slots[id].amount.ToString("n0");
        }
        else
        {
            used = inventory.container.slots[id].used;
            image.sprite = null;
            amountText.text = "";
        }
    }

    public void Clear()
    {
        used = false;
        image.sprite = null;
        amountText.text = "";
    }

    public void OnBeginDrag(PointerEventData _eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData _eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    
    public void OnDrag(PointerEventData _eventData) { }

    public void OnDrop(PointerEventData _eventData)
    {   
        int _getSlotId = _eventData.pointerDrag.GetComponent<Slot>().id;
        inventory.MoveItem(_getSlotId, id);
        inventoryDisplay.UpdateSlot(_getSlotId);
        UpdateUI();
    }
}
