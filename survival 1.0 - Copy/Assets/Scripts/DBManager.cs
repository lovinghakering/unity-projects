using UnityEngine;

public class DBManager
{
    public static string username;
    
    public static bool isLoggedIn { get { return !string.IsNullOrEmpty(username); } }

    //Information about the user
    public static float mouseSensitivityX = 400;
    public static float mouseSensitivityY = 400;

    public static void LogOut()
    {
        username = null;
        PlayerPrefs.DeleteKey("username");
        Logger.Log("Singed out.");
        MenuUIManager.instance.LogOut();
    }
}
