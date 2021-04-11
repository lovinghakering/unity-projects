using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System;

public class MenuUIManager : MonoBehaviour
{
    public static MenuUIManager instance;

    public KeybindsList keybindsList;
    public GameObject redirectPopUp;

    [Header("Start")]
    public GameObject startMenu;
    public Button[] startButtons;
    public Button playBtn;
    public Button settingsBtn;
    public Button exitBtn;
    public Button logOutBtn;
    public Button accountBtn;

    [Header("Settings")]
    public GameObject settingsMenu;
    public Slider mouseSensXSlider;
    public Slider mouseSensYSlider;
    public Slider masterVolumeSlider;
    public TMP_InputField mouseSensXInput;
    public TMP_InputField mouseSensYInput;
    public TMP_InputField masterVolumeInput;
    public TMP_Dropdown resolutonDropdown;
    public Toggle fullscreenToggle;
    public Button audioBtn;
    public Button graphicsBtn;
    public Button mouseBtn;
    public Button keybindsBtn;
    public TextButton settingsCloseBtn;

    [Header("Mouse Tab")]
    public GameObject mouseMenu;

    [Header("Audio Tab")]
    public GameObject audioMenu;

    [Header("Graphics Tab")]
    public GameObject graphicsMenu;

    [Header("Keybinds Tab")]
    public GameObject keybindsMenu;

    [Header("Join")]
    public GameObject joinMenu;
    public TMP_InputField serverAddresIpInput;
    public Button joinBtn;
    public Button joinBackBtn;

    [Header("Register")]
    public GameObject registerMenu;
    public TextMeshProUGUI registerErrorText;
    public TMP_InputField registerUsernameInput;
    public TMP_InputField registerPasswordInput;
    public TMP_InputField registerPasswordConfirmInput;
    public TMP_InputField registerEmailInput;
    public Toggle registerAutoSingInToggle;
    public Button registerBtn;
    public Button registerBackBtn;

    [Header("Login")]
    public GameObject loginMenu;
    public Button[] loginButtons;
    public TextMeshProUGUI loginErrorText;
    public TMP_InputField loginUsernameInput;
    public TMP_InputField loginPasswordInput;
    public Toggle loginAutoSingInToggle;
    public Button loginRegisterBtn;
    public Button loginBtn;

    public int selectedBtn = 0;
    public int currentMenu = 0;

    Resolution[] resolutionsArray;
    List<Resolution> resolutionsList;
    GameObject currentTab;

    private void Start()
    {
        SceneManager.LoadSceneAsync("DebugUI", LoadSceneMode.Additive);

        SubscribeListeners();

        resolutionsArray = Screen.resolutions;
        resolutionsList = new List<Resolution>(resolutionsArray);
        resolutionsList.Reverse();

        resolutonDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutionsArray.Length; i++)
        {
            options.Add(resolutionsArray[i].width + " x " + resolutionsArray[i].height);
        }

        List<string> options2 = options.Distinct().ToList();
        options2.Reverse();

        options.Reverse();
        for (int i = 0; i < options2.Count; i++)
        {
            if (options2[i] == Screen.currentResolution.width + " x " + Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutonDropdown.AddOptions(options2);
        resolutonDropdown.value = currentResolutionIndex;
        resolutonDropdown.RefreshShownValue();

        currentTab = audioMenu;

        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("username")))
        {
            DBManager.username = PlayerPrefs.GetString("username");
            StartCoroutine(Main.instance.Web.LoadSettings());
            StartCoroutine(Main.instance.Web.LoadKeybinds());
            Logger.Log("Auto singed in.");
        }

        if (!DBManager.isLoggedIn)
        {
            loginMenu.SetActive(true);
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }

        startMenu.SetActive(true);
    }

    public void SubscribeListeners()
    {
        ////Buttons
        //settingsCloseBtn.onClick.AddListener(SettingsBack);
        //audioBtn.onClick.AddListener(AudioTabClick);
        //graphicsBtn.onClick.AddListener(GraphicsTabClick);
        //mouseBtn.onClick.AddListener(MouseTabClick);
        //keybindsBtn.onClick.AddListener(KeybindsTabClick);
        //playBtn.onClick.AddListener(Play);
        //settingsBtn.onClick.AddListener(SettingsMenu);
        //exitBtn.onClick.AddListener(Exit);
        //accountBtn.onClick.AddListener(RedirectToAccountSite);
        //logOutBtn.onClick.AddListener(DBManager.LogOut);
        //joinBtn.onClick.AddListener(Join);
        //joinBackBtn.onClick.AddListener(JoinBack);
        //registerBtn.onClick.AddListener(TryRegister);
        //registerBackBtn.onClick.AddListener(RegisterBack);
        //loginBtn.onClick.AddListener(TryLogin);
        //loginRegisterBtn.onClick.AddListener(LoginRegister);
        ////Sliders
        //mouseSensXSlider.onValueChanged.AddListener(MouseSliderXValueChanged);
        //mouseSensYSlider.onValueChanged.AddListener(MouseSliderYValueChanged);
        //masterVolumeSlider.onValueChanged.AddListener(masterVolumeSliderValueChanged);
        ////Toggles
        //fullscreenToggle.onValueChanged.AddListener(FulllscreenToggleValueChanged);
        ////Input fields
        //mouseSensXInput.onValueChanged.AddListener(mouseSensXInputValueChanged);
        //mouseSensYInput.onValueChanged.AddListener(mouseSensYInputValueChanged);
        //masterVolumeInput.onEndEdit.AddListener(masterVolumeInputEndEdit);
        ////Dropdowns
        //resolutonDropdown.onValueChanged.AddListener(ChangeResolution);
    }

    #region Start Menu
    public void Play()
    {
        Debug.Log("Pressed play");
        startMenu.SetActive(false);
        joinMenu.SetActive(true);
    }

    public void SettingsMenu()
    {
        startMenu.SetActive(false);
        settingsMenu.SetActive(true);
        audioMenu.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void RedirectToAccountSite()
    {
        Instantiate(redirectPopUp, transform).GetComponent<RedirectPopUp>().Populate("https://178.169.132.89/");
    }
    #endregion

    #region Join Menu
    public void Join()
    {
        string[] addres = serverAddresIpInput.text.Split(':');
        Main.instance.serverIp = addres[0];
        Main.instance.serverPort = int.Parse(addres[1]);
        SceneManager.UnloadSceneAsync("StartMenu");
        SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
    }

    public void JoinBack()
    {
        joinMenu.SetActive(false);
        startMenu.SetActive(true);
    }
    #endregion

    #region Settings Menu

    class Settings
    {
        public float mouseX, mouseY, masterVolume;
        public bool fullscreen;

        public Settings(float _mouseX, float _mouseY, float _masterVolume, bool _fullscreen)
        {
            mouseX = _mouseX;
            mouseY = _mouseY;
            masterVolume = _masterVolume;
            fullscreen = _fullscreen;
        }
    }

    #region Events

    #region OnClick
    public void MouseTabClick()
    {
        currentTab.SetActive(false);
        mouseMenu.SetActive(true);
        currentTab = mouseMenu;
    }

    public void GraphicsTabClick()
    {
        currentTab.SetActive(false);
        graphicsMenu.SetActive(true);
        currentTab = graphicsMenu;
    }

    public void KeybindsTabClick()
    {
        currentTab.SetActive(false);
        keybindsMenu.SetActive(true);
        currentTab = keybindsMenu;
    }

    public void AudioTabClick()
    {
        currentTab.SetActive(false);
        audioMenu.SetActive(true);
        currentTab = audioMenu;
    }

    public void SettingsBack()
    {
        SaveSettings();
        settingsMenu.SetActive(false);
        startMenu.SetActive(true);
    }

    public void SaveSettings()
    {
        Settings settings = new Settings(mouseSensXSlider.value, mouseSensYSlider.value, masterVolumeSlider.value, fullscreenToggle.isOn);
        string jsonString = JsonUtility.ToJson(settings);
        StartCoroutine(Main.instance.Web.SaveSettings(jsonString));
    }
    #endregion

    public void LoadSettings(string jsonString)
    {
        Settings settings = JsonUtility.FromJson<Settings>(jsonString);
        mouseSensXSlider.value = settings.mouseX;
        mouseSensXInput.text = settings.mouseX.ToString();
        mouseSensYSlider.value = settings.mouseY;
        mouseSensYInput.text = settings.mouseY.ToString();
        masterVolumeSlider.value = settings.masterVolume;
        masterVolumeInput.text = (settings.masterVolume + 80).ToString();

        DBManager.mouseSensitivityX = settings.mouseX;
        DBManager.mouseSensitivityY = settings.mouseY;

        Logger.Log("Loadded settings!");
    }

    public string SettingsToJson()
    {
        Settings settings = new Settings(mouseSensXSlider.value, mouseSensYSlider.value, masterVolumeSlider.value, fullscreenToggle.isOn);
        return JsonUtility.ToJson(settings);
    }

    public void ChangeResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutionsList[resolutionIndex].width, resolutionsList[resolutionIndex].height, Screen.fullScreen);
    }
    #endregion
    #region Event Listeners
    #region Sliders
    public void MouseSliderXValueChanged(float value)
    {
        mouseSensXInput.text = value.ToString();
        DBManager.mouseSensitivityX = value;
    }

    public void MouseSliderYValueChanged(float value)
    {
        mouseSensYInput.text = value.ToString();
        DBManager.mouseSensitivityY = value;
    }

    public void masterVolumeSliderValueChanged(float value)
    {
        masterVolumeInput.text = (value += 80).ToString();
    }
    #endregion
    #region InputFields
    public void mouseSensXInputValueChanged(string value)
    {
        if (value.Length > 0)
        {
            mouseSensXSlider.value = int.Parse(value);
            DBManager.mouseSensitivityX = int.Parse(value);
        }
    }

    public void mouseSensYInputValueChanged(string value)
    {
        if (value.Length > 0)
        {
            mouseSensYSlider.value = int.Parse(value);
            DBManager.mouseSensitivityY = int.Parse(value);
        }
    }

    public void masterVolumeInputEndEdit(string value)
    {
        if (value.Length > 0)
            masterVolumeSlider.value = int.Parse(value) - 80;
    }

    #endregion
    #region Toggles
    public void FulllscreenToggleValueChanged(bool value)
    {
        Screen.fullScreen = value;
    }
    #endregion
    #endregion

    #endregion

    #region Login Menu
    public void LoginError(string msg)
    {
        loginErrorText.text = msg;
    }

    public void LogOut()
    {
        startMenu.SetActive(false);
        loginMenu.SetActive(true);
    }

    public void LoginRegister()
    {
        loginMenu.SetActive(false);
        registerMenu.SetActive(true);
    }

    public void SuccessfulLogin()
    {
        loginMenu.SetActive(false);
        startMenu.SetActive(true);
    }

    public void TryLogin()
    {
        StartCoroutine(Main.instance.Web.LoginUser(loginUsernameInput.text, loginPasswordInput.text, loginAutoSingInToggle.isOn));
    }
    #endregion

    #region Register Menu
    public void RegisterError(string msg)
    {
        registerErrorText.text = msg;
    }

    public void SuccessfulRegister()
    {
        registerMenu.SetActive(false);
        startMenu.SetActive(true);
    }

    public void RegisterBack()
    {
        registerMenu.SetActive(false);
        loginMenu.SetActive(true);
    }

    public void TryRegister()
    {
        if (registerUsernameInput.text.Length < 7)
        {
            registerErrorText.text = "Your username should be atlest 8 characters long!";
        }
        else if (registerPasswordInput.text.Length < 7)
        {
            registerErrorText.text = "Your password should be atlest 8 characters long!";
        }
        else if (registerPasswordInput.text != registerPasswordConfirmInput.text)
        {
            registerErrorText.text = "Passwords don't match!";
        }
        else if (!StringUtils.ContainesDigit(registerPasswordInput.text))
        {
            registerErrorText.text = "Your password should contain atleast one digit!";
        }
        else if (!StringUtils.ContainesLower(registerPasswordInput.text))
        {
            registerErrorText.text = "Your password should contain atleast one lowercase letter!";
        }
        else if (!StringUtils.ContainesUpercase(registerPasswordInput.text))
        {
            registerErrorText.text = "Your password should contain atleast one uperrcase letter!";
        }
        else if (!StringUtils.ValidEmail(registerEmailInput.text))
        {
            registerErrorText.text = "Your email is invalid!";
        }
        else
        {
            StartCoroutine(Main.instance.Web.RegisterUser(registerUsernameInput.text, registerPasswordInput.text, registerEmailInput.text));
        }
    }
    #endregion
}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
    //    {

    //    }

    //    if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
    //    {

    //    }

    //    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
    //    {

    //    }
    //}

    //private void SelectButton(int a)
    //{
    //    int b = selectedBtn + a;
    //    selectedBtn = Mathf.Clamp(b, 0, buttons.Length - 1);


    //    switch (currentMenu)
    //    {
    //        case 0:
    //            //Start menu

    //            break;
    //        case 1:
    //            //Settings menu

    //            break;
    //        case 2:
    //            //Keybinds menu

    //            break;
    //        case 3:
    //            //Login menu

    //            break;
    //        case 4:
    //            //Register menu

    //            break;
    //        case 5:
    //            //Join menu

    //            break;
    //    }
    //}

    //private void UdpateMenu()
    //{
    //    switch (currentMenu)
    //    {
    //        case 0:
    //            //Start menu
    //            for (int i = 0; i < startButtons.Length; i++)
    //            {
    //                if (i == selectedBtn)
    //                {
    //                    //startButtons[i].GetComponent<MeshRenderer>().material = selectedMaterial;
    //                    Debug.Log(startButtons[i].name);
    //                }
    //                else
    //                {
    //                    //startButtons[i].GetComponent<MeshRenderer>().material = idleMaterial;
    //                }
    //            }
    //            break;
    //        case 1:
    //            //Settings menu
    //            for (int i = 0; i < startButtons.Length; i++)
    //            {
    //                if (i == selectedBtn)
    //                {
    //                    //startButtons[i].GetComponent<MeshRenderer>().material = selectedMaterial;
    //                    Debug.Log(startButtons[i].name);
    //                }
    //                else
    //                {
    //                    //startButtons[i].GetComponent<MeshRenderer>().material = idleMaterial;
    //                }
    //            }
    //            break;
    //        case 2:
    //            //Keybinds menu
    //            for (int i = 0; i < startButtons.Length; i++)
    //            {
    //                if (i == selectedBtn)
    //                {
    //                    //startButtons[i].GetComponent<MeshRenderer>().material = selectedMaterial;
    //                    Debug.Log(startButtons[i].name);
    //                }
    //                else
    //                {
    //                    //startButtons[i].GetComponent<MeshRenderer>().material = idleMaterial;
    //                }
    //            }
    //            break;
    //        case 3:
    //            //Login menu
    //            for (int i = 0; i < startButtons.Length; i++)
    //            {
    //                if (i == selectedBtn)
    //                {
    //                    //startButtons[i].GetComponent<MeshRenderer>().material = selectedMaterial;
    //                    Debug.Log(startButtons[i].name);
    //                }
    //                else
    //                {
    //                    //startButtons[i].GetComponent<MeshRenderer>().material = idleMaterial;
    //                }
    //            }
    //            break;
    //        case 4:
    //            //Register menu
    //            for (int i = 0; i < startButtons.Length; i++)
    //            {
    //                if (i == selectedBtn)
    //                {
    //                    //startButtons[i].GetComponent<MeshRenderer>().material = selectedMaterial;
    //                    Debug.Log(startButtons[i].name);
    //                }
    //                else
    //                {
    //                    //startButtons[i].GetComponent<MeshRenderer>().material = idleMaterial;
    //                }
    //            }
    //            break;
    //        case 5:
    //            //Join menu
    //            for (int i = 0; i < startButtons.Length; i++)
    //            {
    //                if (i == selectedBtn)
    //                {
    //                    //startButtons[i].GetComponent<MeshRenderer>().material = selectedMaterial;
    //                    Debug.Log(startButtons[i].name);
    //                }
    //                else
    //                {
    //                    //startButtons[i].GetComponent<MeshRenderer>().material = idleMaterial;
    //                }
    //            }
    //            break;
    //    }

    //}