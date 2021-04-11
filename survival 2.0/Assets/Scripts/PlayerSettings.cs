using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/PlayerSettigns")]
public class PlayerSettings : ScriptableObject
{
    public Settings settings;

    public void LoadDefault() => settings.LoadDefault();
}

[Serializable]
public class Settings
{
    public float mouseSensX;
    public float mouseSensY;
    public float masterVolume;
    public bool fullscreen;
    public int resolution;

    public Settings(float _mouseSensX, float _mouseSensY, float _masterVolume, bool _fullscreen, int _resolution)
    {
        mouseSensX = _mouseSensX;
        mouseSensY = _mouseSensY;
        masterVolume = _masterVolume;
        fullscreen = _fullscreen;
        resolution = _resolution;
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void FromJson(string _json)
    {
        JsonUtility.FromJsonOverwrite(_json, this);
    }

    public void LoadDefault()
    {
        mouseSensX = 300f;
        mouseSensY = 300f;
        masterVolume = -50;
        fullscreen = true;
        resolution = 0;
    }
}