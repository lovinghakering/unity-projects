using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MenuUIManager : MonoBehaviour
{
    [Header("Start")]
    public GameObject StartMenu;
    public Button PlayBtn;
    public Button OptionsBtn;
    public Button ExitBtn;

    [Header("Options")]
    public GameObject OptionsMenu;
    public Slider MouseSensXSlider;
    public Slider MouseSensYSlider;
    public Slider MasterVolumeSlider;
    public TextMeshProUGUI MouseSensXValue;
    public TextMeshProUGUI MouseSensYValue;
    public TextMeshProUGUI MasterVolumeValue;
    public Toggle FullscreenToggle;
    public Button KeybindsBtn;
    public Button OptionsBackBtn;

    [Header("Keybinds")]
    public GameObject KeybindsMenu;
    public Button KeybindsBackBtn;

    [Header("Join")]
    public GameObject JoinMenu;

    [Header("Register")]
    public GameObject RegisterMenu;
    public TextMeshProUGUI RegisterErrorText;
    public TMP_InputField RegisterUsernameInput;
    public TMP_InputField RegisterPasswordInput;
    public TMP_InputField RegisterPasswordConfirmInput;
    public TMP_InputField RegisterEmailInput;
    public Toggle RegisterAutoSingInToggle;
    public Button RegisterBtn;

    [Header("Login")]
    public GameObject LoginMenu;
    public TextMeshProUGUI LoginErrorText;
    public TMP_InputField LoginUsernameInput;
    public TMP_InputField LoginPasswordInput;
    public Toggle LoginAutoSingInToggle;
    public Button LoginBtn;

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

    private void Start()
    {
        BindButtons();
    }

    private void Update()
    {
        if (Input.GetKeyDown(Main.instance.Keys.Interact))
        {
            Debug.Log("Test");
        }
    }

    private IEnumerator SaveSettings()
    {
        Settings settings = new Settings(MouseSensXSlider.value, MouseSensYSlider.value, MasterVolumeSlider.value, FullscreenToggle.isOn);

        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("username", "testuser123"));
        form.Add(new MultipartFormDataSection("settings", JsonUtility.ToJson(settings)));

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/php/sqlconnect/savesettings.php", form);

        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
    }

    private void BindButtons()
    {
        //Buttons
        PlayBtn.onClick.AddListener(Play);
        OptionsBtn.onClick.AddListener(Options);
        ExitBtn.onClick.AddListener(Exit);
        RegisterBtn.onClick.AddListener(TryRegister);
        LoginBtn.onClick.AddListener(TryLogin);
        OptionsBackBtn.onClick.AddListener(OptionsBack);
        KeybindsBtn.onClick.AddListener(Keybinds);

        //Sliders
        MouseSensXSlider.onValueChanged.AddListener(MouseSliderXValueChanged);
        MouseSensYSlider.onValueChanged.AddListener(MouseSliderYValueChanged);
        MasterVolumeSlider.onValueChanged.AddListener(MasterVolumeSliderValueChanged);
        FullscreenToggle.onValueChanged.AddListener(FulllscreenToggleValueChanged);
    }

    //Options
    //Sliders
    private void MouseSliderXValueChanged(float value)
    {
        MouseSensXValue.text = value.ToString();
    }

    private void MouseSliderYValueChanged(float value)
    {
        MouseSensYValue.text = value.ToString();
    }

    private void MasterVolumeSliderValueChanged(float value)
    {
        MasterVolumeValue.text = (value += 20).ToString();
    }

    //Toggles
    private void FulllscreenToggleValueChanged(bool value)
    {
        Screen.fullScreen = value;
    }

    //Start menu
    private void Play()
    {
        if (!DBManager.isLoggedIn)
        {
            StartMenu.SetActive(false);
            LoginMenu.SetActive(true);
        }
        else
        {
            StartMenu.SetActive(false);
            JoinMenu.SetActive(true);
        }
    }

    private void Options()
    {
        StartMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    private void Exit()
    {
        Application.Quit();
    }

    //Options menu
    private void OptionsBack()
    {
        OptionsMenu.SetActive(false);
        StartMenu.SetActive(true);
    }

    private void Keybinds()
    {
        OptionsMenu.SetActive(false);
        KeybindsMenu.SetActive(true);
    }

    private void KeybindsBack()
    {
        KeybindsMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    //Register
    IEnumerator RegisterUser()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("username", RegisterUsernameInput.text));
        form.Add(new MultipartFormDataSection("password", RegisterPasswordInput.text));
        form.Add(new MultipartFormDataSection("email", RegisterEmailInput.text));

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/php/sqlconnect/register.php", form);

        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
    }

    public void TryRegister()
    {
        if (RegisterUsernameInput.text.Length < 7)
        {
            RegisterErrorText.text = "Your username should be atlest 8 characters long!";
            return;
        }
        else if (RegisterPasswordInput.text.Length < 7)
        {
            RegisterErrorText.text = "Your password should be atlest 8 characters long!";
            return;
        }
        else if (RegisterPasswordInput.text != RegisterPasswordConfirmInput.text)
        {
            RegisterErrorText.text = "Passwords don't match!";
            return;
        }
        else if (!StringUtils.ContainesDigit(RegisterPasswordInput.text))
        {
            RegisterErrorText.text = "Your password should contain atleast one digit!";
            return;
        }
        else if (!StringUtils.ContainesLower(RegisterPasswordInput.text))
        {
            RegisterErrorText.text = "Your password should contain atleast one lowercase letter!";
            return;
        }
        else if (!StringUtils.ContainesUpercase(RegisterPasswordInput.text))
        {
            RegisterErrorText.text = "Your password should contain atleast one uperrcase letter!";
            return;
        }
        else if (!StringUtils.ValidEmail(RegisterEmailInput.text))
        {
            RegisterErrorText.text = "Your email is invalid!";
            return;
        }
        else
        {
            StartCoroutine(RegisterUser());
        }

    }

    //Login
    IEnumerator LoginUser()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("username", LoginUsernameInput.text));
        form.Add(new MultipartFormDataSection("password", LoginPasswordInput.text));

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/php/sqlconnect/login.php", form);

        yield return www.SendWebRequest();

        DBManager.username = www.downloadHandler.text.Split('\t')[1];
    }

    public void TryLogin()
    {
        if (RegisterUsernameInput.text.Length < 7)
        {
            RegisterErrorText.text = "Your username should be atlest 8 characters long!";
            return;
        }
        else if (RegisterPasswordInput.text.Length < 7)
        {
            RegisterErrorText.text = "Your password should be atlest 8 characters long!";
            return;
        }
        else
        {
            StartCoroutine(LoginUser());
        }
    }
}
