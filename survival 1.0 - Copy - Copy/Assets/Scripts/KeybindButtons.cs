using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeybindButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool interactable = true;

    [Header("Keybind")]
    public KeybindsList keybindManager;
    public Color ClickColor;
    public Color NormalColor;
    public bool reading;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();

        image.color = NormalColor;
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (interactable)
        {
            image.color = ClickColor;
        }
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (interactable)
        {
            image.color = ClickColor;
            reading = true;
            keybindManager.reading = true;
        }
    }

    private void OnGUI()
    {
        Event e = Event.current;

        if (reading == true)
        {
            if (e.type == EventType.KeyDown)
            {
                keybindManager.Bind(transform.parent.GetSiblingIndex(), e.keyCode);
                Debug.Log(1);
                reading = false;
                keybindManager.reading = false;
                image.color = NormalColor;
            }
        }
    }
}
