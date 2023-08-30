using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class CropsTile
{
    public int growTimer;
    public int StageCount;
    public Crop crop;
    public SpriteRenderer spriteRenderer;
    public Vector3Int Pos;
    public float damage = 0;
    public CropsTile toDelete;
    public GameObject toDeleteGO;
    public int timerToDirt;
    public TileBase tileBase;
    //public Season season
    public bool Completed
    {
        get
        {
            if (crop == null) 
                return false;
            return growTimer >= crop.timeToGrow;
        }
    }
    /*
     public bool isSeasonOver
    {
    get
    {
     if(a.Season != currentSeason)
       return false;
    else
    return true;
    }
    }
    */
    internal void Harvested()
    {
        crop = null;
        growTimer = 0;
        damage = 0; 
        StageCount = 0;
        spriteRenderer.gameObject.SetActive(false);
    }
    internal void _Harvested()
    {
        growTimer = 0;
        StageCount = 0;
    }
    internal void SeasonalCropHarvest()
    {
        StageCount = 2;
        growTimer = crop.growthStageTimes[2];
    }
    public bool ReadyToWither
    {
        get
        {
            if (crop == null)
                return false;
            return damage >= crop.timeToWither;
        }
    }
}

public class CropsManager : MonoBehaviour
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase plowableDirt;
    [SerializeField] TileBase alreadySeeded;
    [SerializeField] Tilemap cropTilemap;
    public TileMapReadController controller;
    [SerializeField] DayNightCycle dayNightCycle;
    [SerializeField] GameObject spriteCropPrefab;
    public float spread = 0.65f;
    Dictionary<Vector2Int, CropsTile> crops;
    
    private void Start()
    {
        crops = new Dictionary<Vector2Int, CropsTile>();
    }
    public void Wither(CropsTile cropsTile)
    {
        cropsTile._Harvested();
        cropsTile.spriteRenderer.sprite = cropsTile.crop.witheredSprite;
    }
    public void Tick2()
    {
        
        List<Vector3Int> positionsToRevert = new List<Vector3Int>();  // Keep track of positions to revert
        List<CropsTile> cropsToRevert = new List<CropsTile>();  // Keep track of crops to revert
      
        foreach (CropsTile croptile in crops.Values)
        {
            if (croptile.tileBase == plowed)
            {
                if (dayNightCycle.mins % 10 == 0)
                {
                    croptile.timerToDirt++;
                }
                if (croptile.timerToDirt > 10)
                {
                    positionsToRevert.Add(croptile.Pos);
                    cropsToRevert.Add(croptile.toDelete);
                }
            }
        }
            for (int i = 0; i < positionsToRevert.Count; i++)
            {
                RevertCrop(positionsToRevert[i], cropsToRevert[i]);
            }
            positionsToRevert.Clear();
            cropsToRevert.Clear();
    }
    
    public void Tick()
    {
        
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
                        if (!croptile.Completed)
                        {
                            croptile.growTimer += 1; // ne szamoljon feleslegesen
                        }
                        if (croptile.Completed)
                        {
                            croptile.damage += 0.1f; //csak ha megnott azutan szamolja

                        }
                    }
                    if(croptile.crop.isSeasonialCrop)
                    {
                    if (croptile.growTimer == 1)
                    {
                        cropTilemap.SetTile(croptile.Pos, alreadySeeded); //ha lerakod akkor meg ottmarad a tile azaz a mag es ezzel csereled le
                        croptile.tileBase = alreadySeeded;
                    }
                     
                    if (!croptile.Completed && croptile.crop != null || !croptile.ReadyToWither) // lepteti es lecsereli a crop skinjeit
                    {
                        if (croptile.growTimer >= croptile.crop.growthStageTimes[croptile.StageCount])
                        {
                            croptile.spriteRenderer.gameObject.SetActive(true);
                            croptile.spriteRenderer.sprite = croptile.crop.sprites[croptile.StageCount];
                            if (croptile.StageCount != croptile.crop.growthStageTimes.Count - 1)
                            {
                               croptile.StageCount++;
                            }
                        }
                    }
                    if(croptile.growTimer == croptile.crop.timeToGrow)
                    {
                        croptile.spriteRenderer.sprite = croptile.crop.sprites[4];
                    }
                    /*
                     if(isSeasonOver)
                    {
                    Wither(croptile);
                    }
                    */
                }
                else
                { 
                    if (croptile.growTimer == 1)
                    {
                        cropTilemap.SetTile(croptile.Pos, alreadySeeded); //ha lerakod akkor meg ottmarad a tile azaz a mag es ezzel csereled le
                        croptile.tileBase = alreadySeeded;
                    }
                    if (!croptile.ReadyToWither)
                    {
                        if (croptile.Completed) //ez arra van, hogy segitsen az utolso leptetesnel, mert atrakta stage 3-ra a cropoot de nem futott le mert megvolt a completed feltetele
                        {
                            croptile.spriteRenderer.gameObject.SetActive(true);
                            croptile.spriteRenderer.sprite = croptile.crop.sprites[croptile.StageCount];
                        }
                    }
                    if (croptile.ReadyToWither)
                    {
                        Wither(croptile);
                    }
                    
                    if (!croptile.Completed && croptile.crop != null || !croptile.ReadyToWither) // lepteti es lecsereli a crop skinjeit
                    {
                        if (croptile.growTimer >= croptile.crop.growthStageTimes[croptile.StageCount])
                        {
                            croptile.spriteRenderer.gameObject.SetActive(true);
                            croptile.spriteRenderer.sprite = croptile.crop.sprites[croptile.StageCount];
                            if (croptile.StageCount != croptile.crop.growthStageTimes.Count - 1)
                            {
                                croptile.StageCount++;
                            }
                        }
                    }

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
    public Crop GetTileCrop(Vector3Int position)
    {
        return crops[(Vector2Int)position].crop;
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
        crop.tileBase = plowed;
    }
    public void RevertCrop(Vector3Int position, CropsTile crop)
    {
        crops.Remove((Vector2Int)position);
        cropTilemap.SetTile(position, plowableDirt);

        if (crop.toDeleteGO != null)
        {
            Destroy(crop.toDeleteGO);
        }

    }

    internal void PickUp(Vector3Int gridposition)
    {

        Vector2Int position = (Vector2Int)gridposition;
        if(crops.ContainsKey(position) == false)
        { 
           return;
        }
        CropsTile cropTile = crops[position];
        if (cropTile.tileBase == plowed)
        {
            return;
        }
        else
        {
            int randomDropAmount = UnityEngine.Random.Range(1, cropTile.crop.maxDropAmount+2);
            if (cropTile.crop.isSeasonialCrop && cropTile.Completed)
            {
                for (int i = 0; i < randomDropAmount; i++)
                {
                    Vector3 positionItem = cropTilemap.CellToWorld(gridposition);
                    if (UnityEngine.Random.value < 0.5f)
                    {
                        positionItem.x -= spread * UnityEngine.Random.value - spread * 2;
                    }
                    else
                        positionItem.x += spread * UnityEngine.Random.value - spread / 2;

                    if (UnityEngine.Random.value < 0.5f)
                    {
                        positionItem.y -= spread * UnityEngine.Random.value - spread * 2;
                    }
                    else
                        positionItem.y += spread * UnityEngine.Random.value - spread / 2;
                    Item.SummonItem(cropTile.crop.yield, positionItem);
                }
                cropTile.SeasonalCropHarvest();
            }
            else
            {
                if (cropTile.Completed && !cropTile.crop.isSeasonialCrop)
                {

                    for (int i = 0; i < randomDropAmount; i++)
                    {
                        Vector3 positionItem = cropTilemap.CellToWorld(gridposition);
                        if (UnityEngine.Random.value < 0.5f)
                        {
                            positionItem.x -= spread * UnityEngine.Random.value - spread * 2;
                        }
                        else
                            positionItem.x += spread * UnityEngine.Random.value - spread / 2;

                        if (UnityEngine.Random.value < 0.5f)
                        {
                            positionItem.y -= spread * UnityEngine.Random.value - spread * 2;
                        }
                        else
                            positionItem.y += spread * UnityEngine.Random.value - spread / 2;
                        Item.SummonItem(cropTile.crop.yield, positionItem);
                    }
                    cropTile.Harvested();
                    cropTile.tileBase = plowed;
                    cropTilemap.SetTile((Vector3Int)position, plowed);
                }
            }
        }
    }

    internal void PlowWitheredGround(Vector3Int gridposition)
    {
        Vector2Int position = (Vector2Int)gridposition;
        if (crops.ContainsKey(position) == false)
        {
            return;
        }
        CropsTile cropTile = crops[position];
        if(cropTile.ReadyToWither && cropTile.crop != null)
        {
            cropTile.tileBase = plowed;
            cropTilemap.SetTile(gridposition, plowed);
            cropTile.crop = null;
            cropTile.damage = 0;
            cropTile.spriteRenderer.gameObject.SetActive(false);
        }
       
    }
}
