using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class NotificationScript : MonoBehaviour
{
    Canvas notificationCanvas;
    public GameObject BoatNotificationPanel;

    //Local Variable
    public List<bool> toggleBools = new List<bool>();
    public bool toggleBool = false;
    public bool dialogueToggle = false;
    //distances
    int numberOfDistances = 10; //ugy valtoztasd, hogy mennyi lesz
    float mineDistance;
    float boatDistance;
    //Gameobjects
    public GameObject mine;
    public GameObject player;

    //References
    TopDownCarController topDownCarController;
    private void Start()
    {
        notificationCanvas = GetComponent<Canvas>();
        for(int i = 0; i < numberOfDistances; i++)
            toggleBools.Add(false);
    }

    public void ToggleBoatNotification()
    {
        bool anyTrue = toggleBools.Contains(true);

        if (anyTrue)
        {
            BoatNotificationPanel.SetActive(true);
        }
        else
        {
            BoatNotificationPanel.SetActive(false);
        }
    }
    private void Update()
    {
        ToggleBoatNotification();
        CheckIfNearCave();
        CheckIfNearBoat();
    }

    private void CheckIfNearBoat()
    {
        boatDistance = (GameManagerSingleton.Instance.player.transform.position - topDownCarController.seat.transform.position).sqrMagnitude;
        if (Input.GetKeyDown(KeyCode.E) && boatDistance < 5f)
        {
            topDownCarController.ToggleSeat();
        }

       // toggleBools[0] = boatDistance < 5f && boatDistance > 0.1f;
        
    }

    private void CheckIfNearCave()
    {
        mineDistance = (player.transform.position - mine.transform.position).sqrMagnitude;
        if (Input.GetKeyDown(KeyCode.E) && mineDistance < 5f)
        {
            //topDownCarController.ToggleSeat();
        }
        Debug.Log("MineDistance:" + mineDistance);
        toggleBools[0] = mineDistance < 5f && mineDistance > 0.1f;
    }

}
