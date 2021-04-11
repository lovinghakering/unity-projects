using UnityEngine;

public class SavePopUp : MonoBehaviour
{
    private SettingsMenu settingsMenu;
    
    public void Populate(SettingsMenu _settingsMenu)
    {
        settingsMenu = _settingsMenu;
    }

    public void Save()
    {
        settingsMenu.SaveClose();
        Destroy(this.gameObject);
    }

    public void DontSave()
    {
        settingsMenu.DontSaveClose();
        Destroy(this.gameObject);
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
