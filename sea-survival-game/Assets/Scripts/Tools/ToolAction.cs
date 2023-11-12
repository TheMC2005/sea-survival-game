using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAction : ScriptableObject
{
   public virtual bool OnApply(Vector2 worldPoint)
    {
        Debug.LogWarning("OnApply is not implemented");
        return true;
    }
    public virtual bool OnApplyToTileMap(Vector3Int gridposition, TileMapReadController tileMapReadController, Item item)
    {
        Debug.LogWarning("OnApplyToTileMap is not implemented");
        return true;
    }
    public virtual bool OnApplyToTileMap(Vector3Int gridposition, TileMapReadController tileMapReadController, CropSeedling item)
    {
        Debug.LogWarning("OnApplyToTileMap is not implemented");
        return true;
    }
    public virtual bool OnApplyToTileMap(Vector3Int gridposition, TileMapReadController tileMapReadController, building item)
    {
        Debug.LogWarning("OnApplyToTileMap is not implemented");
        return true;
    }
    public virtual bool OnApplyToTileMap(Vector3Int gridposition, TileMapReadController tileMapReadController, Tool item)
    {
        Debug.LogWarning("OnApplyToTileMap is not implemented");
        return true;
    }
    public virtual void OnItemUsed(Item usedItem)
    {
        
    }
}
