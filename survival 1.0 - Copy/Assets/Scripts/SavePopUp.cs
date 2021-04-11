using UnityEngine;
using UnityEngine.Events;

public class SavePopUp : MonoBehaviour
{
    [SerializeField] private Button SaveBtn;
    [SerializeField] private Button DontSaveBtn;

    public void Populate(UnityAction saveEvent, UnityAction dontSaveEvent)
    {
        SaveBtn.onClick.AddListener(saveEvent);
        DontSaveBtn.onClick.AddListener(dontSaveEvent);

        SaveBtn.onClick.AddListener(DestroyThis);
        DontSaveBtn.onClick.AddListener(DestroyThis);
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}
