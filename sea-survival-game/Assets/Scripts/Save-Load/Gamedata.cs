using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
    public float seconds;
    public int mins;
    public int hours;
    public int days = 1;
    public Vector3 playerPosition;
   // public Hotbar[] slots;
   // public Inventory inventory;
   // public List<Slot> PlayerSlots;
    public GameData()
    {
        playerPosition = Vector3.zero;
        seconds = 0;
        mins = 0;
        hours = 6;
        days = 1;
     //   slots = new Hotbar[9];
     //   inventory = new Inventory(27, PlayerSlots);
    }
}
