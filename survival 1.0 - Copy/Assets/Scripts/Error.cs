using UnityEngine;

public class Error : MonoBehaviour
{
    [SerializeField]
    private GameObject errorPrefab;

    public void Show(string title, string content, int controlCode)
    {
        Instantiate(errorPrefab, GameObject.Find("Canvas").transform).GetComponent<ErrorPopUp>().Populate(title, content, controlCode);
    }

    public void Show(string title, string content)
    {
        Instantiate(errorPrefab, GameObject.Find("Canvas").transform).GetComponent<ErrorPopUp>().Populate(title, content);
    }

    public void Show(string content)
    {
        Instantiate(errorPrefab, GameObject.Find("Canvas").transform).GetComponent<ErrorPopUp>().Populate(content);
    }
}
