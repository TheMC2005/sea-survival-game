using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Place Building")]
public class PlaceBuilding : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridposition, TileMapReadController tileMapReadController, Building item)
    {
        GameObject building = Instantiate(item.building, Camera.main.ScreenToWorldPoint(Input.mousePosition), new Quaternion(0,0,0,1));
        building.transform.Translate(Vector3.forward*5);
        return true;
    }

    public override void OnItemUsed(Item usedItem)
    {
        InventoryManager.LoadSlots(InventoryManager.inventory);
        InventoryManager.inventory.RemoveItem(usedItem, 1);
        InventoryManager.LoadSlots(InventoryManager.inventory);
    }
}
