using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Place Building")]
public class PlaceBuilding : ToolAction
{
    public override bool OnApply(Vector2 worldPoint){
        if(Hotbar.selSlot.item is Building){
            Building item = (Building)Hotbar.selSlot.item;
            GameObject building = Instantiate(item.building, Camera.main.ScreenToWorldPoint(Input.mousePosition), new Quaternion(0,0,0,1));
            building.transform.position=new Vector3(building.transform.position.x, building.transform.position.y, -5);
            InventoryManager.LoadSlots(InventoryManager.inventory);
            InventoryManager.inventory.RemoveItem(item, 1);
            InventoryManager.LoadSlots(InventoryManager.inventory);
        }
        return true;
    }
}
