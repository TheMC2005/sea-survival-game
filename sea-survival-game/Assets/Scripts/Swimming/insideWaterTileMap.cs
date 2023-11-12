using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class insideWaterTileMap : MonoBehaviour
{
    [SerializeField] CharacterController2D characterController;
    public float _speedAfterExitingWater;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered the waterTilemap!");
            GameManagerSingleton.Instance.isSwimming = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player exited the waterTilemap!");
            GameManagerSingleton.Instance.isSwimming = false;
            characterController.speed = 0.8f;
        }
    }
    private void Update()
    {
        if (!GameManagerSingleton.Instance.isSwimming)
        {
          _speedAfterExitingWater = Mathf.MoveTowards(characterController.speed, 3, 2*Time.deltaTime);
            characterController.speed = _speedAfterExitingWater;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

    }
}
