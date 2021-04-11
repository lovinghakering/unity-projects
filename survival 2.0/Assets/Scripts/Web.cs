using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class Web : MonoBehaviour
{
    [SerializeField] private PlayerSettings playerSettings; 
    [SerializeField] private Keybinds keybinds;
    [SerializeField] private KeybindsList keybindsList;

    //User authentication
    // ReSharper disable Unity.PerformanceAnalysis
    public IEnumerator RegisterUser(string _username, string _password, string _email, bool _autoSingIn)
    {
        WWWForm _form = new WWWForm();
        _form.AddField("username", _username);
        _form.AddField("password", _password);
        _form.AddField("email", _email);
        
        UnityWebRequest _www = UnityWebRequest.Post("http://178.169.132.89/php/sqlconnect/register.php", _form);

        yield return _www.SendWebRequest();

        if (string.IsNullOrEmpty(_www.downloadHandler.text))
        {
            MainMenuUIManager.Instance.ShowError("Register Error: Connecting to database is taking too long.");
        }
        else
        {
            if (!HandleResult(_www.downloadHandler.text))
            {
                //Successful Register
                playerSettings.LoadDefault();
                //TODO: Load settings UI
                keybinds.LoadDefault();
                keybindsList.Load(keybinds);

                DBManager.username = _username;
        
                if (_autoSingIn)
                {
                    PlayerPrefs.SetString("username", _username);
                    DebugUI.Instance.Log("Auto sing in enabled.");
                }
                MainMenuUIManager.Instance.SuccesfullRegister();
                DebugUI.Instance.Log("Successful Register");
            }
        }
        
        _www.Dispose();
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    public IEnumerator SingInUser(string _username, string _password, bool _autoSingIn)
    {
        WWWForm _form = new WWWForm();
        _form.AddField("username", _username);
        _form.AddField("password", _password);
        
        UnityWebRequest _www = UnityWebRequest.Post("http://178.169.132.89/php/sqlconnect/singin.php", _form);

        yield return _www.SendWebRequest();
        if (string.IsNullOrEmpty(_www.downloadHandler.text))
        {
            MainMenuUIManager.Instance.ShowError("Sing In Error: Connecting to database is taking too long.");
        }
        else
        {
            if (!HandleResult(_www.downloadHandler.text))
            {
                string[] _args = _www.downloadHandler.text.Split('\t');
                playerSettings.settings.FromJson(_args[1]);
                //TODO: Load settings UI
                keybinds.FromJson(_args[2]);
                keybindsList.Load(keybinds);
            
                DBManager.username = _username;
            
                if (_autoSingIn)
                {
                    PlayerPrefs.SetString("username", _username);
                    DebugUI.Instance.Log("Auto sing in enabled.");
                }
                MainMenuUIManager.Instance.SuccesfullSingIn();
                DebugUI.Instance.Log("Successful sing in!");
            }
        }

        _www.Dispose();
    }
    
    //Saving Data
    public IEnumerator SaveSettings()
    {
        List<IMultipartFormSection> _form = new List<IMultipartFormSection> {new MultipartFormDataSection("username", "testuser123"), new MultipartFormDataSection("settings", "settings save test")};

        UnityWebRequest _www = UnityWebRequest.Post("http://178.169.132.89/php/sqlconnect/savesettings.php", _form);

        yield return _www.SendWebRequest();

        _www.Dispose();
    }
    
    public IEnumerator SaveKeybinds()
    {
        List<IMultipartFormSection> _form = new List<IMultipartFormSection> {new MultipartFormDataSection("username", "testuser123"), new MultipartFormDataSection("keybinds", "keybinds save test")};

        UnityWebRequest _www = UnityWebRequest.Post("http://178.169.132.89/php/sqlconnect/savekeybinds.php", _form);

        yield return _www.SendWebRequest();

        _www.Dispose();
    }
    
    //Utils
    private bool HandleResult(string _errorText)
    {
        string[] _errorCodes = _errorText.Split('.');

        if (_errorText[0] == '0')
        {
            return false;
        }

        switch (_errorCodes[0].ToCharArray()[0])
        {
            case '1':
                // Register Error
                switch (_errorCodes[1].ToCharArray()[0])
                {
                    case '1':
                        MainMenuUIManager.Instance.ShowError("Couldn't connect to database.");
                        break;
                    case '2':
                        MainMenuUIManager.Instance.ShowError("Name check query failed! Please try again later.");
                        break;
                    case '3':
                        MainMenuUIManager.Instance.RegisterError("Username already in use.");
                        break;
                    case '4':
                        MainMenuUIManager.Instance.ShowError("User query creation failed! Please try again later.");
                        break;
                }
            break;
            case '2':
                //Sing In Error
                switch (_errorCodes[1].ToCharArray()[0])
                {
                    case '1':
                        MainMenuUIManager.Instance.ShowError("Couldn't connect to database.");
                        break;
                    case '2':
                        MainMenuUIManager.Instance.ShowError("Name check query failed! Please try again later.");
                        break;
                    case '3':
                        MainMenuUIManager.Instance.SingInError("Wrong username/password.");
                        break;
                    case '4':
                        MainMenuUIManager.Instance.SingInError("Wrong username/password.");
                        break;
                }
            break;
            case '3':
                //Save settings Error
                switch (_errorCodes[1].ToCharArray()[0])
                {
                    case '1':
                        MainMenuUIManager.Instance.ShowError("Save Settings Error: Couldn't connect to database.");
                        break;
                    case '2':
                        MainMenuUIManager.Instance.ShowError("Save Settings Error: Save settings query failed! Please try again later.");
                        break;
                }
            break;
            case '4':
                //Save keybinds Error
                switch (_errorCodes[1].ToCharArray()[0])
                {
                    case '1':
                        MainMenuUIManager.Instance.ShowError("Save Keybinds Error: Couldn't connect to database.");
                        break;
                    case '2':
                        MainMenuUIManager.Instance.ShowError("Save Keybinds Error: Save keybinds query failed! Please try again later.");
                        break;
                }
            break;
        }

        return true;
    }
}
