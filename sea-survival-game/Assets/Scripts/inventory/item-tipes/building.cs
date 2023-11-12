using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "NewBuilding", menuName = "Item/Create New Building")]
public class building : Item
{
    public TileBase tile;
    public ToolAction onTileMapAction;
    public ToolAction onItemUsed;
}
