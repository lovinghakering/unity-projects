using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnbindButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool interactable = true;
    [SerializeField] private Color normalColor = Color.black;
    [SerializeField] private Color hoverColor = Color.black;
    [SerializeField] private Color clickColor = Color.black;
    
    private TextMeshProUGUI text;
    private UnityEvent onClick;
    private bool hovered;
    private bool clicked;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        onClick = new UnityEvent();
        onClick.AddListener(transform.parent.GetComponentInChildren<BindButton>().Unbind);
    }

    public void OnPointerEnter(PointerEventData _eventData)
    {
        hovered = true;
        if (!clicked)
            text.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData _eventData)
    {
        hovered = false;
        if (!clicked)
            text.color = normalColor;
    }

    public void OnPointerDown(PointerEventData _eventData)
    {
        clicked = true;
        text.color = clickColor;
    }

    public void OnPointerUp(PointerEventData _eventData)
    {
        clicked = false;
        if (hovered)
        {
            onClick.Invoke();
            text.color = hoverColor; 
        }
        else
            text.color = normalColor;
    }
}
