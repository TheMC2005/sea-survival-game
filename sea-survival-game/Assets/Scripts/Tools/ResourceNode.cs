using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class ResourceNode : ToolHit
{
    [SerializeField] Item pickUpDrop;
    [SerializeField] int dropCount = 5;
    [SerializeField] float spread = 0.65f;
    [SerializeField] ResourceNodeType nodeType;
    [SerializeField] int nodeHP;
    [SerializeField] int xpToGive;

    //local bools
    public bool isForaging;
    public bool isMobility;
    public bool isForge;
    public bool isCrafting;
    public override void Hit(int level)
    {
        for (int i = 0; i < level; i++)
        {
            nodeHP--;
            if (nodeHP != 0)
            {
                Debug.Log("hit");
            }
            else
            {
                while (dropCount > 0)
                {
                    dropCount -= 1;
                    Vector3 position = transform.position;
                    position.x += spread * UnityEngine.Random.value - spread / 2;
                    position.y += spread * UnityEngine.Random.value - spread / 2;
                    Item.SummonItem(pickUpDrop, position);
                }
                Destroy(gameObject);
                if (isCrafting)
                {
                    Stats.Instance.GiveCraftingxp(xpToGive);
                }
                if (isForge)
                {
                    Stats.Instance.GiveForgexp(xpToGive);
                }
                if (isForaging)
                {
                    Stats.Instance.GiveForagingxp(xpToGive);
                }
                if (isMobility)
                {
                    Stats.Instance.GiveMobilityxp(xpToGive);
                }
            }
            if(nodeHP == 0)
            {
                break;
            }
        }

        
    }
    public override bool CanBeHit(List<ResourceNodeType> canBeHit)
    {
        return canBeHit.Contains(nodeType);
    }
}
