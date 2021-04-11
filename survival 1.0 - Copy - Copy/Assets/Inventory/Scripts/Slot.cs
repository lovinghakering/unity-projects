using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI amountText;

    private void Awake()
    {
        image = GetComponent<Image>();
        amountText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateSlot(ItemObject _item, int _amount)
    {
        image.sprite = _item.sprite;
        image.color = Color.white;
        amountText.text = _amount.ToString("n0");
    }
}
