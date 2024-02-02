using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CheckPlayerPosition : MonoBehaviour
{

    public Tilemap tilemap;

    public TileBase getTileBaseUnderPlayer()
    {
        Vector3Int playerCellPosition = tilemap.WorldToCell(GameManagerSingleton.Instance.player.transform.position);
        TileBase tile = tilemap.GetTile(playerCellPosition);
        Debug.Log("Under player: "+tile.name);
        return tile;
    }
}
