using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipe : ScriptableObject
{
    public List<Item> Items;
    public List<int> count;
    public Item output;
    public int outputCount;
}
