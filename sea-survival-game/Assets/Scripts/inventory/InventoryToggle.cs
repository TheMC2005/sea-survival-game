using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
   public GameObject inventoryPanel;
    void Update()
    {
     if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(!inventoryPanel.activeSelf)
            {
                inventoryPanel.SetActive(true);
            }
            else
            { inventoryPanel.SetActive(false); }
        }
    }
}
