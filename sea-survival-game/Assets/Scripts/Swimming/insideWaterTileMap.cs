using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class insideWaterTileMap : MonoBehaviour
{
    //references
    [SerializeField] CharacterController2D characterController;
    [SerializeField] CheckPlayerPosition checkPlayer;
    //components
    [SerializeField] TileBase insideWaterTileBase;
    private float updateInterval = 0.05f; 
    private float lastUpdateTime;

    private void Start()
    {
        lastUpdateTime = Time.time;
    }
    private void Update()
    {
        if (Time.time - lastUpdateTime < updateInterval)
        {
            return; 
        }

        lastUpdateTime = Time.time;

        if (checkPlayer.getTileBaseUnderPlayer() == insideWaterTileBase)
        {
            Debug.Log("Player entered the waterTile");
            GameManagerSingleton.Instance.isSwimming = true;
            GameManagerSingleton.Instance.inShallow = false;
        }
        else
        {
            Debug.Log("Player exited the WaterTile");
            GameManagerSingleton.Instance.inShallow = true;
        }

        if (!GameManagerSingleton.Instance.isSwimming)
        {
            characterController.speed = Mathf.MoveTowards(characterController.speed, 3, Time.deltaTime);
        }
    }
}
