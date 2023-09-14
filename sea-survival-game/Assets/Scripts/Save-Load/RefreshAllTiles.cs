using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RefreshAllTiles : MonoBehaviour
{
    public Tilemap tilemap; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RefreshTiles();
        }
    }

    void RefreshTiles()
    {
        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            tilemap.RefreshTile(position);
        }
        Debug.Log("Bazdmeg");
    }
}
