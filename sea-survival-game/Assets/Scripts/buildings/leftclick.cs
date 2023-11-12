using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class leftclick : MonoBehaviour
{
    [SerializeField] building sample;
    [SerializeField] Tilemap tilemap;
    [SerializeField] MarkerManager marker;
    void Update()
    {
        if((Input.GetMouseButtonDown(0))&&(!EventSystem.current.IsPointerOverGameObject())){
            if((Hotbar.selSlot.item.GetType()==sample.GetType())&&(tilemap.GetTile(marker.markedCellPosition)==null)){
                Debug.LogWarning("adjk");
            }
        }
    }
}
