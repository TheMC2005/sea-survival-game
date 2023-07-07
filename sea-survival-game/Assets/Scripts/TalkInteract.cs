using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkInteract : Interactable
{
    public override void Interact(Character character)
    {
        //Csak teszteltem, hogy mennyire altalanosithato a code, de mukodik ha akarod ilyen karaktereket is rakhatunk majd bele
        Debug.Log("Megölöm magam");
    }
}
