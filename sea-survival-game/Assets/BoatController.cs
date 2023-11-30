using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BoatController : MonoBehaviour
{

    public GameObject seat;
    public CharacterController2D characterController;
    public UnityEngine.UI.Slider speedslider;
    public TextMeshProUGUI speedText;
    public Canvas boatUICanvas;
    public AnimationCurve turnSpeedCurve;

    public float maxSpeed = 5f;
    public float maxTorque = 0.1f;
    public float torqueDecayRate = 0.075f;
    public float rotationSpeed = 0.075f;
    public float acceleration = 0.15f;
    public float deceleration = 0.25f;
    private bool isPlayerInSeat = false;

    private Rigidbody2D boatrb;


    void Start()
    {
        boatrb = GetComponent<Rigidbody2D>();
        speedslider.maxValue = 30;
        speedslider.minValue = 0;
        speedText.text = string.Empty;
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
            ChangeTurnFloat(boatrb.velocity.magnitude);
        }
    }

    void ToggleSeat()
    {
        if (isPlayerInSeat)
        {
            characterController.enabled = true;
            isPlayerInSeat = false;
            boatUICanvas.enabled = false;
        }
        else
        {
            isPlayerInSeat = true;
            boatUICanvas.enabled = true;
            characterController.enabled = false;
            GameManagerSingleton.Instance.player.transform.position = seat.transform.position;
        }
    }

    void HandleBoatMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Prevent moving backward
        if (verticalInput < 0)
        {
            verticalInput = 0;
        }

        float torqueToApply = -horizontalInput * rotationSpeed;
        torqueToApply = Mathf.Clamp(torqueToApply, -maxTorque, maxTorque);
        boatrb.AddTorque(torqueToApply);

        // Decay torque over time
        boatrb.AddTorque(-boatrb.angularVelocity * torqueDecayRate);

        Vector2 forwardVector = transform.right;
        boatrb.AddForce(forwardVector * verticalInput * acceleration, ForceMode2D.Impulse);

        LimitSpeed();

        speedslider.value = boatrb.velocity.magnitude * 6;
        speedText.text = Mathf.CeilToInt((boatrb.velocity.magnitude * 6)).ToString();

        boatrb.velocity *= (1f - Time.deltaTime * deceleration);
    }

    void ChangeTurnFloat(float currentVelocity)
    {
       //
    }

    void LimitSpeed()
    {
        float currentSpeed = boatrb.velocity.magnitude;
        if (currentSpeed > maxSpeed)
        {
            boatrb.velocity = boatrb.velocity.normalized * maxSpeed;
        }
    }
}
