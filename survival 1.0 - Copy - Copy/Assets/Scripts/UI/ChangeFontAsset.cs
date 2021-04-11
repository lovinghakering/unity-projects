using System.Collections;
using TMPro;
using UnityEngine;

public class ChangeFontAsset : MonoBehaviour
{
    public TMP_FontAsset font;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(3);

        TextMeshProUGUI[] allGOs = FindObjectsOfType<TextMeshProUGUI>();

        foreach (TextMeshProUGUI obj in allGOs)
        {
            obj.font = font;
        }
    }
}
