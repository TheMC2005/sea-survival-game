using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCraftingRecipe", menuName = "Item/Create New Crafting")]
public class CraftingRecipe : ScriptableObject
{
    public List<Item> Items;
    public List<int> count;
    public Item output;
    public int outputCount;

    public void craft(){
        bool isCrafteble=true;
        for(int i=0; i<Items.Count; i++){
            if(InventoryManager.inventory.GetQuantity(Items[i])<count[i]){
                isCrafteble=false;
            }
        }
        if(isCrafteble){
            for(int i=0; i<Items.Count; i++){
                InventoryManager.inventory.RemoveItem(Items[i], count[i]);
            }
            for(int i=0; i<outputCount; i++){
                InventoryManager.inventory.AddItem(output);
            }
        }
    }
}
