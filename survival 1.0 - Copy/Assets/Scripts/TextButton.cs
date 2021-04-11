using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Color normalColor = Color.white;
    public Color hoverColor = Color.white;
    public Color clickColor = Color.white;

    private TextMeshProUGUI text;
    private bool clicked;
    private bool hovered;

    public UnityEvent onClick;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnDisable()
    {
        clicked = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        text.color = clickColor;
        clicked = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        clicked = false;

        if (hovered == true)
        {
            text.color = hoverColor;
            onClick.Invoke();
        }
        else
            text.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
        if (clicked == false)
            text.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;

        if (!clicked)
            text.color = normalColor;
    }
}
