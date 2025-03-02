using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkManager : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase waterTile;
    public TileBase check;
    public int chunkSize = 16;
    public float playerProximityThreshold = 64f;

    private Dictionary<Vector3Int, TilemapChunk> chunks = new Dictionary<Vector3Int, TilemapChunk>();
    private HashSet<TileBase> tilesToNotRemove = new HashSet<TileBase>();
    private Transform player;

    void Start()
    {
        player = Camera.main.transform;
        DivideTilemapIntoChunks();
        StartCoroutine(CheckPlayerProximityCoroutine());
    }

    IEnumerator CheckPlayerProximityCoroutine()
    {
        while (true)
        {
            CheckPlayerProximity();
            yield return new WaitForSeconds(5f);
        }
    }

    void DivideTilemapIntoChunks()
    {
        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.x; x < bounds.xMax; x += chunkSize)
        {
            for (int y = bounds.y; y < bounds.yMax; y += chunkSize)
            {
                Vector3Int chunkPosition = new Vector3Int(x, y, 0);
                Vector3 chunkMiddlePoint = tilemap.GetCellCenterWorld(new Vector3Int(x + chunkSize / 2, y + chunkSize / 2, 0));

                TilemapChunk chunk = new TilemapChunk(chunkPosition, chunkMiddlePoint, new Vector3Int(chunkSize, chunkSize, 1));
                chunk.SetTiles(tilemap.GetTilesBlock(new BoundsInt(chunk.position, chunk.size)));
                chunks[chunkPosition] = chunk;
            }
        }
    }

    void CheckPlayerProximity()
    {
        Vector3 playerPos = player.position;

        foreach (var kvp in chunks)
        {
            TilemapChunk chunk = kvp.Value;
            float distanceToPlayer = (playerPos - chunk.middlePoint).sqrMagnitude;

            bool shouldBeWater = distanceToPlayer > playerProximityThreshold * playerProximityThreshold;

            if (shouldBeWater)
            {
                chunk.RestoreNullTiles();
                chunk.UpdateTilemap(tilemap);
                
            }
            else
            {
                chunk.ReplaceNullTilesWith(waterTile);
                chunk.UpdateTilemap(tilemap);
            }
        }
    }
}

public class TilemapChunk
{
    public Vector3Int position;
    public Vector3 middlePoint;
    public Vector3Int size;
    public TileBase[] tiles;
    private TileBase[] originalTiles;

    public TilemapChunk(Vector3Int position, Vector3 middlePoint, Vector3Int size)
    {
        this.position = position;
        this.middlePoint = middlePoint;
        this.size = size;
        this.tiles = new TileBase[size.x * size.y];
        this.originalTiles = new TileBase[size.x * size.y];
    }

    public void SetTiles(TileBase[] newTiles)
    {
        Array.Copy(newTiles, this.tiles, newTiles.Length);
        Array.Copy(newTiles, this.originalTiles, newTiles.Length);
    }

    public void ReplaceNullTilesWith(TileBase replacement)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] == null)
            {
                tiles[i] = replacement;
            }
        }
    }

    public void RestoreNullTiles()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (originalTiles[i] == null)
            {
                tiles[i] = null; // Restore only null tiles
            }
        }
    }

    public void UpdateTilemap(Tilemap tilemap)
    {
        tilemap.SetTilesBlock(new BoundsInt(position, size), tiles);
    }
}
