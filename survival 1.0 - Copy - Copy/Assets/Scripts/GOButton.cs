using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GOButton : MonoBehaviour
{
    public UnityEvent onClick;

    public void Click()
    {
        onClick.Invoke();
    }
}
