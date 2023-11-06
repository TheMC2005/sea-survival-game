using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    private bool IsOpen;
    RectTransform rt;
    void Start()
    {
        IsOpen = false;
        rt =this.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(0,-83);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !GameManagerSingleton.Instance.IsPaused){
            if(IsOpen){
                rt.anchoredPosition = new Vector2(0, -83);
                IsOpen = false;
            }
            else{
                rt.anchoredPosition = new Vector2(0, 0);
                IsOpen = true;
            }
        }
    }
}
