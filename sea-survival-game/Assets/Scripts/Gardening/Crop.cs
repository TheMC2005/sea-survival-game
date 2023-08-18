using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "NewCrop", menuName = "Crops/Create New Crop")]
public class Crop : Item
{
    public int timeToGrow;
    public int timeToWither;
    public int dropAmount;  
    public Item yield;
    public TileBase seededTile;
    public List<Sprite> sprites;
    public List<int> growthStageTimes;
}