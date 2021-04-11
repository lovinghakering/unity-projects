using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;
    
    public void Populate(Sprite _sprite, int _amount)
    {
        image.sprite = _sprite;
        text.text = _amount.ToString();
    }
}