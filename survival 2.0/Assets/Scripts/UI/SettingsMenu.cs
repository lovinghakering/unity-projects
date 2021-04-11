using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public GameObject saveChangesPopUp;
    
    public PlayerSettings playerSettings;
    public AudioMixer audioMixer;
    
    public GameObject startMenu;
    
    [Header("Audio Tab")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private TextMeshProUGUI masterVolumeValue;
    
    [Header("Graphics Tab")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private  Toggle fullscreenToggle;

    [Header("Mouse Tab")]
    [SerializeField] private Slider mouseSensXSlider;
    [SerializeField] private TextMeshProUGUI mouseSensXValue;
    [SerializeField] private Slider mouseSensYSlider;
    [SerializeField] private TextMeshProUGUI mouseSensYValue;

    private Resolution[] resolutionsArray;
    private List<Resolution> resolutionsList;

    private Settings startSettings;

    private void OnEnable()
    {
        startSettings = new Settings(mouseSensXSlider.value, mouseSensYSlider.value, masterVolumeSlider.value, fullscreenToggle.isOn, resolutionDropdown.value);
    }

    private void OnDisable()
    {
        startSettings = null;
    }
    
    public void Load(Settings _settings)
    {
        mouseSensXSlider.value = _settings.mouseSensX;
        mouseSensXValue.text = Mathf.Round(_settings.mouseSensX).ToString();
        
        mouseSensYSlider.value = _settings.mouseSensX;
        mouseSensYValue.text = Mathf.Round(_settings.mouseSensX).ToString();

        masterVolumeSlider.value = _settings.mouseSensX;
        masterVolumeValue.text = Mathf.Round(_settings.mouseSensX).ToString();
        
        fullscreenToggle.isOn = _settings.fullscreen;

        resolutionDropdown.value = _settings.resolution;
    }

    public void Save()
    {
        playerSettings.settings.mouseSensX = mouseSensXSlider.value;
        playerSettings.settings.mouseSensY = mouseSensYSlider.value;
        playerSettings.settings.masterVolume = masterVolumeSlider.value;
        playerSettings.settings.fullscreen = fullscreenToggle.isOn;
        playerSettings.settings.resolution = resolutionDropdown.value;
    }

    private void Start()
    {
        SetupResolutionDropdown();
    }

    public void Close()
    {
        Settings _settings = new Settings(mouseSensXSlider.value, mouseSensYSlider.value, masterVolumeSlider.value, fullscreenToggle.isOn, resolutionDropdown.value);
        if (Math.Abs(startSettings.mouseSensX - _settings.mouseSensX) > Consts.FLOAT_TOLERANCE ||
            Math.Abs(startSettings.mouseSensY - _settings.mouseSensY) > Consts.FLOAT_TOLERANCE ||
            Math.Abs(startSettings.masterVolume - _settings.masterVolume) > Consts.FLOAT_TOLERANCE ||
            startSettings.fullscreen != _settings.fullscreen ||
            startSettings.resolution != _settings.resolution)
        {
            Instantiate(saveChangesPopUp, this.transform).GetComponent<SavePopUp>().Populate(this);
        }
        else
        {
            gameObject.SetActive(false);
            startMenu.SetActive(true);
        }
    }
    
    public void SaveClose()
    {
        Save();
        this.gameObject.SetActive(false);
    }
    
    public void DontSaveClose()
    {
        Load(startSettings);
        this.gameObject.SetActive(false);
    }
    
    private void SetupResolutionDropdown()
    {
        resolutionsArray = Screen.resolutions;
        resolutionsList = new List<Resolution>(resolutionsArray);
        resolutionsList.Reverse();

        resolutionDropdown.ClearOptions();

        List<string> _options = new List<string>();

        int _currentResolutionIndex = 0;
        for (int i = 0; i < resolutionsArray.Length; i++)
        {
            _options.Add(resolutionsArray[i].width + " x " + resolutionsArray[i].height);
        }

        List<string> _options2 = _options.Distinct().ToList();
        _options2.Reverse();

        _options.Reverse();
        for (int i = 0; i < _options2.Count; i++)
        {
            if (_options2[i] == Screen.currentResolution.width + " x " + Screen.currentResolution.height)
            {
                _currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(_options2);
        resolutionDropdown.value = _currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void MasterVolumeSliderValueChanged(float _value)
    {
        masterVolumeValue.text = Mathf.Round(_value + 80).ToString();
        audioMixer.SetFloat("Master Volume", _value);
    }

    public void XmouseSensSliderValueChanged(float _value)
    {
        mouseSensXValue.text = Mathf.Round(_value).ToString();
    }
    
    public void YmouseSensSliderValueChanged(float _value)
    {
        mouseSensYValue.text = Mathf.Round(_value).ToString();
    }

    public void FullsceenToggleValueChanged(bool _value)
    {
        Screen.fullScreen = _value;
    }

    public void Reload()
    {
        Load(playerSettings.settings);
    }
}