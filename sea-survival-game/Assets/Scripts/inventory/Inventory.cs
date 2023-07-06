using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory
{
    private int nos = 27;
    Item empty = Resources.Load("Empty") as Item;
    public class Slot
    {
        Item empty = Resources.Load("Empty") as Item;
        public Item item;
        public int count;
        public Slot() 
        {
            item = empty;
            count = 0; 
        }
    }

    public List<Slot> slot = new List<Slot>();
    public Inventory()
    {
        for (int i = 0; i < nos; i++)
        {
            Slot ns = new Slot();
            slot.Add(ns);
        }
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < nos; i++)
        {
            if ((slot[i].item.id == item.id) && (slot[i].count < item.maxq))
            {
                slot[i].count += 1;
                return;
            }
        }
        for (int i = 0; i < nos; i++) 
        {
            if (slot[i].item == empty)
            {
                slot[i].item = item;
                slot[i].count = 1;
                return;
            }
        }
    }

    public bool IsFreeSpaceFor(Item item)
    {
        for(int i = 0; i < nos; i++)
        {
            if(((slot[i].item == item) && (slot[i].count < item.maxq)) || (slot[i].item==empty))
                    return true;
        }
        return false;
    }

    public void SortInv()
    {
        for(int i = 0; i<nos; i++)
        {
            if(slot[i].item == empty)
            {
                int j = i;
                while ((slot[j].item == empty)&&(j<27)) j++;
                slot[i] = slot[j];
                slot[j].item = empty;
                slot[j].count = 0;
            }
        }
    }
}
