using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] Image icon;
    Slot slot;
    public void Set(Slot slot)
    {
        this.slot=slot;
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
