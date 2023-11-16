using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class insideWaterTileMap : MonoBehaviour
{
    [SerializeField] CharacterController2D characterController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered the waterTilemap!");
            GameManagerSingleton.Instance.inShallow = false;
            GameManagerSingleton.Instance.isSwimming = true;
            characterController.speed = 0.8f;

        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player exited the waterTilemap!");
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        GameManagerSingleton.Instance.isSwimming = true;
        GameManagerSingleton.Instance.inShallow = false;
    }
    private void Update()
    {
        if (!GameManagerSingleton.Instance.isSwimming)
        {
            characterController.speed = Mathf.MoveTowards(characterController.speed, 3, Time.deltaTime);
        }
    }
}
