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
    public float rotationSpeed = 0.3f;
    public float acceleration = 0.15f;
    public float deceleration = 0.25f;
    public float turnSpeed = 0.075f;
    private bool isPlayerInSeat = false;


    private Rigidbody2D boatrb;
    public Vector2 boatMotionVector;

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
        if (verticalInput < 0)
        {
            verticalInput = 0;
        }
        RotateBoat(horizontalInput);
        Vector2 forwardVector = transform.right; // merre van az eleje a hajónak
        if(verticalInput >0)
        boatrb.AddForce(forwardVector*verticalInput*30);

        if (boatMotionVector.magnitude > 1f)
        {
            boatMotionVector.Normalize();
        }
        //sebbesége a hajónak, plussz idővel csökken
        speedslider.value = boatrb.velocity.magnitude*6;
        speedText.text = Mathf.CeilToInt((boatrb.velocity.magnitude * 6)).ToString();
        boatrb.velocity *= (1f - Time.deltaTime * deceleration);
    }
    void ChangeTurnFloat(float currentVelocity)
    {
       // float newTurnSpeed = turnSpeedCurve.Evaluate(currentVelocity);
        //turnSpeed = newTurnSpeed;
    }
    void RotateBoat(float horizontalInput)
    {
        // ez itt nem tudom mi a fasz ctrl cztem
        float targetRotation = transform.eulerAngles.z - horizontalInput * turnSpeed;
        transform.rotation = Quaternion.Euler(0, 0, targetRotation);
    }
}
