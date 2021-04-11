using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

class Web : MonoBehaviour
{
    public static Web instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    public IEnumerator SaveInventoryWeb(string _username, string _inventory)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("username", _username));
        form.Add(new MultipartFormDataSection("inventory", _inventory));

        UnityWebRequest www = UnityWebRequest.Post("http://178.169.132.89/php/sqlconnect/saveinventory.php", form);

        yield return www.SendWebRequest();

        www.Dispose();
    }

    public IEnumerator LoadInventoryWeb(string _username, InventoryObject _inventoryObject)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("username", _username));

        UnityWebRequest www = UnityWebRequest.Post("http://178.169.132.89/php/sqlconnect/loadinventory.php", form);

        yield return www.SendWebRequest();

        _inventoryObject.Load(www.downloadHandler.text.Split('\t')[1]);

        www.Dispose();
    }
}