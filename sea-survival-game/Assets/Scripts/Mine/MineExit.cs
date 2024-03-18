using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MineExit : MonoBehaviour
{
    //GO
    public GameObject mineEntrance;
    //References
    public Volume ppv;

    public void ExitMine()
    {
        GameManagerSingleton.Instance.player.transform.position = mineEntrance.transform.position;
        GameManagerSingleton.Instance.insideMine = false;
    }
}
