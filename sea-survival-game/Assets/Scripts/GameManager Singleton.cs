using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSingleton : MonoBehaviour
{
    public GameObject InGameMenu;
    public bool isSwimming;
    public static GameManagerSingleton Instance { get; private set; }
    public bool IsPaused
    {
        get
        {
            return InGameMenu.activeSelf;
        }
    }
    private void Awake()
    {
        if(Instance !=null) 
        { 
           Debug.LogError("There is multiple instances of a singleton");
        }
        Instance = this;
    }
    public GameObject player;
}
