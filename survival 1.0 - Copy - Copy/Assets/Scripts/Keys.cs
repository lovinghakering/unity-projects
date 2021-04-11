using UnityEngine;

[CreateAssetMenu(fileName = "Keys", menuName = "ScriptableObjects/Keybinds", order = 1)]
public class Keys : ScriptableObject
{
    public KeyCode MoveForward = KeyCode.Escape;
    public KeyCode MoveBackward = KeyCode.S;
    public KeyCode MoveLeft = KeyCode.A;
    public KeyCode MoveRight = KeyCode.D;
    public KeyCode Jump = KeyCode.Space;
    public KeyCode Sprint = KeyCode.LeftShift;
    public KeyCode Crouch = KeyCode.LeftControl;
    public KeyCode Interact = KeyCode.E;
    public KeyCode Inventory = KeyCode.Tab;
    public KeyCode Pause = KeyCode.Escape;
    public KeyCode Debug = KeyCode.F3;
    public KeyCode ToggleCursor = KeyCode.F1;

    private class Keybinds
    {
        public KeyCode MoveForward;
        public KeyCode MoveBackward;
        public KeyCode MoveLeft;
        public KeyCode MoveRight;
        public KeyCode Jump;
        public KeyCode Sprint;
        public KeyCode Crouch;
        public KeyCode Interact;
        public KeyCode Inventory;
        public KeyCode Pause;
        public KeyCode Debug;
        public KeyCode ToggleCursor;
    }

    public void Bind(string keyName, KeyCode key)
    {
        switch (keyName)
        {
            case "Move Forward":
                MoveForward = key;
                break;
            case "Move Backward":
                MoveBackward = key;
                break;
            case "Move Left":
                MoveLeft = key;
                break;
            case "Move Right":
                MoveRight = key;
                break;
            case "Jump":
                Jump = key;
                break;
            case "Sprint":
                Sprint = key;
                break;
            case "Crouch":
                Crouch = key;
                break;
            case "Interact":
                Interact = key;
                break;
            case "Inventory":
                Inventory = key;
                break;
            case "Pause":
                Pause = key;
                break;
            case "Debug":
                Debug = key;
                break;
            case "Toggle Cursor":
                ToggleCursor = key;
                break;
        }
    }

    public float GetHorizontalAxis()
    {
        float result = 0;
        if (Input.GetKey(MoveLeft))
        {
            result -= 1;
        }
        else if (Input.GetKey(MoveRight))
        {
            result += 1;
        }
        return result;
    }

    public int GetVerticalAxis()
    {
        int result = 0;
        if (Input.GetKey(MoveBackward))
        {
            result -= 1;
        }
        else if (Input.GetKey(MoveForward))
        {
            result += 1;
        }
        return result;
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this); //Return the keybinds as json string
    }

    public void FromJson(string json)
    {
        Keybinds keybinds = JsonUtility.FromJson<Keybinds>(json); //Converts the givven json to Keybinds class

        MoveForward = keybinds.MoveForward; //Asigns the loaded keybinds to the scriptable object
        MoveBackward = keybinds.MoveBackward;
        MoveLeft = keybinds.MoveLeft;
        MoveRight = keybinds.MoveRight;
        Jump = keybinds.Jump;
        Sprint = keybinds.Sprint;
        Crouch = keybinds.Crouch;
        Interact = keybinds.Interact;
        Inventory = keybinds.Inventory;
        Pause = keybinds.Pause;
        Debug = keybinds.Debug;
        ToggleCursor = keybinds.ToggleCursor;

        MenuUIManager.instance.keybindsList.UpdateKeybinds();
        Logger.Log("Loadded keybinds!");
    }
}
