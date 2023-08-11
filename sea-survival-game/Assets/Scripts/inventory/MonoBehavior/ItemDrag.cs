using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler
{
    private InventorySlot thisSlot;
    private static InventorySlot dragStart;
    private static InventorySlot dragEnd;
    [SerializeField] private RectTransform dragImage;
    Item empty;

    private void Start()
    {
        empty = Resources.Load("Empty") as Item;
        thisSlot = GetComponent<InventorySlot>();
        dragImage = GameObject.Find("DragImage").GetComponent<RectTransform>();
    }
    public void OnPointerEnter(PointerEventData eventData){
        dragEnd = thisSlot;
    }
    public void OnBeginDrag(PointerEventData eventData){
        dragStart = dragEnd;
        if(dragStart.slot.item != empty){
            dragImage.gameObject.SetActive(true);
        }
        else{
            dragImage.gameObject.SetActive(false);
        }
        dragStart.icon.sprite=empty.sprite;
        dragImage.gameObject.GetComponent<Image>().sprite=dragStart.slot.item.sprite;
    }
    public void OnEndDrag(PointerEventData eventData){
        dragImage.gameObject.SetActive(false);
        (InventoryManager.inventory.slot[dragStart.SlotID],InventoryManager.inventory.slot[dragEnd.SlotID])=(InventoryManager.inventory.slot[dragEnd.SlotID],InventoryManager.inventory.slot[dragStart.SlotID]);
        //InventoryManager.inventory.slot[dragStart.SlotID]
        //InventoryManager.inventory.slot[dragEnd.SlotID]
        //dragStart = null;
        //dragEnd = null;
        Hotbar.selSlot=InventoryManager.inventory.slot[Hotbar.selectedID];
        InventoryManager.LoadSlots(InventoryManager.inventory, InventoryManager.PlayerSlots);
    }
    public void OnDrag(PointerEventData eventData){
        dragImage.position=eventData.position;
    }
}
