using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BoatController : MonoBehaviour
{
    enum BoatType
    {
        BoatLevelOne,
        BoatLevelTwo, 
        BoatLevelThree,
    }
    public GameObject seat;
    CinemachineVirtualCamera cinemachineVirtualCamera;
    public GameObject cinemachineCamera;
    public CharacterController2D characterController;
    public UnityEngine.UI.Slider speedslider;
    public TextMeshProUGUI speedText;
    public Canvas boatUICanvas;
    public AnimationCurve accelCurve;
    public AnimationCurve rotationCurve;
    [SerializeField] BoatType boatType;

    public float maxSpeed;
    public float maxTorque;
    public float torqueDecayRate;
    public float rotationSpeed;
    public float acceleration;
    public float deceleration;
    private bool isPlayerInSeat = false;

    private Rigidbody2D boatrb;

    void Start()
    { 
        boatrb = GetComponent<Rigidbody2D>();
        cinemachineVirtualCamera = cinemachineCamera.GetComponent<CinemachineVirtualCamera>();
        speedslider.maxValue = 30;
        speedslider.minValue = 0;
        speedText.text = string.Empty;
        if(boatType == BoatType.BoatLevelOne)
          {
             maxSpeed = 5f;
             maxTorque = 1.5f;
             torqueDecayRate = 0.075f;
             rotationSpeed = 1.125f;
             acceleration = 0.3f;
             deceleration = 0.15f;
          }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleSeat();
        }

        if (isPlayerInSeat)
        {
            GameManagerSingleton.Instance.player.transform.position = seat.transform.position; //megoldás majd megoldom
            HandleBoatMovement();
        }
    }

    void ToggleSeat()
    {
        if (isPlayerInSeat)
        {
            characterController.enabled = true;
            isPlayerInSeat = false;
            cinemachineVirtualCamera.m_Lens.OrthographicSize = 5;
            boatUICanvas.enabled = false;
        }
        else
        {
            isPlayerInSeat = true;
            boatUICanvas.enabled = true;
            characterController.enabled = false;
            cinemachineVirtualCamera.m_Lens.OrthographicSize = 10;
            GameManagerSingleton.Instance.player.transform.position = seat.transform.position;
        }
    }

    void HandleBoatMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        //ne menjen hátra
        if (verticalInput < 0)
        {
            verticalInput = 0;
        }

        float torqueToApply = -horizontalInput * (rotationSpeed * rotationCurve.Evaluate(boatrb.velocity.magnitude)); // úgy kanyarodj amilyen gyorsan mész
        torqueToApply = Mathf.Clamp(torqueToApply, -maxTorque, maxTorque);
        boatrb.AddTorque(torqueToApply);

        boatrb.AddTorque(-boatrb.angularVelocity * torqueDecayRate);

        Vector2 forwardVector = new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad));
       // Debug.Log(accelCurve.Evaluate(boatrb.velocity.magnitude).ToString());
        boatrb.AddForce(accelCurve.Evaluate(boatrb.velocity.magnitude) * acceleration * verticalInput * forwardVector, ForceMode2D.Impulse);
        LimitSpeed();

        speedslider.value = boatrb.velocity.magnitude * 6;
        speedText.text = Mathf.CeilToInt((boatrb.velocity.magnitude * 6)).ToString();

        boatrb.velocity *= (1f - Time.deltaTime * deceleration);
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
