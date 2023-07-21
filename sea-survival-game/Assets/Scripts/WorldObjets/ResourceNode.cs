using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewResourceNode", menuName = "World Object/Create New ResourceNode")]
public class ResourceNode : WorldObject
{
    public List<Item> drops;
    public List<int> amount;
    public ToolType toolType;
    public int minToolLevel;
}
