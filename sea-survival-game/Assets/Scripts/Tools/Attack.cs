using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName ="Data/Tool Action/Attack")]
public class Attack : ToolAction
{
    public override bool OnApply(Vector2 worldPoint){if (EventSystem.current.IsPointerOverGameObject()){return false;} else{ return true;}}
    public override void OnItemUsed(Weapon usedItem)
    {
        Debug.Log("asd");
        GameObject attack = Instantiate(usedItem.Attack,Aim.aim.transform.position,Aim.aim.transform.rotation);
    }
}
