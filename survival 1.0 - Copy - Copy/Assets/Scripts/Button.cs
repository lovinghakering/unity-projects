using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    public KeybindsList keybindsList;
    public bool interactable = true;
    public ButtonType type;

    public Color NormalColor;
    public Color ClickColor;
    public Color HoverColor;

    [Header("Keybind")]
    private KeybindsList keybindManager;
    public Color BindClickColor;
    public Color BindNormalColor;
    public bool reading;

    public UnityEvent onClick;

    private Image image;
    private bool isClicked;

    public enum ButtonType
    {
        Static,
        HoverableOnly,
        OnClickOnly,
        ClickHover,
        Keybind
    }

    private void OnDisable()
    {
        onClick.RemoveAllListeners();
    }

    private void Awake()
    {
        image = GetComponent<Image>();
        keybindManager = FindObjectOfType<KeybindsList>();

        if (type == ButtonType.Keybind)
            image.color = BindNormalColor;
        else
            image.color = NormalColor;
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (interactable)
        {
            if (type == ButtonType.ClickHover || type == ButtonType.HoverableOnly)
            {
                image.color = ClickColor;
            }
        }
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (interactable)
        {
            if (type == ButtonType.Keybind)
            {
                image.color = BindClickColor;
                reading = true;
                keybindManager.reading = true;
            }
            else
            {
                onClick.Invoke();
                image.color = NormalColor;
            }
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
                reading = false;
                image.color = BindNormalColor;
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (interactable)
        {
            if (type == ButtonType.ClickHover || type == ButtonType.HoverableOnly)
            {
                if (!isClicked)
                    image.color = HoverColor;
            }
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (interactable)
        {
            if (type == ButtonType.ClickHover || type == ButtonType.HoverableOnly)
            {
                if (!isClicked)
                    image.color = NormalColor;
            }
        }
    }
}
