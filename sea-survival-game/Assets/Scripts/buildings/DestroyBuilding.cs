using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Destroy Building")]

public class DestroyBuilding : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridposition, TileMapReadController tileMapReadController, Tool item)
    {
        Item.SummonItem(BuildDic.PosToItem[gridposition], new Vector2(gridposition.x, gridposition.y));
        tileMapReadController.cropsTileMap.SetTile(gridposition, null);
        return true;
    }
}
