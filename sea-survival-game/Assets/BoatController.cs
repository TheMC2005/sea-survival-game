using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{

    public GameObject seat;
    public CharacterController2D characterController;

    public float maxSpeed = 5f;
    public float rotationSpeed = 0.01f;
    public float acceleration = 2f;
    public float deceleration = 0.5f;
    public float turnSpeed = 0.5f;
    private bool isPlayerInSeat = false;


    private Rigidbody2D boatrb;
    public Vector2 boatMotionVector;

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
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        if (verticalInput < 0)
        {
            verticalInput = 0;
        }
        RotateBoat(horizontalInput);
        Vector2 forwardVector = transform.right; // merre van az eleje a hajónak
        boatMotionVector = Vector2.Lerp(boatMotionVector, forwardVector * verticalInput, Time.deltaTime * acceleration);

        if (boatMotionVector.magnitude > 1f)
        {
            boatMotionVector.Normalize();
        }

        //sebbesége a hajónak, plussz idővel csökken
        boatrb.velocity = boatMotionVector * maxSpeed;
        boatrb.velocity *= (1f - Time.deltaTime * deceleration);
    }

    void RotateBoat(float horizontalInput)
    {
        // ez itt nem tudom mi a fasz ctrl cztem
        float targetRotation = transform.eulerAngles.z - horizontalInput * turnSpeed;
        transform.rotation = Quaternion.Euler(0, 0, targetRotation);
    }
}
