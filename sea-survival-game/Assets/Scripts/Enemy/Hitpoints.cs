using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitpoints : MonoBehaviour
{
    public int hp;
    private int oldhp;
    void Start()
    {
        oldhp=hp;
    }
    void Update()
    {
        if (hp<=0){die();}
        Debug.Log(hp);
        if (oldhp!=hp){}
    }

    IEnumerator imunity()
    {
        yield return new WaitForSeconds(2);
    }

    private void die()
    {
        Destroy(this.gameObject);
    }
}
