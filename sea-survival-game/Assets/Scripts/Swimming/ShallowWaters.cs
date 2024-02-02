using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShallowWaters : MonoBehaviour
{
    //reference
    public CheckPlayerPosition checkPlayer;
    //components
    [SerializeField] TileBase shallowWaterTilebase;
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
        if (checkPlayer.getTileBaseUnderPlayer() == shallowWaterTilebase) 
        {
            Debug.Log("Player entered the shallowWaters");
            GameManagerSingleton.Instance.isSwimming = false;
            GameManagerSingleton.Instance.inShallow = true;
        }
        else
        {
            Debug.Log("Player exited the shallowWaters!");
            GameManagerSingleton.Instance.inShallow = false;
        }
    }
}
