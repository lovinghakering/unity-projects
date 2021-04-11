using TMPro;
using UnityEngine;

public class ReadingKeybindPopUp : MonoBehaviour
{
    private BindButton bb;
    [SerializeField] private TextMeshProUGUI content;
    [SerializeField] private ImageButton cancelButton;
    private Keybinds keybinds;
    public bool pressedUsedKeyOnce;

    public void Populate(BindButton _bb)
    {
        bb = _bb;
        content.text += _bb.bindName + ".";
        keybinds = _bb.keybinds;
    }
    
    private void OnGUI()
    {
        Event e = Event.current;

        if (e.type == EventType.KeyDown)
        {
            if (e.keyCode == KeyCode.None) { return; }

            if (bb.FormatKeyName(e.keyCode) == bb.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text)
            {
                Cancel();
            }
            
            if (keybinds.IsKeyUsedOrNone(e.keyCode))
            {
                if (!pressedUsedKeyOnce)
                {
                    content.text = $"\"{bb.FormatKeyName(e.keyCode)}\" is already used, press it again to use it for both binds or press another key.";
                    pressedUsedKeyOnce = true;
                }
                else
                {
                    bb.Bind(e.keyCode);
                    Cancel();
                }
            }
            else
            {
                bb.Bind(e.keyCode);
                Cancel();
            }
        }
    }
    
    public void Cancel()
    {
        bb.StopReading();
        Destroy(gameObject);
    }
}
