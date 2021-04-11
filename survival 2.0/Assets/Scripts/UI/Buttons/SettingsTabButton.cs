using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsTabButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool interactable = true;
    public TextMeshProUGUI text;

    private SettingsTabMenu tabMenu;

    private bool hovered;
    private bool clicked;
    private bool activated;
    
    private void Awake()
    {
        tabMenu = transform.parent.GetComponent<SettingsTabMenu>();
        text = GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData _eventData)
    {
        if (!interactable) { return; }
        hovered = true;
        if (!clicked)
            text.color = tabMenu.hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!interactable) { return; }
        hovered = false;
        if (!clicked)
            text.color = tabMenu.idleColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!interactable) { return; }
        clicked = true;
        text.color = tabMenu.clickColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!interactable) { return; }
        clicked = false;
        //Click
        tabMenu.TabClick(transform.GetSiblingIndex());
        //
        if (hovered)
            text.color = tabMenu.hoverColor;
        else
            text.color = tabMenu.idleColor;
    }

    public void Activate()
    {
        text.color = tabMenu.activeColor;
        interactable = false;
    }
    
    public void Deactivate()
    {
        text.color = tabMenu.idleColor;
        interactable = true;
    }
}
