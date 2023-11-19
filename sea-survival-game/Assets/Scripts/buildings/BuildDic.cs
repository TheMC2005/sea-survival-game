using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildDic
{
    public static Dictionary<Vector3,GameObject> PosToBuild = new Dictionary<Vector3, GameObject>();
    public static Dictionary<Vector3,Item> PosToItem = new Dictionary<Vector3, Item>();
}
