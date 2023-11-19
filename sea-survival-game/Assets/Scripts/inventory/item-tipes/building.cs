using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuilding", menuName = "Item/Create New Building")]
public class Building : Item
{
    public GameObject building;
    public ToolAction onItemUsed;
    public ToolAction onTileMapAction;
}
