using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour//, IDataPersistence
{
    public static Slot selSlot;
    public static Hotbar[] slots = new Hotbar[9];
    [SerializeField] int id;

    private void Start()
    {
        slots[id]=this;
        if(slots[0]!=null){
            slots[0].OnLeftClick();
        }
    }
    public void OnLeftClick(){
        selSlot=gameObject.GetComponent<Slot>();
        for(int i=0; i<9; i++){
            slots[i].gameObject.GetComponent<Image>().color = new Color32(255,255,255,255);
        }
        gameObject.GetComponent<Image>().color = new Color32(255,255,255,100);
    }
/*
    public void LoadData(GameData data)
    {
        slots = data.slots;
    }

    public void SaveData(GameData data)
    {
        data.slots = slots;
    }
*/
}
