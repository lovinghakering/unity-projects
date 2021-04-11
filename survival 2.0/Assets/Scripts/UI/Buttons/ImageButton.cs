using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent onClick;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color clickColor;
    
    private Image image;
    private bool hovered;
    private bool clicked;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
        if (!clicked)
            image.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
        if (!clicked)
            image.color = normalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        clicked = true;
        image.color = clickColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        clicked = false;
        onClick.Invoke();
        if (hovered)
            image.color = hoverColor;
        else
            image.color = normalColor;
    }
}