using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{

    public GameObject seat;
    public float speed = 5f;
    public float rotationSpeed = 3f;
    public float acceleration = 1f;
    public float deceleration = 0.5f;
    public CharacterController2D characterController;
    private bool isPlayerInSeat = false;
    private Rigidbody2D boatrb;
    private Vector2 boatMotionVector;

    void Start()
    {
        boatrb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleSeat();
        }

        if (isPlayerInSeat)
        {
            GameManagerSingleton.Instance.player.transform.position = seat.transform.position;
            HandleBoatMovement();
        }
    }

    void ToggleSeat()
    {
        if (isPlayerInSeat)
        {
            characterController.enabled = true;
            isPlayerInSeat = false;
        }
        else
        {
            isPlayerInSeat = true;
            characterController.enabled = false;
            GameManagerSingleton.Instance.player.transform.position = seat.transform.position;
        }
    }

    void HandleBoatMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        boatMotionVector = Vector2.Lerp(boatMotionVector, new Vector2(horizontalInput, verticalInput), Time.deltaTime * acceleration);
        if (boatMotionVector.magnitude > 1f)
        {
            boatMotionVector.Normalize();
        }
        boatrb.velocity = boatMotionVector * speed;
        float rotateAmount = -horizontalInput * rotationSpeed;
        boatrb.AddTorque(rotateAmount);
        boatrb.velocity *= (1f - Time.deltaTime * deceleration);
    }
}
