using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Move : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var dx = Input.GetAxisRaw("Horizontal");
        var dy = Input.GetAxisRaw("Vertical");
        if ((dx != 0) || (dy != 0))
        {
            if ((dx != 0) && (dy != 0))
            {
                rb.velocity = new Vector2(Convert.ToSingle(dx * 6 / Math.Sqrt(2)), Convert.ToSingle(dy * 6 / Math.Sqrt(2)));
            }
            else
            {
                rb.velocity = new Vector2(dx * 6, dy * 6);
            }
        }
        else
        {
            rb.velocity = new Vector2(0.0f, 0.0f);
        }
    }
}
