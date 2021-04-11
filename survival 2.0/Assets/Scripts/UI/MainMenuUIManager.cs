using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    public static MainMenuUIManager Instance;
    
    [Header("Prefabs")]
    [SerializeField] private GameObject errorPrefab;
    
    [Header("Panels")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject playPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject registerPanel;
    [SerializeField] private GameObject singInPanel;
    
    [Header("Sing In Menu")]
    [SerializeField] private TextMeshProUGUI singInErrorText;
    [SerializeField] private TMP_InputField singInUsernameInputField;
    [SerializeField] private TMP_InputField singInPasswordInputField;
    [SerializeField] private Toggle singInRememberMeToggle;
    
    [Header("Register Menu")]
    [SerializeField] private TextMeshProUGUI registerErrorText;
    [SerializeField] private TMP_InputField registerUsernameInputField;
    [SerializeField] private TMP_InputField registerPasswordInputField;
    [SerializeField] private TMP_InputField registerPasswordConformInputField;
    [SerializeField] private TMP_InputField registerEmailInputField;
    [SerializeField] private Toggle registerRememberMeToggle;

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

    private void Start()
    {
        if (PlayerPrefs.HasKey("username"))
        {
            DBManager.username = PlayerPrefs.GetString("username");
            DebugUI.Instance.Log("Auto singed in.");
        }
        else
        {
            startPanel.SetActive(false);
            singInPanel.SetActive(true);
        }
    }

    public void ShowError(string _content)
    {
        Instantiate(errorPrefab, this.transform).GetComponent<ErrorPupUp>().Populate(_content);
        DebugUI.Instance.LogError(_content);
    }

    #region startPanel
    public void Settings()
    {
        settingsPanel.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
    #endregion

    #region playPanel
    public void PlayToggle()
    {
        playPanel.SetActive(!playPanel.activeSelf);
    }
    #endregion

    public void SignOut()
    {
        DBManager.SignOut();
        startPanel.SetActive(false);
        singInPanel.SetActive(true);
    }
    
    #region SingInMenu
    public void SingIn()
    {
        string _username = singInUsernameInputField.text;
        string _password = singInPasswordInputField.text;

        if (_username.Length < 8)
        {
            DebugUI.Instance.LogError("Username should be 8 characters or more!");
            singInErrorText.text = "Username should be 8 characters or more!";
        }
        else if (_password.Length < 8)
        {
            DebugUI.Instance.LogError("Password should be 8 characters or more!");
            singInErrorText.text = "Password should be 8 characters or more!";
        }
        else if (!StringUtils.ContainesDigit(_password))
        {
            DebugUI.Instance.LogError("Password should contain at least one digit!");
            singInErrorText.text = "Password should contain at least one digit!";
        }
        else if (!StringUtils.ContainesLetter(_password))
        {
            DebugUI.Instance.LogError("Password should contain at least one letter!");
            singInErrorText.text = "Password should contain at least one letter!";
        }
        else
        {
            StartCoroutine(GetComponent<Web>().SingInUser(_username, _password, singInRememberMeToggle.isOn));
        }
    }

    public void DontHaveAccount()
    {
        singInPanel.SetActive(false);
        registerPanel.SetActive(true);
    }
    
    public void SuccesfullSingIn()
    {
        singInPanel.SetActive(false);
        startPanel.SetActive(true);
    }
    
    public void SingInError(string _text)
    {
        singInErrorText.text = _text;
    }
    #endregion
    
    #region RegisterMenu
    public void Regiser()
    {
        string _username = registerUsernameInputField.text;
        string _password = registerPasswordInputField.text;
        string _passwordConfirm = registerPasswordConformInputField.text;
        string _email = registerEmailInputField.text;

        if (_username.Length < 8)
        {
            DebugUI.Instance.LogError("Username should be 8 characters or more!");
            registerErrorText.text = "Username should be 8 characters or more!";
        }
        else if (_password.Length < 8)
        {
            DebugUI.Instance.LogError("Password should be 8 characters or more!");
            registerErrorText.text = "Password should be 8 characters or more!";
        }
        else if (!StringUtils.ContainesDigit(_password))
        {
            DebugUI.Instance.LogError("Password should contain at least one digit!");
            registerErrorText.text = "Password should contain at least one digit!";
        }
        else if (!StringUtils.ContainesLetter(_password))
        {
            DebugUI.Instance.LogError("Password should contain at least one letter!");
            registerErrorText.text = "Password should contain at least one letter!";
        }
        else if (_password != _passwordConfirm)
        {
            DebugUI.Instance.LogError("Passwords doesn't match!");
            registerErrorText.text = "Passwords doesn't match!";
        }
        else if (!StringUtils.IsValidEmail(_email))
        {
            DebugUI.Instance.LogError("Invalid email!");
            registerErrorText.text = "Invalid email!";
        }
        else
        {
            StartCoroutine(GetComponent<Web>().RegisterUser(_username, _password, _email, registerRememberMeToggle.isOn));
        }
    }

    public void AlreadyHaveAccount()
    {
        registerPanel.SetActive(false);
        singInPanel.SetActive(true);
    }
    
    public void SuccesfullRegister()
    {
        registerPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    public void RegisterError(string _text)
    {
        registerErrorText.text = _text;
    }
    #endregion
}
