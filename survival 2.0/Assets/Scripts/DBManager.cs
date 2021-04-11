using UnityEngine;

public static class DBManager
{
    public static string username;

    public static bool isSingedIn
    {
        get { return !string.IsNullOrEmpty(username); }
    }

    public static void SignOut()
    {
        username = null;
        PlayerPrefs.DeleteKey("username");
        DebugUI.Instance.Log("Signed out.");
    }
}
