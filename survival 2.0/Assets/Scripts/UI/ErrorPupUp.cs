using TMPro;
using UnityEngine;

public class ErrorPupUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    
    public void Populate(string _content)
    {
        text.text = _content;
    }
}