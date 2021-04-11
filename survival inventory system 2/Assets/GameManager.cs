using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private InventoryObject inventory;
    [SerializeField] private ItemDatabase database;
    
    public ItemDrag itemDragObject;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void UpdateDragObject(int _slotId, int _amount)
    {
        itemDragObject.Populate(database.GetItem[inventory.container.slots[_slotId].id].sprite, _amount);
        inventory.container.slots[_slotId].amount =- _amount;
        if (inventory.container.slots[_slotId].)
    }
}
