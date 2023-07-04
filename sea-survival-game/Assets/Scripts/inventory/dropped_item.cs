using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class dropped_item : MonoBehaviour
{
    public Item item;
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.sprite;
    }
    void OnTriggerEnter2D(Collider2D targetObj)
    {
        if (targetObj.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
