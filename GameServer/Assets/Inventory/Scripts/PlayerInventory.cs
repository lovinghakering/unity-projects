using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;

    private int id;
    
    private void Start()
    {
        id = GetComponent<Player>().id;
        inventory = new InventoryObject();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Web.instance.SaveInventoryWeb("testuser123", JsonUtility.ToJson(inventory)));
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(Web.instance.LoadInventoryWeb("testuser123", inventory));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            var item = other.GetComponent<Item>();
            if(inventory.AddItem(id, item.item, 1))
                Destroy(other.gameObject);
        }
    }
}
