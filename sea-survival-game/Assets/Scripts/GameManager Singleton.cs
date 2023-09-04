using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSingleton : MonoBehaviour
{
  public static GameManagerSingleton Instance { get; private set; }
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
