using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName ="Data/Tool Action/Plow")]
public class PlowTile : ToolAction
{
   [SerializeField] public List<TileBase> canPlow;
    public override bool OnApplyToTileMap(Vector3Int gridposition, TileMapReadController tileMapReadController, Tool tool)
    {
        TileBase tileToPlow = tileMapReadController.GetTileBase(gridposition);
        if(canPlow.Contains(tileToPlow) && tileMapReadController.cropsManager.Check(gridposition)==false)
        {
            tileMapReadController.cropsManager.Plow(gridposition);
        }
        if (tileMapReadController.cropsManager.Check(gridposition) == true)
        {
            tileMapReadController.cropsManager.PlowOutCrop(gridposition);
        }
        return true;
    }
}
/*
 * if (canPlow.Contains(tileToPlow) == false || tileMapReadController.cropsManager.Check(gridposition) == true)
        {
            tileMapReadController.cropsManager.PlowOutCrop(gridposition);
            return false;
        }
        else
        {
            Debug.Log("En vagyok a hibas");
            tileMapReadController.cropsManager.Plow(gridposition);
        }*/