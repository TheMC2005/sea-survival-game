using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour, IDataPersistence
{
    Rigidbody2D rb;
    [SerializeField] public float speed = 3f;
    [SerializeField] public float shallowSpeed = 2.5f;
    [SerializeField] public float swimmingSpeed = 2f;
    [SerializeField] TopDownCarController topDownCarController;
    public float speedMultiplier;
    public Vector2 motionVector;
    public Vector2 swimmingMotionVector;
    public Vector2 shallowMotionVector;
    public Vector2 LastMotionVector;
    public Animator animator;
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
            if(topDownCarController.isPlayerInSeat ==  false) 
            { 
            if (GameManagerSingleton.Instance.inShallow)
            {
                animator.SetBool("inShallow", true);
                animator.SetBool("isSwimming", false);
                animator.SetBool("isMoving", false);
                float shallowHorizontal = Input.GetAxisRaw("Horizontal");
                float shallowVertical = Input.GetAxisRaw("Vertical");
                moving = shallowHorizontal != 0 || shallowVertical != 0;
                shallowMotionVector = new Vector2(shallowHorizontal, shallowVertical);
                animator.SetFloat("horizontalShallow", shallowHorizontal);
                animator.SetFloat("verticalShallow", shallowVertical);
            }

            if(GameManagerSingleton.Instance.isMoving)  
            {
                animator.SetBool("inShallow", false);
                animator.SetBool("isSwimming", false);
                animator.SetBool("isMoving", true);
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
            if (GameManagerSingleton.Instance.isSwimming)
            {
                animator.SetBool("inShallow", false);
                animator.SetBool("isSwimming", true);
                animator.SetBool("isMoving", false);
                float shorizontal = Input.GetAxisRaw("Horizontal");
                float svertical = Input.GetAxisRaw("Vertical");
                moving = shorizontal != 0 || svertical != 0;
                swimmingMotionVector = new Vector2(shorizontal, svertical);
                animator.SetFloat("horizontalSwimming", shorizontal);
                animator.SetFloat("verticalSwimming", svertical);
            }
          }
            else
            {
                animator.SetBool("inShallow", false);
                animator.SetBool("isSwimming", false);
                animator.SetBool("isMoving", false);
            }
        }
    }

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
          speedMultiplier = 1.5f;
        }
        else
        speedMultiplier = 1.0f;
        if (GameManagerSingleton.Instance.inShallow)
        {
            rb.velocity = shallowMotionVector * shallowSpeed * speedMultiplier;
        }
        if (GameManagerSingleton.Instance.isSwimming)
        {
            rb.velocity = swimmingMotionVector * swimmingSpeed * speedMultiplier;
        }
        if(GameManagerSingleton.Instance.isMoving)
        {
            rb.velocity = motionVector * speed * speedMultiplier;
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