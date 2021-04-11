using UnityEngine;

public class RedirectPopUp : MonoBehaviour
{
    [SerializeField] private Button closeBtn;
    [SerializeField] private Button continueBtn;

    string url;

    private void Awake()
    {
        closeBtn.onClick.AddListener(Close);
    }

    public void Populate(string _url)
    {
        url = _url;

        continueBtn.onClick.AddListener(Continue);
    }

    private void Close()
    {
        Destroy(gameObject);
    }

    private void Continue()
    {
        Application.OpenURL(url);
        Destroy(gameObject);
    }
}
