using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public static Slot selSlot;
    public static Hotbar[] slots = new Hotbar[9];
    public static int selectedID;
    [SerializeField] int id;

    private void Start()
    {
        slots[id]=this;
    }
    public void OnLeftClick(){
        selSlot=InventoryManager.inventory.slot[id];
        selectedID=id;
        for(int i=0; i<9; i++){
            slots[i].gameObject.GetComponent<Image>().color = new Color32(255,255,255,255);
        }
        gameObject.GetComponent<Image>().color = new Color32(255,255,255,100);
    }
}
