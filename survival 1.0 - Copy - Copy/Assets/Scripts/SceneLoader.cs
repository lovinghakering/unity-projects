using UnityEngine.SceneManagement;

class SceneLoader
{
    public static void SettingsClose()
    {
        SceneManager.UnloadSceneAsync("SettingsMenu");
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Additive);
    }

    public static void SettingsOpen()
    {
        SceneManager.UnloadSceneAsync("StartMenu");
        SceneManager.LoadScene("SettingsMenu", LoadSceneMode.Additive);
    }
}