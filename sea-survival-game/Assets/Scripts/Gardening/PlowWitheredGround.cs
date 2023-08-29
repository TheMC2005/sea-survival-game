using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Plow Withered Ground")]
public class PlowWitheredGround : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridposition, TileMapReadController tileMapReadController, Item item)
    {
        tileMapReadController.cropsManager.PlowWitheredGround(gridposition);
        return true;
    }
}
