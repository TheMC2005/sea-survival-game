using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IDataPersistence
{
    public static Inventory inventory;
    [SerializeField] private ItemDatabaseObject database; 
    public static ItemDatabaseObject itemDatabase;
    [SerializeField] List<Slot> buttons;
    public static List<Slot> PlayerSlots;
    private int[] items;
    private int[] itemcount;
    void Start()
    {
        PlayerSlots = buttons;
        inventory = new Inventory(27, PlayerSlots);
        LoadSlots(inventory);
        itemDatabase = database;
        items = new int[27];
        itemcount = new int[27];
    }

    public static void LoadSlots(Inventory inventory)
    {
        for (int i = 0; i < inventory.nos; i++)
        {
            inventory.slot[i].Set();
        }
    }

    public void LoadData(GameData data)
    {
        items = data.items;
        itemcount = data.itemcount;
        for (int i = 0; i<items.Length; i++){
            inventory.slot[i].item = itemDatabase.GetItem[items[i]];
            PlayerSlots[i].count=itemcount[i];
        }
    }

    public void SaveData(GameData data)
    {
        for (int i = 0; i<items.Length; i++){
            data.items[i]=InventoryManager.itemDatabase.GetID[PlayerSlots[i].item];
            data.itemcount[i]=PlayerSlots[i].count;
        }
    }
}
