using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class KeybindsList : MonoBehaviour
{
    public Scrollbar scrollbar;
    public List<Transform> bindTransforms = new List<Transform>();
    private Dictionary<string, KeyCode> binds = new Dictionary<string, KeyCode>();
    private Dictionary<int, string> intToString = new Dictionary<int, string>();

    public bool reading;

    private void Start()
    {
        scrollbar.value = 0;
        InitializeData();
    }

    public void Bind(int index, KeyCode key)
    {
        bindTransforms[index].GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = key.ToString(); //Set the new key to the text

        string name = bindTransforms[index].GetChild(1).GetComponent<TextMeshProUGUI>().text; 
        binds[name] = StringToKeyCode(bindTransforms[index].GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text);
    }

    public KeyCode GetKey(string keyName)
    {
        KeyCode key;
        binds.TryGetValue(keyName, out key);
        return key;
    }

    private void InitializeData()
    {
        for (int i = 0; i < bindTransforms.Count; i++)
        {
            string name = bindTransforms[i].GetChild(1).GetComponent<TextMeshProUGUI>().text;
            binds.Add(name, StringToKeyCode(bindTransforms[i].GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text));
            intToString.Add(i, name);
        }
    }

    private KeyCode StringToKeyCode(string keyName) => (KeyCode)Enum.Parse(typeof(KeyCode), keyName, true);
}
