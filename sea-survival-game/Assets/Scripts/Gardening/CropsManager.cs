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
    public float damage;
    public CropsTile toDelete;
    public GameObject toDeleteGO;

    public bool Completed
    {
        get
        {
            if (crop == null) 
                return false;
            return growTimer >= crop.timeToGrow;
        }
    }
    internal void Harvested()
    {
        crop = null;
        growTimer = 0;
        damage = 0; 
        StageCount = 0;
        spriteRenderer.gameObject.SetActive(false);
    }
}



public class CropsManager : MonoBehaviour
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase plowableDirt;
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
        List<Vector2Int> cropsToWither = new List<Vector2Int>();
        foreach (CropsTile croptile in crops.Values) 
        {
            if (croptile.crop == null)
            {
                continue;
            }
            else
            {
                if (dayNightCycle.mins % 10 == 0)
                {
                    if(!croptile.Completed)
                    {
                        croptile.growTimer += 1; // ne szamoljon feleslegesen
                    }
                    if (croptile.Completed)
                    {
                        croptile.damage += 0.1f; //csak ha megnott azutan szamolja
                    }
                }
                if(croptile.damage >= croptile.crop.timeToWither)
                {
                    croptile.Harvested();  //ha damage nagyobb meghal a crop
                    cropsToWither.Add((Vector2Int)croptile.Pos);
                    Destroy(croptile.toDeleteGO);
                }
                if (croptile.growTimer == 1)
                {
                    cropTilemap.SetTile(croptile.Pos, plowed); //ha lerakod akkor meg ottmarad a tile azaz a mag es ezzel csereled le
                }
                if(croptile.Completed) //ez arra van, hogy segitsen az utolso leptetesnel, mert atrakta stage 3-ra a cropoot de nem futott le mert megvolt a completed feltetele
                {
                    croptile.spriteRenderer.gameObject.SetActive(true);
                    croptile.spriteRenderer.sprite = croptile.crop.sprites[croptile.StageCount];
                }
                if (!croptile.Completed && croptile.crop !=null) // lepteti es lecsereli a crop skinjeit
                {
                    if (croptile.growTimer >= croptile.crop.growthStageTimes[croptile.StageCount])
                    {
                        croptile.spriteRenderer.gameObject.SetActive(true);
                        croptile.spriteRenderer.sprite = croptile.crop.sprites[croptile.StageCount];
                        if (croptile.StageCount != croptile.crop.growthStageTimes.Count-1)
                        {
                            croptile.StageCount++;
                        }
                    }
                }
                
            }
        }
         foreach (Vector2Int pos in cropsToWither)
       {
        crops.Remove(pos);
        cropTilemap.SetTile((Vector3Int)pos, plowableDirt);
         
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
        crop.toDelete = crop;
        crops.Add((Vector2Int)position, crop); 
        //
        GameObject go = Instantiate(spriteCropPrefab);
        crop.toDeleteGO = go;
        crop.Pos = position;
        go.transform.position = cropTilemap.CellToWorld(position);
        go.transform.position -= Vector3.forward *0.65f;
        go.SetActive(false);
        crop.spriteRenderer = go.GetComponent<SpriteRenderer>();
        //
        cropTilemap.SetTile(position, plowed);
    }

    internal void PickUp(Vector3Int gridposition)
    {
        Vector2Int position = (Vector2Int)gridposition;
        if(crops.ContainsKey(position) == false)
        { 
           return;
        }
        CropsTile cropTile = crops[position];
        if(cropTile.Completed)
        {
            Item.SummonItem(cropTile.crop.yield, cropTilemap.CellToWorld(gridposition));
            cropTile.Harvested();
        }
    }
}
