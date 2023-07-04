using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 2f;
    Vector2 motionVector;
    public Vector2 LastMotionVector;
    Animator animator;
    public bool moving;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        motionVector = new Vector2(horizontal,vertical);
        animator.SetFloat("horizontal", horizontal);
        animator.SetFloat("vertical", vertical);
        moving = horizontal != 0 || vertical != 0;
        animator.SetBool("moving", moving);
        if (horizontal !=0 || vertical !=0)
        {
            LastMotionVector = new Vector2(horizontal, vertical).normalized;
            animator.SetFloat("lastHorizontal", horizontal);
            animator.SetFloat("lastVertical", vertical);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = motionVector * speed;
    }
}