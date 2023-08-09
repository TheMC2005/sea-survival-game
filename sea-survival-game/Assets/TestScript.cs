using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickUp;

    public void PickUpItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickUp[id]);
        if(result)
        {
            Debug.Log("Item Added");
        }
        else
        {
            Debug.Log("Item Not Added");
        }
    }
    public void GetSelectedItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem();
        if(receivedItem != null)
        {
            Debug.Log(receivedItem);
        }
        else
            Debug.Log("No items received");
    }
}
