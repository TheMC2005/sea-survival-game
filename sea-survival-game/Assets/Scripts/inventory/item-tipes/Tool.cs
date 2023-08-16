using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolType { Axe, Pickaxe, Hoe};

[CreateAssetMenu(fileName = "NewTool", menuName = "Item/Create New Tool")]
public class Tool : Item
{
    public ToolType toolType;
    public int toolLevel;
}