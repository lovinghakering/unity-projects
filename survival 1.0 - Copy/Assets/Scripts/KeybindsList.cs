using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeybindsList : MonoBehaviour
{
    public Keys keys;
    public Scrollbar scrollbar;
    private List<Transform> bindTransforms = new List<Transform>();
    public Transform parentOfBindTransforms;

    [HideInInspector]
    public bool reading;
    private void Start()
    {
        InitializeData();
        UpdateContent();

        scrollbar.value = 1; //Set scrollbar to start
    }

    private void InitializeData()
    {
        for (int i = 0; i < parentOfBindTransforms.childCount; i++)
        {
            bindTransforms.Add(parentOfBindTransforms.GetChild(i));
        }
    }

    private void UpdateContent()
    {
        float height = (parentOfBindTransforms.childCount * 125 + (parentOfBindTransforms.childCount - 2) * 25) - 15; //Calculate the new height for the parent object ("content")
        parentOfBindTransforms.GetComponent<RectTransform>().sizeDelta = new Vector2(1500, height); //Set the new height to the parent object ("content")
    }

    public void UpdateKeybinds()
    {
        for (int i = 0; i < bindTransforms.Count; i++)
        {
            switch (bindTransforms[i].GetChild(1).GetComponent<TextMeshProUGUI>().text)
            {
                case "Move Forward":
                    bindTransforms[i].GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = FormatKey(keys.MoveForward.ToString());
                    break;
                case "Move Backward":
                    bindTransforms[i].GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = FormatKey(keys.MoveBackward.ToString());
                    break;
                case "Move Left":
                    bindTransforms[i].GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = FormatKey(keys.MoveLeft.ToString());
                    break;
                case "Move Right":
                    bindTransforms[i].GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = FormatKey(keys.MoveRight.ToString());
                    break;
                case "Jump":
                    bindTransforms[i].GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = FormatKey(keys.MoveRight.ToString());
                    break;
                case "Sprint":
                    bindTransforms[i].GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = FormatKey(keys.Sprint.ToString());
                    break;
                case "Crouch":
                    bindTransforms[i].GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = FormatKey(keys.Crouch.ToString());
                    break;
                case "Interact":
                    bindTransforms[i].GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = FormatKey(keys.Interact.ToString());
                    break;
                case "Inventory":
                    bindTransforms[i].GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = FormatKey(keys.Inventory.ToString());
                    break;
                case "Pause":
                    bindTransforms[i].GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = FormatKey(keys.Pause.ToString());
                    break;
                case "Debug":
                    bindTransforms[i].GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = FormatKey(keys.Debug.ToString());
                    break;
                case "Toggle Cursor":
                    bindTransforms[i].GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = FormatKey(keys.ToggleCursor.ToString());
                    break;
            }
        }
    }

    public void Bind(int index, KeyCode key)
    {
        bindTransforms[index].GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = key.ToString(); //Update the key text
        string name = bindTransforms[index].GetChild(1).GetComponent<TextMeshProUGUI>().text; //Get the keybinds name
        keys.Bind(name, key); //Bind it to the scriptable object "Keys"
    }

    private string FormatKey(string key)
    {
        if (StringUtils.ContainesUpercase(key, 2))
        {
            int index = StringUtils.SecondUpercaseLetter(key);

            string rStr = "";
            for (int i = 0; i < key.Length; i++)
            {
                if (i == index - 1)
                {
                    rStr += " ";
                }

                rStr += key[i];
            }

            return rStr;
        }
        else
        {
            return key;
        }
    }
}
