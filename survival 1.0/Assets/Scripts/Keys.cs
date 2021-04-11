using UnityEngine;

[CreateAssetMenu(fileName = "Keys", menuName = "ScriptableObjects/Keybinds", order = 1)]
public class Keys : ScriptableObject
{
    public KeyCode Forward = KeyCode.Escape;
    public KeyCode Backwards = KeyCode.S;
    public KeyCode Left = KeyCode.A;
    public KeyCode Right = KeyCode.D;
    public KeyCode Sprint = KeyCode.LeftShift;
    public KeyCode Crouch = KeyCode.LeftControl;
    public KeyCode Interact = KeyCode.E;
    public KeyCode Pause = KeyCode.Escape;

    private class Keybinds
    {
        public KeyCode Forward;
        public KeyCode Backwards;
        public KeyCode Left;
        public KeyCode Right;
        public KeyCode Sprint;
        public KeyCode Crouch;
        public KeyCode Interact;
        public KeyCode Pause;
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void FromJson(string json)
    {
        Keybinds keybinds = JsonUtility.FromJson<Keybinds>(json);
        
        Forward = keybinds.Forward;
        Backwards = keybinds.Backwards;
        Left = keybinds.Left;
        Right = keybinds.Right;
        Sprint = keybinds.Sprint;
        Crouch = keybinds.Crouch;
        Interact = keybinds.Interact;
        Pause = keybinds.Pause;

        Main.instance.logger.Log("Loadded keybinds!");
    }
}
