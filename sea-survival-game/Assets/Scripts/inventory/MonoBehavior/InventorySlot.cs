using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] public Image icon;
    public Slot slot;
    public int SlotID;
    public void Set(Slot slot, int id)
    {
        this.slot=slot;
        this.SlotID=id;
        icon.sprite = slot.item.sprite;
        if ((slot.count == 0) || (slot.item.maxq == 1))
        {
            text.SetText(string.Empty);
        }
        else
        {
            text.SetText(slot.count.ToString());
        }
    }
}
