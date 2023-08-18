using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Progress;

public class ToolsPlayerController : MonoBehaviour
{
    CharacterController2D character;
    Rigidbody2D rb;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TileMapReadController tileMapReadController;
    [SerializeField] float maxDistance = 2.3f;
    [SerializeField] CropsManager cropsManager;
    [SerializeField] TileData plowableTiles;
    Vector3Int selectedTilePosition;
    bool selectable;
    public static bool GetToolType(ToolType toolType)
    {
        if (Hotbar.selSlot.item is Tool)
        {
            Tool toolItem = (Tool)Hotbar.selSlot.item;
            if (toolItem.toolType == toolType)
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }
    private void Awake()
    {
        character = GetComponent<CharacterController2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {

        SelectTile();
        CanSelectCheck();
        Marker();
        if (Input.GetMouseButtonDown(0)) 
        {
            if(UseToolWorld() == true)
            {
                return;
            }
            UseToolGrid();  
        }
    }
    private void SelectTile()
    {
        selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
    }
    void CanSelectCheck()
    {
        Vector2 characterPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;
        markerManager.Show(selectable);
    }
    private void Marker()
    {
        markerManager.markedCellPosition = selectedTilePosition;
    }

    private bool UseToolWorld()
    {
        Vector2 position = rb.position + character.LastMotionVector*offsetDistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);
        foreach(Collider2D collider in colliders)
        {
            ToolHit hit = collider.GetComponent<ToolHit>();
            if(hit != null)
            {
                hit.Hit();
                return true;
            }
        }
        return false;
    }
    private void UseToolGrid()
    {
       if(selectable == true)
        {
            TileBase tileBase = tileMapReadController.GetTileBase(selectedTilePosition);
            TileData tileData = tileMapReadController.GetTileData(tileBase);
            if (tileData != plowableTiles /*|| !GetToolType(ToolType.Hoe)*/)
            {
                return;
            }
            else
            {
                if (cropsManager.Check(selectedTilePosition))
                {
                     if(Hotbar.selSlot.item.isCropSeed)
                    {
                        Item item = Hotbar.selSlot.item;
                        cropsManager.Seed(selectedTilePosition, item.crop);
                       
                    }
                   
                }
                else
                {
                    cropsManager.Plow(selectedTilePosition);
                }
            }
        }      
    }
   
}

