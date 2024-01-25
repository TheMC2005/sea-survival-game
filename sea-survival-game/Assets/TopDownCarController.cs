using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TopDownCarController : MonoBehaviour
{
    enum BoatType
    {
        BoatLevelOne,
        BoatLevelTwo,
        BoatLevelThree,
    }

    [Header("Boat settings")]
    public float driftFactor;
    public float accelerationFactor;
    public float turnFactor;
    public float maxSpeed;
    public bool isPlayerInSeat = false;
    //Local variables
    float accelerationInput = 0;
    float steeringInput = 0;
    int randomTeleportX;
    int randomTeleportY;    
    float rotationAngle = 0;
    float velocityVsUp = 0;

    //Components
    Rigidbody2D carRigidbody2D;
    public Rigidbody2D playerRigidbody;
    public GameObject seat;
    CinemachineVirtualCamera cinemachineVirtualCamera;
    public GameObject cinemachineCamera;
    public Canvas boatUICanvas;
    [SerializeField]BoatType boatType;
    [SerializeField] Slider speedSlider;
    [SerializeField] TextMeshProUGUI boatSpeedText;
    

    public CharacterController2D characterController;

    //Awake is called when the script instance is being loaded.
    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
        cinemachineVirtualCamera = cinemachineCamera.GetComponent<CinemachineVirtualCamera>();
    }
    private void Start()
    {
        if(boatType == BoatType.BoatLevelOne)
        {
            driftFactor = 0.95f;
            accelerationFactor = 0.625f;
            turnFactor = 0.75f;
            maxSpeed = 5f;
        }
        if (boatType == BoatType.BoatLevelTwo)
        {
            driftFactor = 0.95f;
            accelerationFactor = 1.25f;
            turnFactor = 1.125f;
            maxSpeed = 7.5f;
        }
        if (boatType == BoatType.BoatLevelThree)
        {
            driftFactor = 0.95f;
            accelerationFactor = 1.875f;
            turnFactor = 1.75f;
            maxSpeed = 10f;
        }
        boatSpeedText.text = 0.ToString();
    }
    IEnumerator StopAnimation()
    {
        GameManagerSingleton.Instance.inShallow = false;
        GameManagerSingleton.Instance.isSwimming = false;
        characterController.animator.SetBool("inShallow", false);
        characterController.animator.SetBool("isSwimming", false);
        characterController.animator.SetBool("isMoving", true);
        yield return new WaitForSeconds(0.25f);
        characterController.animator.Play("Movement",0,0);
        characterController.animator.speed = 0;  
    }
    public void ToggleSeat()
    {
        if (isPlayerInSeat)
        {
            characterController.enabled = true;
            isPlayerInSeat = false;
            cinemachineVirtualCamera.m_Lens.OrthographicSize = 5;
            boatUICanvas.enabled = false;
           
            GameManagerSingleton.Instance.isSwimming = true;
            characterController.animator.SetBool("inShallow", false);
            characterController.animator.SetBool("isSwimming", true);
            characterController.animator.SetBool("isMoving", false);
            randomTeleportX = Random.Range(-1,1);
            randomTeleportY = Random.Range(-1,1);
            GameManagerSingleton.Instance.player.transform.position += new Vector3(randomTeleportX, randomTeleportY, 0);

        }
        else
        {
            isPlayerInSeat = true;
            StartCoroutine(StopAnimation());
            boatUICanvas.enabled = true;
            cinemachineVirtualCamera.m_Lens.OrthographicSize = 10;
        }
    }
    
    public void Update()
    {
        if (isPlayerInSeat)
        {
            RigidbodyConstraints2D constraints = playerRigidbody.constraints;
            GameManagerSingleton.Instance.player.transform.position = seat.transform.position;
            constraints = RigidbodyConstraints2D.FreezePosition;
            playerRigidbody.constraints = constraints;
            characterController.enabled = false;
        }
        else
        {
            RigidbodyConstraints2D constraints = playerRigidbody.constraints;
            constraints = RigidbodyConstraints2D.None;
            playerRigidbody.constraints = constraints;
            characterController.animator.speed = 1;

        }
    }
    //Frame-rate independent for physics calculations.
    void FixedUpdate()
    {
        if (isPlayerInSeat)
        {
            ApplyEngineForce();
            KillOrthogonalVelocity();
            ApplySteering();
            speedSlider.value = GetVelocityMagnitude()*5;
            boatSpeedText.text = (Mathf.CeilToInt(speedSlider.value)).ToString();
        }
    }

    void ApplyEngineForce()
    {
        if (accelerationInput == 0)
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        else carRigidbody2D.drag = 0;

        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;
        if (velocityVsUp < -maxSpeed*0.5f && accelerationInput < 0)
            return;
        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
            return;
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 2);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);
        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    float GetLateralVelocity()
    {
        return Vector2.Dot(transform.right, carRigidbody2D.velocity);
    }

    public bool IsTireScreeching(out float lateralVelocity, out bool isBraking)
    {
        lateralVelocity = GetLateralVelocity();
        isBraking = false;

        if (accelerationInput < 0 && velocityVsUp > 0)
        {
            isBraking = true;
            return true;
        }
        if (Mathf.Abs(GetLateralVelocity()) > 4.0f)
            return true;

        return false;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    public float GetVelocityMagnitude()
    {
        return carRigidbody2D.velocity.magnitude;
    }
}
