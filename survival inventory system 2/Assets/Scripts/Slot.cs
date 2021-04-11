using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public int id;

    [SerializeField] private Transform canvasObject;
    [SerializeField] private GameObject dragPrefab;
    [SerializeField] private InventoryDisplay display;
    
    private Image itemImage;
    private TextMeshProUGUI amountText;

    public RectTransform dragObject;
    
    private void Awake()
    {
        itemImage = transform.GetChild(1).GetComponent<Image>();
        amountText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Clear()
    {
        itemImage.sprite = null;
        amountText.text = "";
    }

    public void UpdateUI(Sprite _sprite, int _amount)
    {
        itemImage.sprite = _sprite;
        amountText.text = _amount.ToString();
    }
    
    public void UpdateUI(Sprite _sprite, string _amount)
    {
        if (_sprite == null)
        {
            itemImage.sprite = null;
        }
        else
        {
            itemImage.sprite = _sprite;
        }
        
        amountText.text = _amount;
    }

    public void OnPointerClick(PointerEventData _eventData)
    {
        if (_eventData.button == PointerEventData.InputButton.Right)
        {
            GameManager.instance.UpdateDragObject(id, 1);
        }
    }
    
    // public void OnBeginDrag(PointerEventData _eventData)
    // {
    //     GameObject _obj = Instantiate(dragPrefab, transform.parent.parent.parent);
    //     _obj.GetComponent<Image>().sprite = itemImage.sprite;
    //     dragObject = _obj.GetComponent<RectTransform>();
    //     GetComponent<CanvasGroup>().blocksRaycasts = false;
    // }
    //
    // public void OnEndDrag(PointerEventData _eventData)
    // {
    //     Destroy(dragObject.gameObject);
    //     GetComponent<CanvasGroup>().blocksRaycasts = true;
    // }
    //
    // public void OnDrag(PointerEventData _eventData)
    // {
    //     Vector3 _newPos = Input.mousePosition;
    //     _newPos.z = 0f;
    //     dragObject.transform.position = _newPos;
    // }
    //
    // public void OnDrop(PointerEventData _eventData)
    // {   
    //     int _getSlotId = _eventData.pointerDrag.GetComponent<Slot>().id;
    //     display.SwapSlots(id, _getSlotId);
    // }
}
