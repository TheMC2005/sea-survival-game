using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static Inventory inventory;
    [SerializeField] List<InventorySlot> buttons;
    public static List<InventorySlot> PlayerSlots;
    void Start()
    {
        inventory = new Inventory(27);
        PlayerSlots = buttons;
        LoadSlots(inventory, PlayerSlots);
    }

    public static void LoadSlots(Inventory inventory, List<InventorySlot> buttons)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Set(inventory.slot[i], i);
        }
    }
}
