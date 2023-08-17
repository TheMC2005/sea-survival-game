using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCrop", menuName = "Crops/Create New Crop")]
public class Crop : ScriptableObject
{
    public int timeToGrow;
    public int timeToWither;
    public int dropAmount;
    public Item yield;
    public List<Sprite> sprites;
    public List<int> growthStageTimes;
}