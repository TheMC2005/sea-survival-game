using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class summonwall : MonoBehaviour
{
    [SerializeField] GameObject wall;
    [SerializeField] GameObject boss;
    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag=="Player"){
            wall.SetActive(true);
            boss.SetActive(true);
        }
    }
}
