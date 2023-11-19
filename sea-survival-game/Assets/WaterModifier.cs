using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterModifier : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase targetTile;
    public TileBase replacementTile;

    public int areaSize = 128;

    void Start()
    {
        StartCoroutine(ModifyTilesCoroutine());
    }

    IEnumerator ModifyTilesCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f); 

        while (true)
        {
            ModifyTilesAroundTarget();
            yield return wait;
            
        }
    }
    private void ModifyTilesAroundTarget()
    {
        //ha tölunk a tile distance < 128 akkor a tile az wa'er ha nem akkor pedig null
        throw new NotImplementedException();
    }
}
