using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSingleton : MonoBehaviour
{
    public GameObject InGameMenu;
    public bool isSwimming;
    public bool inShallow;
    
    public static GameManagerSingleton Instance { get; private set; }
    public bool IsPaused
    {
        get
        {
            return InGameMenu.activeSelf;
        }
    }
    public bool isMoving
    {
        get
        {
            if(inShallow == true && isSwimming == false)
            {
                return false;
            }
            if (inShallow == false && isSwimming == true)
            {
                return false;
            }
            if(inShallow == false && isSwimming == false)
            {
                return true;
            }
            else
                return true;
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
