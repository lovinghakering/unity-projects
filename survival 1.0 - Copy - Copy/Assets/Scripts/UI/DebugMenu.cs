using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugMenu : MonoBehaviour
{
    public static DebugMenu instance;

    [SerializeField] private Keys keys;

    [SerializeField] private GameObject linePrefab;

    [SerializeField] private TextMeshProUGUI stateText;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform contentRect;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Logger.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(keys.Debug))
        {
            GetComponent<Canvas>().enabled = !GetComponent<Canvas>().enabled;
        }
    }

    public void PromptMessage(string msg)
    {
        TextMeshProUGUI text = Instantiate(linePrefab, contentRect).GetComponent<TextMeshProUGUI>(); //Instantiate a new line and change its color
        text.text = msg;

        // Scroll to bottom 
        float height = contentRect.childCount * 26.82f + (contentRect.childCount - 2) * 0.78f;
        if (height < 706.51)
            height = 706.51f;

        contentRect.sizeDelta = new Vector2(850f, height);
        contentRect.localPosition = new Vector3(0, 1000, 0);
    }

    public void PromptError(string msg)
    {
        TextMeshProUGUI text = Instantiate(linePrefab, contentRect).GetComponent<TextMeshProUGUI>(); //Instantiate a new line and change its color
        text.color = Color.red;
        text.text = msg;

        // Scroll to bottom 
        float height = contentRect.childCount * 26.82f + (contentRect.childCount - 2) * 0.78f;
        if (height < 706.51)
            height = 706.51f;

        contentRect.sizeDelta = new Vector2(850f, height);
        contentRect.localPosition = new Vector3(0, height, 0);
    }

    public void PromptWarning(string msg)
    {
        TextMeshProUGUI text = Instantiate(linePrefab, contentRect).GetComponent<TextMeshProUGUI>(); //Instantiate a new line and change its color
        text.color = Color.yellow;
        text.text = msg;

        // Scroll to bottom 
        float height = contentRect.childCount * 26.82f + (contentRect.childCount - 2) * 0.78f;
        if (height < 706.51)
            height = 706.51f;

        contentRect.sizeDelta = new Vector2(850f, height);
        contentRect.localPosition = new Vector3(0, 1000, 0);
    }

    public void UpdateStatistics(Vector3 position, float speed, float fov, bool isGrounded, Vector3 velocity)
    {
        string x;
        string y;
        string z;

        if (position.x.ToString().Length > 5)
        {
            x = position.x.ToString().Remove(4);
        }
        else
        {
            x = position.x.ToString();
        }
        if (position.y.ToString().Length > 5)
        {
            y = position.y.ToString().Remove(4);
        }
        else
        {
            y = position.y.ToString();
        }
        if (position.z.ToString().Length > 5)
        {
            z = position.z.ToString().Remove(4);
        }
        else
        {
            z = position.z.ToString();
        }

        stateText.text = $"Player Position: X: {x} Y: {y} Z: {z}\n" +
            $"Player speed: {speed}\n" +
            $"Player FOV: {fov}\n" +
            $"Is player grounded: {isGrounded}\n" +
            $"Gravitation velocity: {velocity.y}";
    }
}
