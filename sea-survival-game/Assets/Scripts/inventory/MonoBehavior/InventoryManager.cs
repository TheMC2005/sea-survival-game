using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static Inventory inventory;
    [SerializeField] List<Slot> buttons;
    public static List<Slot> PlayerSlots;
    void Start()
    {
        PlayerSlots = buttons;
        inventory = new Inventory(27, PlayerSlots);
        LoadSlots(inventory);
    }

    public static void LoadSlots(Inventory inventory)
    {
        for (int i = 0; i < inventory.nos; i++)
        {
            inventory.slot[i].Set();
        }
    }
}
