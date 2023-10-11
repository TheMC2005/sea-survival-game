using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;
//emlekezteto a majmunnak mindennek csinalj adatbazist mint ahogy a tilebasenek is csinaltal koszi

[System.Serializable]
public class CropsTile
{
    public double growTimer;
    public int StageCount;
    public int waterTime;
    public int timerToDirt;
    [JsonIgnore]
    public Crop crop;
    public int? cropsID;
    public Vector3Int Pos;
    public float damage = 0;
    [JsonIgnore]
    public TileBase tileBase;
    public string tileBaseName;
    [JsonIgnore]
    public SpriteRenderer spriteRenderer;
    [JsonIgnore]
    public GameObject toDeleteGO;
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
        timerToDirt = 0;
        cropsID = null;
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
        timerToDirt = 0;
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

public class CropsManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] TileBase PlowedDirt;
    [SerializeField] TileBase PlowableDirt;
    [SerializeField] TileBase AlreadySeeded;
    [SerializeField] TileBase WateredPlowedDirt;
    [SerializeField] Tilemap cropTilemap;
    [SerializeField] CropDatabaseObject cropDatabaseObject;
    public TileMapReadController controller;
    [SerializeField] DayNightCycle dayNightCycle;
    [SerializeField] GameObject spriteCropPrefab;
    public float spread = 0.65f;
    [JsonConverter(typeof(DictionaryVector2IntJsonConverter))]
    Dictionary<Vector2Int, CropsTile> crops;
    
    private void Start()
    {
        
    }
    private void Awake()
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
        List<Vector3Int> positionsToRevert = new List<Vector3Int>();

        foreach (CropsTile croptile in crops.Values)
        {
            if (croptile.tileBase == PlowedDirt) //lehet az a baj, hogy nem croptiletilebasename mert azt nem mented hulye
            {
                if (dayNightCycle.mins % 10 == 0)
                {
                    croptile.timerToDirt++;
                }
                if (croptile.timerToDirt > 10)
                {
                    positionsToRevert.Add(croptile.Pos);
                }
            }
        }
        foreach (Vector3Int position in positionsToRevert)
        {
            if (crops.TryGetValue((Vector2Int)position, out CropsTile cropToDelete))
            {
                RevertCrop(position, cropToDelete);
            }
        }

        foreach(CropsTile cropTile in crops.Values)
        {
            if (dayNightCycle.mins % 10 == 0 && cropTile.waterTime !=0)
            {
                if(cropTile.tileBase == (cropTile.tileBase == WateredPlowedDirt || cropTile.tileBase == AlreadySeeded) && cropTile.waterTime !=0)
                     cropTile.waterTime--;
            }
            if(cropTile.waterTime <= 0)
            {
                if (cropTile.crop != null)
                {
                    cropTile.tileBase = AlreadySeeded;
                    cropTile.tileBaseName = "AlreadySeeded";
                    cropTilemap.SetTile(cropTile.Pos, AlreadySeeded);
                }
                if(cropTile.crop == null)
                {
                    cropTile.tileBase = PlowedDirt;
                    cropTile.tileBaseName = "PlowedDirt";
                    cropTilemap.SetTile(cropTile.Pos, PlowedDirt);
                }
            }
        }
    }


    public void Tick()
    {
        
        foreach (CropsTile croptile in crops.Values) 
        {
            Debug.Log("*");
            if (croptile.crop == null)
            {
                continue;
            }
            else
            {
                Debug.Log("***");
                if (dayNightCycle.mins % 10 == 0)
                    {
                        if (!croptile.Completed && (croptile.tileBase == WateredPlowedDirt || croptile.tileBase == AlreadySeeded) && croptile.waterTime >0 && !croptile.ReadyToWither)
                        {
                            croptile.growTimer += 1; // ne szamoljon feleslegesen
                            croptile.damage = 0;
                            croptile.timerToDirt = 0;
                    }
                        if (croptile.Completed || croptile.waterTime == 0 )
                        {
                           if(!croptile.crop.isSeasonialCrop && croptile.damage < croptile.crop.timeToWither) //a seasonal crop nem fog rohadni csak nem nol
                            croptile.damage += 0.1f; //csak ha megnott azutan szamolja

                        }
                    }
                    if(croptile.crop.isSeasonialCrop)
                    {
                    if (croptile.growTimer == 2)
                    {
                        if (croptile.waterTime > croptile.crop.growthStageTimes[0])
                        {
                            cropTilemap.SetTile(croptile.Pos, WateredPlowedDirt);
                            croptile.tileBase = WateredPlowedDirt;
                            croptile.tileBaseName = "WateredPlowedDirt";
                        }
                        else
                        {
                            cropTilemap.SetTile(croptile.Pos, AlreadySeeded);
                            croptile.tileBase = AlreadySeeded;
                            croptile.tileBaseName = "AlreadySeeded";
                        }
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
                    if (croptile.growTimer == 2)
                    {
                        if (croptile.waterTime > croptile.crop.growthStageTimes[0])
                        {
                            cropTilemap.SetTile(croptile.Pos, WateredPlowedDirt);
                            croptile.tileBase = WateredPlowedDirt;
                            croptile.tileBaseName = "WateredPlowedDirt";
                        }
                        else
                        {
                            cropTilemap.SetTile(croptile.Pos, AlreadySeeded);
                            croptile.tileBase = AlreadySeeded;
                            croptile.tileBaseName = "AlreadySeeded";
                        }
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
        crops[(Vector2Int)position].cropsID = toSeed.cropID;
        CropsTile croptile = crops[(Vector2Int)position];
        if (croptile.waterTime > croptile.crop.growthStageTimes[0])
        {
            //Do nothing
        }
        else
        {
            croptile.waterTime = croptile.crop.growthStageTimes[0];
        }
        croptile.tileBase = AlreadySeeded;
        croptile.tileBaseName = "AlreadySeeded";

    } 
    private void CreatePlowedTile(Vector3Int position)
    {
        CropsTile crop = new CropsTile();
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
        cropTilemap.SetTile(position, PlowedDirt);
        crop.tileBase = PlowedDirt;
        crop.tileBaseName = "PlowedDirt";
    }
    public void RevertCrop(Vector3Int position, CropsTile crop)
    {
        crops.Remove((Vector2Int)position);
        cropTilemap.SetTile(position, PlowableDirt);

        if (crop.toDeleteGO != null)
        {
            Destroy(crop.toDeleteGO);
        }

    }
    public void CheckIfInDictionary(Vector3Int gridposition)
    {
        Vector2Int position = (Vector2Int)gridposition;

        if (crops.ContainsKey(position))
        {
            CropsTile crop = crops[position];

            // Now you can access details of the crop tile
            double cropTimer = crop.growTimer;
            Vector3Int pos1 = crop.Pos;
            Debug.Log("Crop Timer: " + cropTimer);
            Debug.Log("Dirt:" + crop.timerToDirt);
            Debug.Log(crop.tileBaseName);
            Debug.Log("D:"+ crop.damage);
            Debug.Log("Water:" + crop.waterTime);
        }
        else
        {
            Debug.Log("No crop found at position: " + position);
        }
    }

    public Crop GetTileCrop(Vector3Int position)
    {
        return crops[(Vector2Int)position].crop;
    }
    internal void PickUp(Vector3Int gridposition)
    {

        Vector2Int position = (Vector2Int)gridposition;
        if(crops.ContainsKey(position) == false)
        { 
           return;
        }
        CropsTile cropTile = crops[position];
        if(cropTile.cropsID == null)
        {
            return;
        }
        if (cropTile.ReadyToWither)
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
                    cropTile.tileBase = PlowedDirt;
                    cropTile.tileBaseName = "PlowedDirt";
                    cropTile.waterTime = 0;
                    cropTilemap.SetTile((Vector3Int)position, PlowedDirt);
                }
            }
        }
    }
    public void LoadData(GameData data)
    {
        crops.Clear();

        foreach (var kvp in data.crops)
        {
            Vector2Int position = kvp.Key;
            CropsTile dataCropsTile = kvp.Value;
            CropsTile cropTile = new CropsTile
            {
                growTimer = dataCropsTile.growTimer,
                Pos = dataCropsTile.Pos,
                StageCount = dataCropsTile.StageCount,
                timerToDirt = dataCropsTile.timerToDirt,
                crop = dataCropsTile.crop,
                damage = dataCropsTile.damage,
                tileBaseName = dataCropsTile.tileBaseName,
                cropsID = dataCropsTile.cropsID,
                waterTime = dataCropsTile.waterTime,
        };
            if(cropTile.cropsID.HasValue)
            {
                if (cropTile.ReadyToWither && !cropTile.crop.isSeasonialCrop)
                {
                    int regularInt = cropTile.cropsID.Value;
                    cropTile.crop = cropDatabaseObject.GetCrop[regularInt];
                    //gameobjectes resz
                    GameObject go = Instantiate(spriteCropPrefab);
                    cropTile.toDeleteGO = go;
                    go.transform.position = cropTilemap.CellToWorld(cropTile.Pos);
                    go.transform.position -= Vector3.forward * 0.65f;
                    cropTile.spriteRenderer = go.GetComponent<SpriteRenderer>();
                    cropTile.spriteRenderer.gameObject.SetActive(true);
                    cropTile.spriteRenderer.sprite = cropTile.crop.witheredSprite;

                    //gameobjectes resz vege
                    crops.Add(position, cropTile);
                }
                else
                {
                    int regularInt = cropTile.cropsID.Value;
                    cropTile.crop = cropDatabaseObject.GetCrop[regularInt];
                    //gameobjectes resz
                    GameObject go = Instantiate(spriteCropPrefab);
                    cropTile.toDeleteGO = go;
                    go.transform.position = cropTilemap.CellToWorld(cropTile.Pos);
                    go.transform.position -= Vector3.forward * 0.65f;
                    cropTile.spriteRenderer = go.GetComponent<SpriteRenderer>();
                    cropTile.spriteRenderer.gameObject.SetActive(true);
                    cropTile.spriteRenderer.sprite = cropTile.crop.sprites[cropTile.StageCount];

                    //gameobjectes resz vege
                    crops.Add(position, cropTile);
                }
            }
            else
            {
                GameObject go = Instantiate(spriteCropPrefab);
                cropTile.toDeleteGO = go;
                go.transform.position = cropTilemap.CellToWorld(cropTile.Pos);
                go.transform.position -= Vector3.forward * 0.65f;
                cropTile.spriteRenderer = go.GetComponent<SpriteRenderer>();
                cropTile.spriteRenderer.gameObject.SetActive(false);
                cropTile.crop = null;
                crops.Add(position, cropTile);
            }
            
            
            if (cropTile.tileBaseName == "PlowedDirt")
            {
                cropTilemap.SetTile(cropTile.Pos, PlowedDirt);
                cropTile.tileBase = PlowedDirt;
            }
            if (cropTile.tileBaseName == "AlreadySeeded")
            {
                cropTilemap.SetTile(cropTile.Pos, AlreadySeeded);
                cropTile.tileBase = AlreadySeeded;
            }
            if (cropTile.tileBaseName == "PlowableDirt")
            {
                cropTilemap.SetTile(cropTile.Pos, PlowableDirt);
                cropTile.tileBase = PlowableDirt;
            }
            if (cropTile.tileBaseName == "WateredPlowedDirt")
            {
                cropTilemap.SetTile(cropTile.Pos, WateredPlowedDirt);
                cropTile.tileBase = PlowableDirt;
            }
            Debug.Log("Crop added"+ position);
          
            
        }

    }


    public void SaveData(GameData data)
    {
        data.crops.Clear();

        foreach (var kvp in crops)
        {
            Vector2Int position = kvp.Key;
            CropsTile cropTile = kvp.Value;
            CropsTile dataCropsTile = new CropsTile
            {
                growTimer = cropTile.growTimer,
                Pos = cropTile.Pos,
                StageCount = cropTile.StageCount,
                timerToDirt = cropTile.timerToDirt,
                crop = cropTile.crop,
                damage = cropTile.damage,
                tileBaseName = cropTile.tileBaseName,
                cropsID = cropTile.cropsID,
                waterTime = cropTile.waterTime,
    };

            data.crops.Add(position, dataCropsTile);
        }
    }

    internal void PlowOutCrop(Vector3Int gridposition)
    {
        Vector2Int position = (Vector2Int)gridposition;
        CropsTile cropTile = crops[position];
        if (cropTile.crop != null)
        {
            cropTile.tileBase = PlowedDirt;
            cropTile.tileBaseName = "PlowedDirt";
            cropTilemap.SetTile(gridposition, PlowedDirt);
            cropTile.waterTime = 0;
            cropTile.Harvested();
        }
    }

    internal void WaterTile(Vector3Int gridposition)
    {
        Vector2Int position = (Vector2Int)gridposition;
        CropsTile cropTile = crops[position];
        cropTile.waterTime = 21;
        cropTilemap.SetTile(gridposition, WateredPlowedDirt);
        cropTile.tileBase = WateredPlowedDirt;
        cropTile.tileBaseName = "WateredPlowedDirt";
    }
}
