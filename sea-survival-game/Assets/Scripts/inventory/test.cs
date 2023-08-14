using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class test : MonoBehaviour
{
    public Item item;
    public Camera cam;

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject()){
            if (Input.GetMouseButtonDown(0))
                Item.SummonItem(item, cam.ScreenToWorldPoint(Input.mousePosition));
            if (Input.GetMouseButtonDown(1))
            {
                InventoryManager.inventory.RemoveItem(item, 5);
                InventoryManager.LoadSlots(InventoryManager.inventory);
            }
        }
    }
}
