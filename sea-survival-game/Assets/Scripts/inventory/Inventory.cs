using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory
{
    public class Slot
    {
        public Item item;
        public int count;
    }

    public Slot[] slot = new Slot[27];

    public void AddItem(Item item)
    {
        for(int i=0; i<27; i++)
        {
            if (slot[i].item = item)
            {
                if (slot[i].count<item.maxq)
                    slot[i].count += 1;
            }
        }
    }
}
