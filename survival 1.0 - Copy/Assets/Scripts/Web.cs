using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    public Keys keys;

    public IEnumerator LoginUser(string username, string password, bool autoSingIn)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("username", username));
        form.Add(new MultipartFormDataSection("password", password));

        UnityWebRequest www = UnityWebRequest.Post("http://178.169.132.89/php/sqlconnect/login.php", form);

        yield return www.SendWebRequest();

        if (string.IsNullOrEmpty(www.downloadHandler.text))
        {
            Main.instance.Error.Show("Login Error", "Couldn't connect to the database!");
        }
        else if (www.downloadHandler.text[0] == '0')
        {
            Debug.Log("Logged in as: " + username);
            DBManager.username = username;
            StartCoroutine(LoadSettings());
            StartCoroutine(LoadKeybinds());
            MenuUIManager.instance.SuccessfulLogin();

            if (autoSingIn == true)
            {
                PlayerPrefs.SetString("username", username);
                Logger.Log("Auto sing in enabled.");
            }
        }
        else if (www.downloadHandler.text[0] == '1')
        {
            //Couldn't connect to database
            Main.instance.Error.Show("Login Error", "Couldn't connect to the database!");
        }
        else if (www.downloadHandler.text[0] == '3')
        {
            //This username does not exist
            MenuUIManager.instance.LoginError("This username does not exist!");
        }
        else if (www.downloadHandler.text[0] == '2')
        {
            //Name check query failed
            Main.instance.Error.Show("Login Error", "Name check query failed!");
        }
        else if (www.downloadHandler.text[0] == '4')
        {
            //Wrong password
            MenuUIManager.instance.LoginError("Wrong password!");
        }

        www.Dispose();
    }

    public IEnumerator RegisterUser(string username, string password, string email)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("username", username));
        form.Add(new MultipartFormDataSection("password", password));
        form.Add(new MultipartFormDataSection("email", email));

        UnityWebRequest www = UnityWebRequest.Post("http://178.169.132.89/php/sqlconnect/register.php", form);

        yield return www.SendWebRequest();

        if (string.IsNullOrEmpty(www.downloadHandler.text))
        {
            Main.instance.Error.Show("Login Error", "Couldn't connect to the database!");
        }
        else if (www.downloadHandler.text[0] == '0')
        {
            DBManager.username = username;
            StartCoroutine(LoadSettings());
            StartCoroutine(LoadKeybinds());
            MenuUIManager.instance.SuccessfulRegister();
        }
        else if (www.downloadHandler.text[0] == '1')
        {
            //Couldn't connect to database
            Main.instance.Error.Show("Register Error", "Couldn't connect to the database!");
        }
        else if (www.downloadHandler.text[0] == '3')
        {
            //This username already exists
            MenuUIManager.instance.RegisterError("This username already exists!");
        }
        else if (www.downloadHandler.text[0] == '2')
        {
            //Name check query failed
            Main.instance.Error.Show("Register Error", "Name check query failed!");
        }
        else if (www.downloadHandler.text[0] == '6')
        {
            //Wrong password
            Main.instance.Error.Show("Register Error", "Game data query failed!");
        }

        www.Dispose();
    }

    //Keybinds
    public IEnumerator SaveKeybinds()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("username", DBManager.username));
        form.Add(new MultipartFormDataSection("keybinds", keys.ToJson()));

        UnityWebRequest www = UnityWebRequest.Post("http://178.169.132.89/php/sqlconnect/savekeybinds.php", form);

        yield return www.SendWebRequest();

        www.Dispose();
    }

    public IEnumerator LoadKeybinds()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("username", DBManager.username));

        UnityWebRequest www = UnityWebRequest.Post("http://178.169.132.89/php/sqlconnect/loadkeybinds.php", form);

        yield return www.SendWebRequest();

        keys.FromJson(www.downloadHandler.text.Split('\t')[1]);

        www.Dispose();
    }

    //Settings
    public IEnumerator SaveSettings(string settingsJson)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("username", DBManager.username));
        form.Add(new MultipartFormDataSection("settings", settingsJson));

        UnityWebRequest www = UnityWebRequest.Post("http://178.169.132.89/php/sqlconnect/savesettings.php", form);

        yield return www.SendWebRequest();

        www.Dispose();
    }

    public IEnumerator LoadSettings()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("username", DBManager.username));

        UnityWebRequest www = UnityWebRequest.Post("http://178.169.132.89/php/sqlconnect/loadsettings.php", form);

        yield return www.SendWebRequest();
        try
        {
            MenuUIManager.instance.LoadSettings(www.downloadHandler.text.Split('\t')[1]);
        }
        catch (Exception _ex)
        {
            Main.instance.Error.Show("Data base error. Couldn't connect to the database to load settings!", _ex.ToString());
        }

        www.Dispose();
    }
}
