using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int id;
    public bool used;
    
    [SerializeField] private InventoryObject inventory;

    private ItemObject item;
    private Image image;
    private TextMeshProUGUI amountText;
    
    private void Awake()
    {
        image = GetComponent<Image>();
        amountText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void SetItem(ItemObject _item)
    {
        used = true;
        item = _item;
        UpdateUI();
    }

    public void UpdateUI()
    {
        image.sprite = item.uiDisplay;
        amountText.text = inventory.container.items[item.Id].amount.ToString("n0");
    }
}
