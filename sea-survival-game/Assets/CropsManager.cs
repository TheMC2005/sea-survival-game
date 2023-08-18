using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class CropsTile
{
    public int growTimer;
    public int StageCount;
    public Crop crop;
    public SpriteRenderer spriteRenderer;
    public Vector3Int Pos;
}
public class CropsManager : MonoBehaviour
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] Tilemap cropTilemap;
    [SerializeField] DayNightCycle dayNightCycle;
    [SerializeField] GameObject spriteCropPrefab;
    Dictionary<Vector2Int, CropsTile> crops;
    private void Start()
    {
        crops = new Dictionary<Vector2Int, CropsTile>();
    }
    public void Tick()
    {
        foreach(CropsTile croptile in crops.Values) 
        {
            if (croptile.crop == null)
            {
                continue;
            }
            else
            {
                if (dayNightCycle.mins % 10 == 0)
                {
                    croptile.growTimer += 1;
                    Debug.Log(croptile.growTimer);
                }
                if (croptile.growTimer == 1)
                {
                    cropTilemap.SetTile(croptile.Pos, plowed);
                }
               
                if (croptile.growTimer >= croptile.crop.growthStageTimes[croptile.StageCount])
                {
                    croptile.spriteRenderer.gameObject.SetActive(true);
                    croptile.spriteRenderer.sprite = croptile.crop.sprites[croptile.StageCount];
                    croptile.StageCount++;
                }

                if (croptile.growTimer >= croptile.crop.timeToGrow)
                {
                    Debug.Log("Done growing");
                    croptile.crop = null;
                }
            }
        }
    }
    public bool Check(Vector3Int position)
    {
        return crops.ContainsKey((Vector2Int)position);
    }
    public void Plow(Vector3Int position)
    {
        if(crops.ContainsKey((Vector2Int)position))
        {
            return;
        }
        CreatePlowedTile(position);
    }
    public void Seed(Vector3Int position, Crop toSeed)
    {
        cropTilemap.SetTile(position, toSeed.seededTile);
        crops[(Vector2Int)position].crop = toSeed;
    }

    private void CreatePlowedTile(Vector3Int position)
    {
        CropsTile crop = new CropsTile();
        crops.Add((Vector2Int)position, crop); 
        //
        GameObject go = Instantiate(spriteCropPrefab);
        crop.Pos = position;
        go.transform.position = cropTilemap.CellToWorld(position);
        go.transform.position -= Vector3.forward *0.65f;
        go.SetActive(false);
        crop.spriteRenderer = go.GetComponent<SpriteRenderer>();
        //
        cropTilemap.SetTile(position, plowed);
    }
}
