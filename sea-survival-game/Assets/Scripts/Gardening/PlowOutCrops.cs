using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Plow Out Crops")]
public class PlowOutCrops : ToolAction
{ 
    public override bool OnApplyToTileMap(Vector3Int gridposition, TileMapReadController tileMapReadController, Item item)
    {
        if(tileMapReadController.cropsManager.Check(gridposition) == false)
        {
            tileMapReadController.cropsManager.Plow(gridposition);
        }
        else
        {
            tileMapReadController.cropsManager.PlowOutCrop(gridposition);
        }
        return true;
    }
}
/*
Vector2Int position = (Vector2Int)gridposition;
if (crops.ContainsKey(position) == false)
{
    return;
}
CropsTile cropTile = crops[position];
if (cropTile.ReadyToWither && cropTile.crop != null)
{
    cropTile.tileBase = PlowedDirt;
    cropTile.tileBaseName = "PlowedDirt";
    cropTilemap.SetTile(gridposition, PlowedDirt);
    cropTile.crop = null;
    cropTile.cropsID = null;
    cropTile.damage = 0;
    cropTile.spriteRenderer.gameObject.SetActive(false);
}
*/