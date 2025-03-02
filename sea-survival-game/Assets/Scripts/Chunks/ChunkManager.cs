using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase waterTile; 
    public float playerProximityThreshold = 128f;
    public float checkInterval = 2f; 

    private Transform player;

    void Start()
    {
        player = Camera.main.transform;
        StartCoroutine(CheckPlayerProximityCoroutine());
    }

    IEnumerator CheckPlayerProximityCoroutine()
    {
        while (true)
        {
            ConvertTilesToWaterInRange(); 
            yield return new WaitForSeconds(checkInterval);
        }
    }

    void ConvertTilesToWaterInRange()
    {
        Vector3 playerPos = player.position;  

        Vector3 min = playerPos - new Vector3(playerProximityThreshold, playerProximityThreshold, 0);
        Vector3 max = playerPos + new Vector3(playerProximityThreshold, playerProximityThreshold, 0);

        Vector3Int minTilePos = tilemap.WorldToCell(min);
        Vector3Int maxTilePos = tilemap.WorldToCell(max);

        for (int x = minTilePos.x; x <= maxTilePos.x; x++)
        {
            for (int y = minTilePos.y; y <= maxTilePos.y; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                Vector3 tileWorldPos = tilemap.CellToWorld(tilePos);
                float distanceToPlayer = (playerPos - tileWorldPos).sqrMagnitude;

                if (distanceToPlayer <= playerProximityThreshold * playerProximityThreshold)
                {               
                    if (tilemap.GetTile(tilePos) == null)
                    {
                        tilemap.SetTile(tilePos, waterTile);
                    }
                }
            }
        }
    }
}
