using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Place Building")]
public class PlaceBuilding : ToolAction
{
    public Tilemap tilemap;
    public override bool OnApplyToTileMap(Vector3Int gridposition, TileMapReadController tileMapReadController, Building item)
    {
        if (tileMapReadController.cropsManager.Check(gridposition) == false)
        {
            tileMapReadController.cropsTileMap.SetTile(gridposition, item.tile);
            BuildDic.PosToTile.Add(gridposition, item.tile);
            BuildDic.PosToItem.Add(gridposition, item);
        }
        return true;
    }

    public override void OnItemUsed(Item usedItem)
    {
        InventoryManager.LoadSlots(InventoryManager.inventory);
        InventoryManager.inventory.RemoveItem(usedItem, 1);
        InventoryManager.LoadSlots(InventoryManager.inventory);
    }
}
