using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class GameUIManager : MonoBehaviour
{
    public bool paused;
    public Keys keys;
    public static GameUIManager instance;

    [Header("Pause")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button disconnectBtn;

    [Header("Settings")]
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject audioMenu;
    [SerializeField] private GameObject graphicsMenu;
    [SerializeField] private GameObject mouseMenu;
    [SerializeField] private GameObject keybindsMenu;
    [SerializeField] private TMP_Dropdown resolutonDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Slider mouseSensXSlider;
    [SerializeField] private Slider mouseSensYSlider;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private TMP_InputField mouseSensXInput;
    [SerializeField] private TMP_InputField mouseSensYInput;
    [SerializeField] private TMP_InputField masterVolumeInput;

    [Header("Inventory")]
    public GameObject Inventory;

    [Header("Hud")]
    public GameObject hudPanel;
    public TextMeshProUGUI chatText;
    public Scrollbar chatScrollbar;
    public TMP_InputField chatInput;

    Resolution[] resolutionsArray;
    List<Resolution> resolutionsList;
    [SerializeField] private GameObject currentTab;
    private bool toggingMenu = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
        chatInput.onFocusSelectAll = false;
    }

    private void Start()
    {
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

        Main.instance.GameUIManager = this;
        Main.instance.inGame = true;

        Client.instance.ConnectToServer();
    }

    private void Update()
    {
        if (Input.GetKeyDown(keys.Pause))
        {
            Pause();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (chatInput.gameObject.activeSelf)
            {
                SendChatMessage();
                chatInput.gameObject.SetActive(false);
                GameManager.instance.localPlayer.GetComponent<PlayerController>().allowMovement = true;
            }
            else
            {
                chatInput.gameObject.SetActive(true);
                chatInput.Select();
                GameManager.instance.localPlayer.GetComponent<PlayerController>().allowMovement = false;
            }
        }

        if (Input.GetKeyDown(keys.ToggleCursor))
        {
            if (Cursor.lockState == CursorLockMode.None)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(keys.Inventory))
        {
            Inventory.SetActive(!Inventory.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            ReceiveChatMessage(1, "Test chat message!");
        }
    }

    #region Hud
    public void ReceiveChatMessage(int _fromId, string _msg)
    {
        string _username = GameManager.players[_fromId].username;
        chatText.text += $"[{_username}] {_msg}\n";
        chatText.rectTransform.sizeDelta = new Vector2(chatText.rectTransform.sizeDelta.x, chatText.preferredHeight);
        chatScrollbar.value = 0;
    }

    public void SendChatMessage()
    {
        string _msg = chatInput.text;
        if (string.IsNullOrEmpty(chatInput.text))
            return;

        chatInput.text = string.Empty;
        ClientSend.SendChatMessage(_msg);
    }
    #endregion

    #region Pause Menu
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

    public void SettingsClose()
    {
        SaveSettings();
        SaveKeybinds();
        settingsMenu.SetActive(false);
    }

    private void SaveSettings()
    {
        Settings settings = new Settings(mouseSensXSlider.value, mouseSensYSlider.value, masterVolumeSlider.value, fullscreenToggle.isOn);
        string jsonString = JsonUtility.ToJson(settings);
        StartCoroutine(Main.instance.Web.SaveSettings(jsonString));
    }

    private void SaveKeybinds() => StartCoroutine(Main.instance.Web.SaveKeybinds());

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

    public void FulllscreenToggleValueChanged(bool value)
    {
        Screen.fullScreen = value;
    }

    public void MouseSensXSliderValueChanged(float value)
    {
        mouseSensXInput.text = value.ToString();
        DBManager.mouseSensitivityX = value;
    }

    public void MouseSensYSliderValueChanged(float value)
    {
        mouseSensYInput.text = value.ToString();
        DBManager.mouseSensitivityY = value;
    }

    public void masterVolumeSliderValueChanged(float value)
    {
        masterVolumeInput.text = (value += 80).ToString();
    }
    #endregion
    public void Pause()
    {
        if (paused)
        {
            if (!GetComponent<KeybindsList>().reading)
            {
                hudPanel.SetActive(true);
                pauseMenu.SetActive(false);
                settingsMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                paused = false;
            }
        }
        else
        {
            hudPanel.SetActive(false);
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            paused = true;
        }
    }

    public void ToggleSettings()
    {
        if (settingsMenu.activeSelf)
        {
            StartCoroutine(CloseSettigns());
        }
        else
        {
            OpenSettings();
        }
    }

    public void OpenSettings()
    {
        if (!toggingMenu)
        {
            toggingMenu = true;
            settingsMenu.SetActive(true);
            LeanTween.moveLocalX(settingsMenu, 254f, 0.15f);
            toggingMenu = false;
        }
    }

    public IEnumerator CloseSettigns()
    {
        if (!toggingMenu)
        {
            toggingMenu = true;
            LeanTween.moveLocalX(settingsMenu, 1660f, 0.215f);

            yield return new WaitForSeconds(0.15f);

            settingsMenu.SetActive(false);
            toggingMenu = false;
        }
    }

    public void Disconnect()
    {
        Logger.Log("Disconnecting...");
        Client.instance.Disconnect();
        GameManager.instance.Disconnect();
        SceneManager.UnloadSceneAsync("Game");
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Additive);
    }
    #endregion
}
