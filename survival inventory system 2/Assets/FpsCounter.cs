using TMPro;
using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    public float timer, refresh, avgFramerate;
    string display = "{0} FPS";
    private TextMeshProUGUI text;
 
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
 
 
    private void Update()
    {
        float _timelapse = Time.deltaTime;
        timer = timer <= 0 ? refresh : timer -= _timelapse;
 
        if(timer <= 0) avgFramerate = (int) (1f / _timelapse);
        text.text = string.Format(display,avgFramerate.ToString());
    }
}
