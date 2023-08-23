using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Tool Action/Seed Tile")]
public class SeedTile : ToolAction
{

    public override bool OnApplyToTileMap(Vector3Int gridposition, TileMapReadController tileMapReadController, Item item)
    {
        if(tileMapReadController.cropsManager.Check(gridposition) == false)
        {
            return false;
        }
        
        tileMapReadController.cropsManager.Seed(gridposition, item.crop);

        return true;
    }
    public override void OnItemUsed(Item usedItem)
    {
        InventoryManager.LoadSlots(InventoryManager.inventory);
        InventoryManager.inventory.RemoveItem(usedItem, 1);
        InventoryManager.LoadSlots(InventoryManager.inventory);
    }
}
