using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildDic
{
    public static Dictionary<Vector3Int,TileBase> PosToTile = new Dictionary<Vector3Int, TileBase>();
    public static Dictionary<Vector3Int,Item> PosToItem = new Dictionary<Vector3Int, Item>();
}
