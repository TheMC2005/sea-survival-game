using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Water Crops")]
public class WaterCrops : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridposition, TileMapReadController tileMapReadController, Item item)
    { 
        return true;
    }
}
