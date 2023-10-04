using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class dropped_item : MonoBehaviour
{
    public Item item;
    public int amount = 1;
    Transform player;
    [SerializeField] float speed = 5f; // ezt meg lehet valtoztatom de lehet jo
    [SerializeField] float pickUpDistance = 1.15f; // Ez szerintem jol be van love
    [SerializeField] float TimeToLeave = 100f; //mennyi ido alatt tunik el a cucc a foldrol item despawn time roviden

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.sprite;
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Transform>();
    }
    private void Update()
    {
        TimeToLeave -= Time.deltaTime;
        if (TimeToLeave < 0)
        {
            Destroy(gameObject);
        }
        float distance = Vector3.Distance(transform.position, player.position); //object es player kozotti distance
        if ((distance > pickUpDistance)||(!InventoryManager.inventory.IsFreeSpaceFor(item)))
        {
            return; // ha nagyobb a distance nem veszi fel csak vissza kuldi
        }
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        if (distance < 0.1f)
        {
            for(int i = 0; i < amount; i++)
            { 
                InventoryManager.inventory.AddItem(item);
            }
            InventoryManager.LoadSlots(InventoryManager.inventory);
            Destroy(gameObject);
        }
    }
}
