using System;
using System.Collections;
using System.Collections.Generic;
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
    public bool ReadyToWither
    {
        get
        {
            return damage >= crop.timeToWither;
        }
    }
}

public class CropsManager : MonoBehaviour
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase plowableDirt;
    [SerializeField] Tilemap cropTilemap;
    [SerializeField] DayNightCycle dayNightCycle;
    [SerializeField] GameObject spriteCropPrefab;
    public float spread = 0.65f;
    Dictionary<Vector2Int, CropsTile> crops;
    List<Vector2Int> cropsToWither = new List<Vector2Int>();


    private void Start()
    {
        crops = new Dictionary<Vector2Int, CropsTile>();
    }
    public void Wither(CropsTile cropsTile)
    {
        cropsTile.Harvested();
        cropsToWither.Add((Vector2Int)cropsTile.Pos);
        Destroy(cropsTile.toDeleteGO);
    }
    public void KillCropFromDictionary()
    {
        foreach (Vector2Int pos in cropsToWither)
        {
            Debug.Log("Lefutok");
            crops.Remove(pos);
        }
        cropsToWither.Clear();
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
                    if(!croptile.Completed)
                    {
                        croptile.growTimer += 1; // ne szamoljon feleslegesen
                    }
                    if (croptile.Completed)
                    {
                        croptile.damage += 0.1f; //csak ha megnott azutan szamolja

                    }
                }
                if (croptile.growTimer == 1)
                {
                    cropTilemap.SetTile(croptile.Pos, plowed); //ha lerakod akkor meg ottmarad a tile azaz a mag es ezzel csereled le
                }
                if (croptile.ReadyToWither)
                {
                    Wither(croptile);
                }
                if (croptile.Completed) //ez arra van, hogy segitsen az utolso leptetesnel, mert atrakta stage 3-ra a cropoot de nem futott le mert megvolt a completed feltetele
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
        if (cropsToWither.Count !=0)
        {
            KillCropFromDictionary();
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
            
            for(int i = 0; i<cropTile.crop.dropAmount; i++)
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
        }
    }
}
