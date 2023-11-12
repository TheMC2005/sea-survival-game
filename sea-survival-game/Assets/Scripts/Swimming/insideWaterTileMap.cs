using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class insideWaterTileMap : MonoBehaviour
{
    Tilemap waterTilemap;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered the waterTilemap!");
            GameManagerSingleton.Instance.isSwimming = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player exited the waterTilemap!");
            GameManagerSingleton.Instance.isSwimming = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
}
