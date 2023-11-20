using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkManager : MonoBehaviour
{
    public Tilemap tilemap; // Reference to your Tilemap
    public TileBase waterTile; // Reference to your water tile
    public int chunkSize = 16; // Size of each chunk
    public float playerProximityThreshold = 64f; // Proximity threshold to the player

    public List<TilemapChunk> chunks = new List<TilemapChunk>();
    public List<TileBase> tilesToNotRemove = new List<TileBase>();
    private Transform player;

    void Start()
    {
        player = Camera.main.transform;
        DivideTilemapIntoChunks();

        // Start the coroutine for periodic checks
        StartCoroutine(CheckPlayerProximityCoroutine());
    }

    // Coroutine for periodic checks
    IEnumerator CheckPlayerProximityCoroutine()
    {
        while (true)
        {
            CheckPlayerProximity();
            yield return new WaitForSeconds(3.0f); // Adjust the interval as needed
        }
    }

    void DivideTilemapIntoChunks()
    {
        BoundsInt bounds = tilemap.cellBounds;

        foreach (var position in bounds.allPositionsWithin)
        {
            int x = position.x;
            int y = position.y;

            if (x % chunkSize == 0 && y % chunkSize == 0)
            {
                int chunkWidth = Mathf.Min(chunkSize, bounds.size.x - x);
                int chunkHeight = Mathf.Min(chunkSize, bounds.size.y - y);

                Vector3Int chunkPosition = new Vector3Int(x, y, 0);
                Vector3 chunkMiddlePoint = tilemap.GetCellCenterWorld(chunkPosition);

                TilemapChunk chunk = new TilemapChunk(chunkPosition, chunkMiddlePoint, new Vector3Int(chunkWidth, chunkHeight, 1));
                chunk.CreateGameObject(tilemap);
                chunks.Add(chunk);
            }
        }
    }

    void CheckPlayerProximity()
    {
        foreach (var chunk in chunks)
        {
            float distanceToPlayer = (player.position - chunk.middlePoint).sqrMagnitude;

            if (distanceToPlayer > playerProximityThreshold * playerProximityThreshold)
            {
                UpdateTiles(chunk, waterTile, null);
            }
            else
            {
                UpdateTiles(chunk, null, waterTile);
            }
        }
    }

    void UpdateTiles(TilemapChunk chunk, TileBase tileToReplace, TileBase replacementTile)
    {
        BoundsInt chunkBounds = new BoundsInt(chunk.position, chunk.size);
        TileBase[] tiles = tilemap.GetTilesBlock(chunkBounds);

        for (int i = 0; i < tiles.Length; i++)
        {
            if (tilesToNotRemove.Contains(tiles[i]))
            {
                continue;
            }

            if (tiles[i] == tileToReplace)
            {
                tiles[i] = replacementTile;
            }
        }

        tilemap.SetTilesBlock(chunkBounds, tiles);
    }
}

public class TilemapChunk
{
    public Vector3Int position;
    public Vector3 middlePoint;
    public Vector3Int size;

    public TilemapChunk(Vector3Int position, Vector3 middlePoint, Vector3Int size)
    {
        this.position = position;
        this.middlePoint = middlePoint;
        this.size = size;
    }

    public void CreateGameObject(Tilemap originalTilemap)
    {
        GameObject chunkObject = new GameObject("Chunk_" + position.x + "_" + position.y);
        chunkObject.transform.position = middlePoint;

        Tilemap chunkTilemap = chunkObject.AddComponent<Tilemap>();
        chunkTilemap.tileAnchor = originalTilemap.tileAnchor;

        TilemapRenderer chunkRenderer = chunkObject.AddComponent<TilemapRenderer>();
        chunkRenderer.sharedMaterial = originalTilemap.GetComponent<TilemapRenderer>().sharedMaterial;

        TileBase[] tiles = originalTilemap.GetTilesBlock(new BoundsInt(position, size));
        chunkTilemap.SetTilesBlock(new BoundsInt(Vector3Int.zero, size), tiles);
    }
}
