using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Tool Action/Seed Tile")]
public class SeedTile : ToolAction
{

    public override bool OnApplyToTileMap(Vector3Int gridposition, TileMapReadController tileMapReadController)
    {
        if(tileMapReadController.cropsManager.Check(gridposition) == false)
        {
            return false;
        }
        if (Hotbar.selSlot.item.isCropSeed)
        {
            Item item = Hotbar.selSlot.item;
            tileMapReadController.cropsManager.Seed(gridposition, item.crop);

        }
        return true;
    }
    public override void OnItemUsed(Item usedItem, Inventory inventory)
    {
        inventory.RemoveItem(usedItem, 1);
    }
}
