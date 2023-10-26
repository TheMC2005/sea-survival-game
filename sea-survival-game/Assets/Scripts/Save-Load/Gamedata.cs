using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[System.Serializable]
public class GameData
{
    public float seconds;
    public int mins;
    public int hours;
    public int days = 1;
    public Vector3 playerPosition;
    public int[] items;
    public int[] itemcount;
    public Dictionary<Vector2Int, CropsTile> crops;
    public float musicSliderValue;
    public float masterSliderValue;

    public GameData()
    {
        playerPosition = Vector3.zero;
        seconds = 0;
        mins = 0;
        hours = 6;
        days = 1;
        items = new int[27];
        itemcount = new int[27];
        for(int i=0; i<27;i++)
        {
            items[i] = 0;
            itemcount[i] = 0;
        }
        crops = new Dictionary<Vector2Int, CropsTile>(); 
    }
}
