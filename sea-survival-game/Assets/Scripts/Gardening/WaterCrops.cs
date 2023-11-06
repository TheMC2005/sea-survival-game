using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Water Crops")]
public class WaterCrops : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridposition, TileMapReadController tileMapReadController, Tool tool)
    {   if(tileMapReadController.cropsManager.Check(gridposition))
        tileMapReadController.cropsManager.WaterTile(gridposition);
    else
        {
            Debug.Log("Te most komolyan a sima földet/füvet öntözöd?");
        }
        return true;
    }
}
