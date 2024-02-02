using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int hp;

    void Update()
    {
        if (hp<=0){die();}
    }
    private void die(){
        Debug.Log("Meghaltal");
        hp = 100000;
    }
}
