using UnityEngine;

public class GroundItem : MonoBehaviour
{
    public bool pickedUp = false;
    public ItemObject item;
    public int amount;

    public void SetAmount(float _leftOver)
    {
        amount = (int) _leftOver;
        if (amount <= 0)
            Destroy(gameObject);
    }
}
