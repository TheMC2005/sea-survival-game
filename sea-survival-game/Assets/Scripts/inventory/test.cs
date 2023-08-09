using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Item item;
    public Camera cam;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Item.SummonItem(item, cam.ScreenToWorldPoint(Input.mousePosition));
    }
}
