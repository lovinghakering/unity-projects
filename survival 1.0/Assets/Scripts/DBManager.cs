using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager
{
    public static string username;
    
    public static bool isLoggedIn { get { return !string.IsNullOrEmpty(username); } }

    public static void LogOut()
    {
        username = null;
    }
}
