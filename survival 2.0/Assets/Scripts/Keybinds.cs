using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Keybinds", menuName = "ScriptableObjects/Keybinds")]
public class Keybinds : ScriptableObject
{
    public KeyCode moveForward = KeyCode.W;
    public KeyCode moveBackward = KeyCode.S;
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode jump = KeyCode.Space;
    public KeyCode sprint = KeyCode.LeftShift;
    public KeyCode crouch = KeyCode.LeftControl;
    public KeyCode pause = KeyCode.Escape;
    public KeyCode debug = KeyCode.F3;

    public void Bind(string _name, KeyCode _key)
    {
        switch (_name)
        {
            case "Move Forward":
                moveForward = _key;
                break;
            case "Move Backward":
                moveBackward = _key;
                break;
            case "Move Left":
                moveLeft = _key;
                break;
            case "Move Right":
                moveRight = _key;
                break;
            case "Jump":
                jump = _key;
                break;
            case "Sprint":
                sprint = _key;
                break;
            case "Crouch":
                crouch = _key;
                break;
            case "Pause":
                pause = _key;
                break;
            case "Debug":
                debug = _key;
                break;
            default:
                DebugUI.Instance.LogError("Key bind name does not exist!");
                break;
        }
    }
    
    public void LoadDefault()
    {
        moveForward = KeyCode.W;
        moveBackward = KeyCode.S;
        moveLeft = KeyCode.A;
        moveRight = KeyCode.D;
        jump = KeyCode.Space;
        sprint = KeyCode.LeftShift;
        crouch = KeyCode.LeftControl;
        pause = KeyCode.Escape;
        debug = KeyCode.F3;
    }
    
    public bool IsKeyUsedOrNone(KeyCode _key)
    {
        if (_key == KeyCode.None)
        {
            return true;
        }
        
        List<KeyCode> _keyBinds = new List<KeyCode> {moveForward, moveBackward, moveLeft, moveRight, jump, sprint, crouch, pause, debug};
        
        for (int i = 0; i < _keyBinds.Count; i++)
        {
            if (_key == _keyBinds[i])
            {
                return true;
            }
        }

        return false;
    }
    
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void FromJson(string _json)
    {
        JsonUtility.FromJsonOverwrite(_json, this);
    }
}