using UnityEngine;
using UnityEngine.UI;

public class ListContent : MonoBehaviour
{
    private void Start()
    {
        RectTransform _rt;
        _rt = GetComponent<RectTransform>();
        var _childCount = transform.childCount;
        float _height = (_childCount * transform.GetChild(0).GetComponent<RectTransform>().rect.height) + (GetComponent<VerticalLayoutGroup>().spacing * (_childCount - 1));
        _rt.sizeDelta = new Vector2(_rt.rect.width, _height);
    }
}
