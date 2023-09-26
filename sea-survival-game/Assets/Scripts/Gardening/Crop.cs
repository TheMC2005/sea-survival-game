using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "NewCrop", menuName = "Crops/Create New Crop")]
public class Crop : Item
{
    public int cropID;
    public int timeToGrow;
    public int timeToWither;
    public int maxDropAmount;
    public bool isSeasonialCrop;
    public Item yield;
    public TileBase seededTile;
    public Sprite witheredSprite;
    public List<Sprite> sprites;
    [Tooltip("A growthStageTimesnak a lastIndex valuejának >= timeToGrowal")]
    public List<int> growthStageTimes;

}