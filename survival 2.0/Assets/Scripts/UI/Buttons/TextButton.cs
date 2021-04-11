using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool interactable = true;
    [SerializeField] private bool autoSize = false;
    [SerializeField] private Color normalColor = Color.black;
    [SerializeField] private Color hoverColor = Color.black;
    [SerializeField] private Color clickColor = Color.black;
    public UnityEvent onClick;
    
    private TextMeshProUGUI text;
    private bool hovered;
    private bool clicked;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        if (autoSize)
            GetComponent<RectTransform>().sizeDelta = new Vector2(text.preferredWidth, text.preferredHeight);
    }

    public void OnPointerEnter(PointerEventData _eventData)
    {
        hovered = true;
        if (!interactable) return;
        if (!clicked)
            text.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData _eventData)
    {
        hovered = false;
        if (!interactable) return;
        if (!clicked)
            text.color = normalColor;
    }

    public void OnPointerDown(PointerEventData _eventData)
    {
        clicked = true;
        if (!interactable) return;
        text.color = clickColor;
    }

    public void OnPointerUp(PointerEventData _eventData)
    {
        clicked = false;
        if (!interactable) return;
        onClick.Invoke();
        if (hovered)
            text.color = hoverColor;
        else
            text.color = normalColor;
    }
}