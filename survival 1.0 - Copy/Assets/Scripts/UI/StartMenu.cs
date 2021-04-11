using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private TextButton playButton;
    [SerializeField] private TextButton settingsButton;
    [SerializeField] private TextButton exitButton;

    private void Start()
    {
        SubscribeListeners();
    }

    private void SubscribeListeners()
    {
        playButton.onClick.AddListener(Play);
        settingsButton.onClick.AddListener(Settings);
        exitButton.onClick.AddListener(Exit);
    }

    private void Play()
    {

    }

    private void Settings()
    {
        SceneLoader.SettingsOpen();
    }

    private void Exit()
    {
        Application.Quit();
    }
}
