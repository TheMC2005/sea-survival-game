using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test2 : MonoBehaviour
{
    Image pic;
    void Start()
    {
        pic=gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        pic.sprite=Hotbar.selSlot.item.sprite;
    }
}
