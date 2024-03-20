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
