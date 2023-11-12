using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour, IDataPersistence
{
    Rigidbody2D rb;
    [SerializeField] public float speed = 2f;
    [SerializeField] public float swimmingSpeed = 3f;
    public Vector2 motionVector;
    public Vector2 swimmingMotionVector;
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
        if (!GameManagerSingleton.Instance.IsPaused)
        {
            if (GameManagerSingleton.Instance.isSwimming)
            {
                animator.SetBool("isSwimming", true);
                float shorizontal = Input.GetAxisRaw("Horizontal");
                float svertical = Input.GetAxisRaw("Vertical");
                swimmingMotionVector = new Vector2(shorizontal, svertical);
                animator.SetFloat("horizontalSwimming", shorizontal);
                animator.SetFloat("verticalSwimming", svertical);
            }
            else
            {
                animator.SetBool("isSwimming", false);
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                motionVector = new Vector2(horizontal, vertical);
                animator.SetFloat("horizontal", horizontal);
                animator.SetFloat("vertical", vertical);
                moving = horizontal != 0 || vertical != 0;
                animator.SetBool("moving", moving);
                if (horizontal != 0 || vertical != 0)
                {
                    LastMotionVector = new Vector2(horizontal, vertical).normalized;
                    animator.SetFloat("lastHorizontal", horizontal);
                    animator.SetFloat("lastVertical", vertical);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (GameManagerSingleton.Instance.isSwimming)
        {
            rb.velocity = swimmingMotionVector * swimmingSpeed;
        }
        else
        {
            rb.velocity = motionVector * speed;
        }
    }

    private void OnDestroy()
    {
        DataPersistanceManager.instance.readyToSave = true;
        DataPersistanceManager.instance.SaveGame();
    }
    public void LoadData(GameData data)
    {
        transform.position = data.playerPosition;
        GameManagerSingleton.Instance.isSwimming = data.isSwimming;
    }

    public void SaveData(GameData data)
    {
        data.playerPosition = transform.position;
        data.isSwimming = GameManagerSingleton.Instance.isSwimming;
    }
}