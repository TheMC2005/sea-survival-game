using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] public TMP_Text text;
    [SerializeField] public Image icon;
    public Item item;
    public int count;

    void Start()
    {
        
    }
    private void Awake()
    {
        item = Resources.Load("Empty") as Item;
    }
    public void Set()
    {
        icon.sprite = item.sprite;
        if ((count == 0) || (item.maxq == 1))
        {
            text.SetText(string.Empty);
        }
        else
        {
            text.SetText(count.ToString());
        }
    }
}
