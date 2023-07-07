using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainerInteract : Interactable
{
    [SerializeField] GameObject closed;
    [SerializeField] GameObject opened;
    [SerializeField] bool isOpen;
    //ide rakhatunk majd minden cuccot pl furnace es hasonlok csak majd kulon script kell nekik
    // ez csak ilyen barrelek meg mas kinyithato dolgokra hasznalhato
    public override void Interact(Character character)
    {
        if(isOpen == false)
        {
            isOpen = true;
            closed.SetActive(false);
            opened.SetActive(true);
        }
    }
}
 