using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

public class Inventory
{
    private int nos;//number of slots
    Item empty = Resources.Load("Empty") as Item;

    public List<Slot> slot = new List<Slot>();
    public Inventory(int nos)
    {
        this.nos = nos;
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
            if ((slot[i].item == item) && (slot[i].count < item.maxq))
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

    public void RemoveItem(Item item, int count)
    {
        int n = count;
        if (this.GetQuantity(item) < count)
            return;
        for (int i = 0; i < nos; i++)
        {
            if (slot[i].item == item)
            {
                if (slot[i].count > n)
                {
                    slot[i].count -= n;
                    return;
                }
                if (slot[i].count == n)
                {
                    slot[i].count = 0;
                    slot[i].item = empty;
                    return;
                }
                if (slot[i].count < n)
                {
                    n -= slot[i].count;
                    slot[i].count = 0;
                    slot[i].item = empty;
                }
            }
        }
    }

    public int GetQuantity(Item item)
    {
        int n = 0;
        for (int i = 0; i < nos; i++)
        {
            if (slot[i].item == item)
                n += slot[i].count;
        }
        return n;
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
