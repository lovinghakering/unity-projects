using System;
using MiscUtil.Collections.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BindButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Keybinds keybinds;
    public string bindName;
    public bool reading;
    
    [SerializeField] private GameObject readingKeybindPopUp;
    [SerializeField] private SettingsMenu settingsMenu;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color clickColor;
    [SerializeField] private Color readColor;
    
    private GameObject readingObject;
    private Image image;
    private bool hovered;
    private TextMeshProUGUI text;

    public void Bind(KeyCode _key)
    {
        keybinds.Bind(bindName, _key);
        text.text = FormatKeyName(_key);
    }

    public string FormatKeyName(KeyCode _key)
    {
        string _keyName = _key.ToString();
        if (StringUtils.ContainesUpercase(_keyName, 2))
        {
            int _index = StringUtils.SecondUpercaseLetter(_keyName);

            string _rStr = "";
            for (int i = 0; i < _keyName.Length; i++)
            {
                if (i == _index - 1)
                {
                    _rStr += " ";
                }

                _rStr += _keyName[i];
            }

            return _rStr;
        }
        else
        {
            return _keyName;
        }
    }

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData _eventData)
    {
        hovered = true;
        if (!reading)
            image.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData _eventData)
    {
        if (!reading)
            image.color = normalColor;
    }

    public void OnPointerDown(PointerEventData _eventData)
    {
        if (!reading)
            image.color = clickColor;
    }

    public void OnPointerUp(PointerEventData _eventData)
    {
        if (reading)
        {
            StopReading();
        }
        else
        {
            reading = true;
            image.color = readColor;
            readingObject = Instantiate(readingKeybindPopUp, this.transform.parent.parent.parent.parent.parent.parent.parent.parent);
            readingObject.GetComponent<ReadingKeybindPopUp>().Populate(this);
        }
    }

    public void StopReading()
    {
        reading = false;
        image.color = hovered ? hoverColor : normalColor;
    }

    public void Unbind()
    {
        keybinds.Bind(bindName, KeyCode.None);
        text.text = "None";
    }
}
