using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Tool Action/Attack")]
public class Attack : ToolAction
{
    public override bool OnApply(Vector2 worldPoint){return true;}
    public override void OnItemUsed(Weapon usedItem)
    {
        GameObject attack = Instantiate(usedItem.Attack,Aim.aim.transform);
    }
}
