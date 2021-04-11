using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Keys keys;
    public InventoryObject inventory;
    public DisplayInventory displayInventory;
    public GameObject inventoryUI;

    private void Start()
    {
        inventory = (InventoryObject)ScriptableObject.CreateInstance("InventoryObject");
        displayInventory = GameUIManager.instance.GetComponent<DisplayInventory>();
        displayInventory.inventory = inventory;
        inventoryUI = GameUIManager.instance.transform.GetChild(0).GetChild(0).gameObject;
        inventory.displayInventory = displayInventory;
    }

    private void Update()
    {
        if (Input.GetKeyDown(keys.Inventory))
        {
            if (inventoryUI.activeSelf)
            {
                inventoryUI.SetActive(true);
                displayInventory.UpdateInventory();
            }
            else
            {
                inventoryUI.SetActive(false);
            }
        }
    }
}
