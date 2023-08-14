using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler
{
    private Slot thisSlot;
    private static Slot dragStart;
    private static Slot dragEnd;
    [SerializeField] private RectTransform dragImage;
    Item empty;

    private void Start()
    {
        empty = Resources.Load("Empty") as Item;
        thisSlot = GetComponent<Slot>();
        dragImage = GameObject.Find("DragImage").GetComponent<RectTransform>();
    }
    public void OnPointerEnter(PointerEventData eventData){
        dragEnd = thisSlot;
    }
    public void OnBeginDrag(PointerEventData eventData){
        dragStart = dragEnd;
        if(dragStart.item != empty){
            dragImage.gameObject.SetActive(true);
        }
        else{
            dragImage.gameObject.SetActive(false);
        }
        dragStart.icon.sprite=empty.sprite;
        dragStart.text.SetText(string.Empty);
        dragImage.gameObject.GetComponent<Image>().sprite=dragStart.item.sprite;
    }
    public void OnEndDrag(PointerEventData eventData){
        dragImage.gameObject.SetActive(false);
        if(dragEnd!=dragStart){
            if(dragStart.item==dragEnd.item){
                if(dragStart.count+dragEnd.count<=dragStart.item.maxq){
                    dragEnd.count+=dragStart.count;
                    dragStart.item=empty;
                    dragStart.count=0;
                }
                else{
                    int a = dragEnd.item.maxq-dragEnd.count;
                    dragEnd.count=dragEnd.item.maxq;
                    dragStart.count-=a;
                }
            }
            else{
                (dragStart.item,dragEnd.item)=(dragEnd.item,dragStart.item);
                (dragStart.count,dragEnd.count)=(dragEnd.count,dragStart.count);
            }
        }
        dragEnd.Set();
        dragStart.Set();
    }
    public void OnDrag(PointerEventData eventData){
        dragImage.position=eventData.position;
    }
}
