using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public static Main instance;
    public GameUIManager GameUIManager;
    public Web Web;
    public Error Error;

    public int serverPort;
    public string serverIp;

    public bool inGame;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }
}
