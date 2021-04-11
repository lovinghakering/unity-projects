using TMPro;
using UnityEngine;

public class ErrorPopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private GameObject tryAgainBtn;
    [SerializeField] private GameObject closeBtn;

    private void Awake()
    {
        closeBtn.GetComponent<Button>().onClick.AddListener(Close);
    }

    public void Close()
    {
        Destroy(this.gameObject);
    }

    public void Populate(string title, string content, int controlCode)
    {
        titleText.text = title;
        contentText.text = content;

        if (controlCode == 0)
        {
            tryAgainBtn.SetActive(false);
            closeBtn.SetActive(false);
        }
        else if (controlCode == 1)
        {
            tryAgainBtn.SetActive(false);
            closeBtn.SetActive(true);
        }
        else if (controlCode == 2)
        {
            tryAgainBtn.SetActive(true);
            closeBtn.SetActive(true);
        }
    }

    public void Populate(string title, string content)
    {
        titleText.text = title;
        contentText.text = content;

        tryAgainBtn.SetActive(false);
        closeBtn.SetActive(true);
    }

    public void Populate(string content)
    {
        contentText.text = content;

        tryAgainBtn.SetActive(false);
        closeBtn.SetActive(true);
    }
}
