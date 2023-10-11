using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEditor.Progress;
/*
 * A jovobeli magamnak, ha epp keresed, hogy a tooloknal mert nem akar lefutni a script akkor emlekez arra, hogy vagy a use tool worldnel vagy a gridnel megkell hivnod
 * a function meg az itemnel csinalj hozza egy masik toolactiont
 * kb igy
 * if(item.plowWitheredGround == null) { return; }
            bool complete2 = item.plowWitheredGround.OnApplyToTileMap(selectedTilePosition, tileMapReadController, item);
 * */
public class ToolsPlayerController : MonoBehaviour
{
    CharacterController2D character;
    Rigidbody2D rb;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TileMapReadController tileMapReadController;
    [SerializeField] float maxDistance = 2.3f;
    [SerializeField] ToolAction onTilePickUp;
    [SerializeField] CropsManager cropsManager;

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
        if (Input.GetKeyDown(KeyCode.R))
        {

            cropsManager.CheckIfInDictionary(selectedTilePosition);
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
        Item item = Hotbar.selSlot.item;

        if (Hotbar.selSlot.item is Tool)
        {
            Tool tool = (Tool)item;
            if (tool == null)
                return false;
            if (tool.onAction == null)
                return false;
            bool complete = tool.onAction.OnApply(position);

            if (complete == true)
            {
                if (tool.onItemUsed != null)
                {
                    tool.onItemUsed.OnItemUsed(item);
                }
            }

            return complete;
        }
        if (Hotbar.selSlot.item is CropSeedling)
        {
            CropSeedling cropseed = (CropSeedling)item;
            if (cropseed == null)
                return false;
            if (cropseed.onAction == null)
                return false;
            bool complete = cropseed.onAction.OnApply(position);

            if (complete == true)
            {
                if (cropseed.onItemUsed != null)
                {
                    cropseed.onItemUsed.OnItemUsed(item);
                }
            }

            return complete;
        }
        return false;
    }
    private void UseToolGrid()
    {
       if(selectable == true)
        {
            Item item = Hotbar.selSlot.item;
            if (Hotbar.selSlot.item is Item)
            {
                if (item.itemName == " ")
                {
                    PickUpTile();
                    return;
                }
            }
            if (Hotbar.selSlot.item is CropSeedling)
            {
                CropSeedling cropseed = (CropSeedling)item;
                if (cropseed.onTileMapAction == null) { return; }
                bool complete = cropseed.onTileMapAction.OnApplyToTileMap(selectedTilePosition, tileMapReadController, cropseed);
                if (complete == true)
                {
                    if (cropseed.onItemUsed != null)
                    {
                        cropseed.onItemUsed.OnItemUsed(cropseed);
                    }
                }
                if (cropseed.plowOutCrops == null) { return; }
                bool complete3 = cropseed.plowOutCrops.OnApplyToTileMap(selectedTilePosition, tileMapReadController, cropseed);
            }
            if (Hotbar.selSlot.item is Tool)
            {
                Tool tool = (Tool)item;
                if (tool.onTileMapAction == null) { return; }
                bool complete = tool.onTileMapAction.OnApplyToTileMap(selectedTilePosition, tileMapReadController, tool);
                if (complete == true)
                {
                    if (tool.onItemUsed != null)
                    {
                        tool.onItemUsed.OnItemUsed(tool);
                    }
                }
            }
        }
        
    }

    private void PickUpTile()
    {
        if(onTilePickUp == null)
        {
            return;     
        }
        onTilePickUp.OnApplyToTileMap(selectedTilePosition, tileMapReadController, (Item)null);
    }
}