using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceLaser : MonoBehaviour
{
    [SerializeField] private int damage;
    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag=="Player"){
            col.gameObject.GetComponent<PlayerHP>().DealDamage(damage);
        }
    }
}
