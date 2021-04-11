using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Main : MonoBehaviour
{
    public static Main instance;
    public Keys Keys;
    public Logger logger;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        //if (DBManager.isLoggedIn)
            StartCoroutine(SaveKeybinds());    
    }

    IEnumerator SaveKeybinds()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("username", "testuser123"));
        form.Add(new MultipartFormDataSection("keybinds", Keys.ToJson()));
        //form.Add(new MultipartFormDataSection("username", DBManager.username));

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/php/sqlconnect/savekeybinds.php", form);

        yield return www.SendWebRequest();
    }

    IEnumerator LoadKeybinds()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("username", "testuser123"));
        //form.Add(new MultipartFormDataSection("username", DBManager.username));

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/php/sqlconnect/loadkeybinds.php", form);

        yield return www.SendWebRequest();

        Keys.FromJson(www.downloadHandler.text.Split('\t')[1]);
    }
}
