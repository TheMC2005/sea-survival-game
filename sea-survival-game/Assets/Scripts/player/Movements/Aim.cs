using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Aim : MonoBehaviour
{
    public static GameObject aim;
    public Camera cam;
    private Rigidbody2D rb;
    GameObject pl;
    Vector2 mPos;
    Vector2 pos;
    float r;

    void Start()
    {
        pl = GameObject.Find("player");
        aim = this.gameObject;
    }

    void Update()
    {
        mPos = cam.ScreenToWorldPoint(Input.mousePosition);
        pos = new Vector2(transform.position.x, transform.position.y);
    }

    void FixedUpdate()
    {
        Vector2 lDir = mPos - pos;
        float d = Mathf.Atan2(lDir.y, lDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, d);
        r = transform.localEulerAngles.z;
    }
}
