using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBoatController : MonoBehaviour
{
    //Components
    BoatController boatController;

    //Awake is called when the script instance is being loaded.
    void Awake()
    {
        boatController = GetComponent<BoatController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame and is frame dependent
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        //Get input from Unity's input system.
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");
        //Send the input to the car controller.
        boatController.SetInputVector(inputVector);
    }
}
