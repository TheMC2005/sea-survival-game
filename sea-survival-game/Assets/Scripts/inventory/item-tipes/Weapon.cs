using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Item/Create New Weapon")]
public class Weapon : Item
{
    public GameObject Attack;
    public ToolAction onAction;
    public ToolAction onItemUsed;
}
