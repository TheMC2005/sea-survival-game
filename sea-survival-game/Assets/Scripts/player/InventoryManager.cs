using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static Inventory inventory;
    void Start()
    {
        inventory = new Inventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
