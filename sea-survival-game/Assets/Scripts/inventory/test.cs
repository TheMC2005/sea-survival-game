using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Item item;
    public Camera cam;
    private Inventory inv;

    void Start()
    {
        inv = InventoryManager.inventory;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Item.SummonItem(item, cam.ScreenToWorldPoint(Input.mousePosition));
        if (Input.GetMouseButtonDown(1))
        {
            inv.RemoveItem(item, 5);
            InventoryManager.LoadSlots(InventoryManager.inventory, InventoryManager.PlayerSlots);
        }
    }
}
