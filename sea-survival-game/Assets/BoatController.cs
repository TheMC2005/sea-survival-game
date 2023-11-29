using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{

    public GameObject seat;
    public float maxSpeed = 5f;
    public float rotationSpeed = 2f;
    public float acceleration = 1f;
    public float deceleration = 0.5f;
    public float turnSpeed = 2f; // New variable for turning speed
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

        // Prevent moving backward
        if (verticalInput < 0)
        {
            verticalInput = 0;
        }

        // Adjust the boat's rotation based on the input
        RotateBoat(horizontalInput);

        // Get the boat's local forward vector
        Vector2 forwardVector = transform.up;

        // Calculate the velocity based on the input and the local forward vector
        boatMotionVector = Vector2.Lerp(boatMotionVector, forwardVector * verticalInput, Time.deltaTime * acceleration);

        if (boatMotionVector.magnitude > 1f)
        {
            boatMotionVector.Normalize();
        }

        // Apply velocity and deceleration
        boatrb.velocity = boatMotionVector * maxSpeed;
        boatrb.velocity *= (1f - Time.deltaTime * deceleration);
    }

    void RotateBoat(float horizontalInput)
    {
        // Calculate the target rotation angle based on the input
        float targetRotation = transform.eulerAngles.z - horizontalInput * turnSpeed;

        // Set the new rotation
        transform.rotation = Quaternion.Euler(0, 0, targetRotation);
    }
}
