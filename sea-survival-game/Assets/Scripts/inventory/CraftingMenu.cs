using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingMenu : MonoBehaviour
{
    [SerializeField] CraftingRecipe recipe;
    [SerializeField] Image icon;
    void Start() {
        icon.sprite = recipe.output.sprite;
    }
    public void Craft(){
        recipe.craft();
        InventoryManager.LoadSlots(InventoryManager.inventory);
    }
}
