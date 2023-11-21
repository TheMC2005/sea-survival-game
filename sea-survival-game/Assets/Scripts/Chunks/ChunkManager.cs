using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//hashsettekkel optomalizáld a tilestonotremoveot, meg lehet a chunks-ot is gyorsaság + checkplayerproximityvel kezdj valamit mert néha spikeol
public class ChunkManager : MonoBehaviour
{
    public Tilemap tilemap; // Reference to your Tilemap
    public TileBase waterTile; // Reference to your water tile
    public TileBase check;
    public int chunkSize = 16; // Size of each chunk
    public float playerProximityThreshold = 64f; // Proximity threshold to the player

    public List<TilemapChunk> chunks = new List<TilemapChunk>();
    public List<TileBase> tilesToNotRemove = new List<TileBase>();
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
            yield return new WaitForSeconds(3.0f); // Adjust the interval as needed
        }
    }

    void DivideTilemapIntoChunks()
    {
        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.x; x < bounds.xMax; x += chunkSize)
        {
            for (int y = bounds.y; y < bounds.yMax; y += chunkSize)
            {
                int chunkWidth = Mathf.Min(chunkSize, bounds.xMax - x);
                int chunkHeight = Mathf.Min(chunkSize, bounds.yMax - y);

                Vector3Int chunkPosition = new Vector3Int(x, y, 0);
                Vector3 chunkMiddlePoint = tilemap.GetCellCenterWorld(new Vector3Int(x + chunkWidth / 2, y + chunkHeight / 2, 0));

                TilemapChunk chunk = new TilemapChunk(chunkPosition, chunkMiddlePoint, new Vector3Int(chunkWidth, chunkHeight, 1));
                chunk.SetTiles(tilemap.GetTilesBlock(new BoundsInt(chunk.position, chunk.size)));
                chunks.Add(chunk);
            }
        }
    }
    public void ShowMiddlePoint()
    {
        foreach (var chunk in chunks)
        {
            Vector3Int chunkmid = Vector3Int.RoundToInt(chunk.middlePoint);
            tilemap.SetTile(chunkmid,check);
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
            //ShowMiddlePoint();
            chunk.UpdateTilemap(tilemap);
        }
    }

    void UpdateTiles(TilemapChunk chunk, TileBase tileToReplace, TileBase replacementTile)
    {
        for (int i = 0; i < chunk.tiles.Length; i++)
        {
            if (tilesToNotRemove.Contains(chunk.tiles[i]))
            {
                continue;
            }

            if (chunk.tiles[i] == tileToReplace)
            {
                chunk.tiles[i] = replacementTile;
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

    public TilemapChunk(Vector3Int position, Vector3 middlePoint, Vector3Int size)
    {
        this.position = position;
        this.middlePoint = middlePoint;
        this.size = size;
        this.tiles = new TileBase[size.x * size.y];
    }

    public void SetTiles(TileBase[] tiles)
    {
        if (tiles.Length == this.tiles.Length)
        {
            Array.Copy(tiles, this.tiles, tiles.Length);
        }
    }

    public void UpdateTilemap(Tilemap tilemap)
    {
        BoundsInt bounds = new BoundsInt(position, size);
        tilemap.SetTilesBlock(bounds, tiles);
    }
}
