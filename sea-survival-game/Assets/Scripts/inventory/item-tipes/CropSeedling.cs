using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewItem", menuName = "Item/Create New Cropseedling")]
public class CropSeedling : Item
{
    [Header("Crops")]
    public Crop crop;
    public bool isCropSeed;
    public ToolAction onAction;
    public ToolAction onTileMapAction;
    public ToolAction onItemUsed;
    public ToolAction plowWitheredGround;
    public ToolAction plowOutCrops;
}
