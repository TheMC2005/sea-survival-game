using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public int maxq;
    public Sprite sprite;

    public static void SummonItem(Item item, Vector2 pos)
    {
        GameObject itemobj = Resources.Load("Item") as GameObject;
        GameObject dropeditem = Instantiate(itemobj, pos, new Quaternion(0,0,0,1));
        dropeditem.GetComponent<dropped_item>().item = item;
    }
}
