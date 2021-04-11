using UnityEngine;
using TMPro;

public class KeybindsList : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveForward;
    [SerializeField] private TextMeshProUGUI moveBackward;
    [SerializeField] private TextMeshProUGUI moveLeft;
    [SerializeField] private TextMeshProUGUI moveRight;
    [SerializeField] private TextMeshProUGUI jump;
    [SerializeField] private TextMeshProUGUI sprint;
    [SerializeField] private TextMeshProUGUI crouch;
    [SerializeField] private TextMeshProUGUI pause;
    [SerializeField] private TextMeshProUGUI debug;
    
    [SerializeField] private Keybinds keybinds;
    
    public void Load(Keybinds _keybinds)
    {
        keybinds = _keybinds;
    
        moveForward.text = FormatKeyName(keybinds.moveForward);
        moveBackward.text = FormatKeyName(keybinds.moveBackward);
        moveLeft.text = FormatKeyName(keybinds.moveLeft);
        moveRight.text = FormatKeyName(keybinds.moveRight);
        jump.text = FormatKeyName(keybinds.jump);
        sprint.text = FormatKeyName(keybinds.sprint);
        crouch.text = FormatKeyName(keybinds.crouch);
        pause.text = FormatKeyName(keybinds.pause);
        debug.text = FormatKeyName(keybinds.debug);
    }

    private string FormatKeyName(KeyCode _key)
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
        
        return _keyName;
    }
}
