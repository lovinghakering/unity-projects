using UnityEngine;

public class GroundItem : MonoBehaviour
{
    public int amount;
    public int itemId;

    public void SetAmount(int _addAmount)
    {
        amount = _addAmount;
        if (amount <= 0)
        {
            Destroy(gameObject);
        }
    }
}