using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RefreshAllTiles : MonoBehaviour
{
    public Tilemap tilemap; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            RefreshTiles();
        }
        if(Input.GetKeyDown(KeyCode.U)) {
            ResetHungerBar();
            Debug.Log("Reset");
        }
    }

    void RefreshTiles()
    {
        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            tilemap.RefreshTile(position);
        }
        Debug.Log("RefreshAllTiles Script lefutot");
    }
    void ResetHungerBar()
    {
        Stats.Instance.food = 100;
    }
}
