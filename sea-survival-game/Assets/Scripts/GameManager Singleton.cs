using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSingleton : MonoBehaviour
{
  public static GameManagerSingleton Instance;
    private void Awake()
    {
        Instance = this;
    }
    public GameObject player;
}
