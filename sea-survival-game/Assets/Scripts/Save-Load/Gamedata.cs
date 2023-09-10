using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public int[] items;
    public int[] itemcount;

    public GameData()
    {
        playerPosition = Vector3.zero;
        items=new int[27];
        itemcount=new int[27];
        for (int i=0; i<27;i++){
            items[i]=0;
            itemcount[i]=0;
        }
    }
}
