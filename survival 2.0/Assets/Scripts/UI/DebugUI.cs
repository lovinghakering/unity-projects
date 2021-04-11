using System;
using TMPro;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    public static DebugUI Instance;
    [SerializeField] private TextMeshProUGUI content;
    [SerializeField] private Keybinds keybinds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(keybinds.debug))
        {
            transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);
        }
    }

    public void Log(string _message)
    {
        content.text += $"{_message}\n";
        content.rectTransform.sizeDelta = new Vector2(content.rectTransform.rect.x, content.preferredHeight);
        content.transform.localPosition += new Vector3(0, 100, 0);
        Debug.Log(_message);
    }
    
    public void LogWarning(string _message)
    {
        Debug.LogWarning(_message);
        content.text += $"<color=yellow>{_message}</color>\n";
        content.rectTransform.sizeDelta = new Vector2(content.rectTransform.rect.x, content.preferredHeight);
        content.transform.localPosition += new Vector3(0, 100, 0);
    }
    
    public void LogError(string _message)
    {
        Debug.LogError(_message);
        content.text += $"<color=red>{_message}</color>\n";
        content.rectTransform.sizeDelta = new Vector2(content.rectTransform.rect.x, content.preferredHeight);
        content.transform.localPosition += new Vector3(0, 100, 0);
    }
}
