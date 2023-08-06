using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public static Slot selItem;
    [SerializeField] Image image;
    public static Image itemIcon;
    void Start()
    {
        selItem=new Slot();
        itemIcon = image;
    }
    public static void loadHotbar(Slot slot, Image image){
        image.sprite = slot.item.sprite;
    }
}
