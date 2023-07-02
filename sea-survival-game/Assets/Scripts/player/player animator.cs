using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeranimator : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var dx = Input.GetAxisRaw("Horizontal");
        var dy = Input.GetAxisRaw("Vertical");
        anim.SetFloat("horizontal", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("vertical", Input.GetAxisRaw("Vertical"));
    }
}
