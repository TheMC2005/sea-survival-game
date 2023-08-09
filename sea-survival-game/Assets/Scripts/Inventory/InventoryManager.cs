using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStack = 999;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    int selectedSlot = -1;

    private void Update()
    {
        //erre biztos van jobb megoldas is de majd kiugyeskedem
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangedSelectedSlot(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangedSelectedSlot(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangedSelectedSlot(2);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangedSelectedSlot(3);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangedSelectedSlot(4);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangedSelectedSlot(5);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha7))
        {
            ChangedSelectedSlot(6);
        }
        
    }
    void ChangedSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }
    public bool AddItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            // ha van tobb hasonlo adja oket ossze
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStack && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }
        //empty slotok
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            // ures slotos
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }
    

    public void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponentInChildren<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }
    //kis hozzaadas a tool systemhet tuti jol jon
    public Item GetSelectedItem()
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            return itemInSlot.item;
        }
        return null;
    }
}

