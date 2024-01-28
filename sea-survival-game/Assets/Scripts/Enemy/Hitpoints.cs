using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitpoints : MonoBehaviour
{
    public int hp;
    void Update()
    {
        if (hp<=0){die();}
    }

    private void die()
    {
        Destroy(this.gameObject);
    }
}
