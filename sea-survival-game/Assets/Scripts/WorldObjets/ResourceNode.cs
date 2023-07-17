using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewResourceNode", menuName = "World Object/Create New ResourceNode")]
public class ResourceNode : WorldObject
{
    public Item drop_1;
    public int drop_1_min;
    public int drop_1_max;
    public Item drop_2;
    public int drop_2_min;
    public int drop_2_max;
}
