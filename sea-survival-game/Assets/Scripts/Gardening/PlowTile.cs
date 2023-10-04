using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName ="Data/Tool Action/Plow")]
public class PlowTile : ToolAction
{
   [SerializeField] public List<TileBase> canPlow;
    public override bool OnApplyToTileMap(Vector3Int gridposition, TileMapReadController tileMapReadController, Item item)
    {
        TileBase tileToPlow = tileMapReadController.GetTileBase(gridposition);
        if(canPlow.Contains(tileToPlow) == false)
        { 
        return false;
        }
        tileMapReadController.cropsManager.Plow(gridposition);
        return true;
    }
}
