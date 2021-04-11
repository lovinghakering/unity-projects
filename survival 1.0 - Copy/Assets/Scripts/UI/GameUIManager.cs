using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public bool paused;
    public static GameUIManager instance;

    [Header("Pause")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button disconnectBtn;

    [Header("Settings")]
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Slider mouseSensXSlider;
    [SerializeField] private Slider mouseSensYSlider;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private TMP_InputField mouseSensXInput;
    [SerializeField] private TMP_InputField mouseSensYInput;
    [SerializeField] private TMP_InputField masterVolumeInput;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Button keybindsBtn;
    [SerializeField] private Button settingsCloseBtn;

    [Header("Keybinds")]
    [SerializeField] private GameObject keybindsMenu;
    [SerializeField] private Button keybindsCloseBtn;

    [Header("Inventory")]
    public GameObject Inventory;

    public Keys keys;
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
    }

    private void Start()
    {
        BindControlls();

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

        if (Input.GetKeyDown(keys.Inventory))
        {
            Inventory.SetActive(!Inventory.activeSelf);
        }
    }

    private void BindControlls()
    {
        //Buttons
        resumeBtn.onClick.AddListener(Pause);
        settingsBtn.onClick.AddListener(ToggleSettings);
        disconnectBtn.onClick.AddListener(Disconnect);
        settingsCloseBtn.onClick.AddListener(ToggleSettings);
        keybindsBtn.onClick.AddListener(ToggleKeybinds);
        keybindsCloseBtn.onClick.AddListener(ToggleKeybinds);

        //Slider
        mouseSensXSlider.onValueChanged.AddListener(MouseSensXSliderValueChanged);
        mouseSensYSlider.onValueChanged.AddListener(MouseSensYSliderValueChanged);
        masterVolumeSlider.onValueChanged.AddListener(masterVolumeSliderValueChanged);

        //Input
        mouseSensXInput.onValueChanged.AddListener(mouseSensXInputValueChanged);
        mouseSensYInput.onValueChanged.AddListener(mouseSensYInputValueChanged);
        mouseSensYInput.onEndEdit.AddListener(masterVolumeInputEndEdit);

        //Toggles
        fullscreenToggle.onValueChanged.AddListener(FulllscreenToggleValueChanged);
        Logger.Log("Bound UI controlls.");
    }

    //Pause Menu Controlls
    private void Pause()
    {
        if (paused)
        {
            pauseMenu.SetActive(false);
            settingsMenu.SetActive(false);
            keybindsMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            paused = false;
        }
        else
        {
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            paused = true;
        }
    }


    private void ToggleSettings()
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

    private void OpenSettings()
    {
        if (!toggingMenu)
        {
            toggingMenu = true;
            settingsMenu.SetActive(true);
            LeanTween.moveLocalX(settingsMenu, 254f, 0.15f);
            toggingMenu = false;
        }
    }

    private IEnumerator CloseSettigns()
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

    private void ToggleKeybinds()
    {
        if (keybindsMenu.activeSelf)
        {
            StartCoroutine(CloseKeybinds());
        }
        else
        {
            StartCoroutine(OpenKeybinds());
        }
    }

    private IEnumerator OpenKeybinds()
    {
        if (!toggingMenu)
        {
            toggingMenu = true;
            keybindsMenu.SetActive(true);
            LeanTween.moveLocalX(keybindsMenu, 254f, 0.15f);
            yield return new WaitForSeconds(0.15f);
            toggingMenu = false;
        }
    }

    private IEnumerator CloseKeybinds()
    {
        if (!toggingMenu)
        {
            toggingMenu = true;
            LeanTween.moveLocalX(keybindsMenu, 1660f, 0.15f);

            yield return new WaitForSeconds(0.15f);

            keybindsMenu.SetActive(false);
            toggingMenu = false;
        }
    }

    private void Disconnect()
    {
        Client.instance.Disconnect();
        SceneManager.UnloadSceneAsync("Game");
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Additive);
        Logger.Log("Disconnecting...");
    }

    //Settings Menu Controlls

    private void MouseSensXSliderValueChanged(float value)
    {
        mouseSensXInput.text = value.ToString();
        DBManager.mouseSensitivityX = value;
    }

    private void MouseSensYSliderValueChanged(float value)
    {
        mouseSensYInput.text = value.ToString();
        DBManager.mouseSensitivityY = value;
    }

    private void masterVolumeSliderValueChanged(float value)
    {
        masterVolumeInput.text = (value += 80).ToString();
    }

    //Input fields
    private void mouseSensXInputValueChanged(string value)
    {
        if (value.Length > 0)
        {
            mouseSensXSlider.value = int.Parse(value);
            DBManager.mouseSensitivityX = int.Parse(value);
        }
    }

    private void mouseSensYInputValueChanged(string value)
    {
        if (value.Length > 0)
        {
            mouseSensYSlider.value = int.Parse(value);
            DBManager.mouseSensitivityY = int.Parse(value);
        }
    }

    private void masterVolumeInputEndEdit(string value)
    {
        if (value.Length > 0)
            masterVolumeSlider.value = int.Parse(value) - 80;
    }

    private void FulllscreenToggleValueChanged(bool value)
    {
        Screen.fullScreen = value;
    }
}
