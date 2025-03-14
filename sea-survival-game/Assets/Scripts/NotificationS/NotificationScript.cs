using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NotificationScript : MonoBehaviour
{
    Canvas notificationCanvas;
    public GameObject BoatNotificationPanel;

    //Local Variable
    public List<bool> toggleBools = new List<bool>();
    public bool toggleBool = false;
    public bool dialogueToggle = false;
    //distances
    int numberOfDistances = 99; //ugy valtoztasd, hogy mennyi lesz
    float mineEntranceDistance;
    float mineExitDistance;
    float boatDistance;
    //Gameobjects
    public GameObject mineEntranceObj;
    public GameObject mineExitObj;
    public GameObject player;

    //References
    public TopDownCarController topDownCarController;
    public MineEntrance mineEntrance;
    public MineExit mineExit;
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
        //Az npc-set a dialogueTriggerben van mert ott sokkal konnyebb volt es elegansabb
        ToggleBoatNotification();
        CheckIfNearBoat(); 
        StartCoroutine(CheckIfNearCaveEntranceCoroutine());
        StartCoroutine(CheckIfNearCaveExitCoroutine());
    }

    

    private void CheckIfNearBoat()
    {
        boatDistance = (GameManagerSingleton.Instance.player.transform.position - topDownCarController.seat.transform.position).sqrMagnitude;
        if (Input.GetKeyDown(KeyCode.E) && boatDistance < 5f)
        {
            topDownCarController.ToggleSeat();
        }

        toggleBools[0] = boatDistance < 5f && boatDistance > 0.1f;
        
    }
    private IEnumerator CheckIfNearCaveExitCoroutine()
    {
            mineExitDistance = (player.transform.position - mineExitObj.transform.position).sqrMagnitude;
            if (Input.GetKeyDown(KeyCode.E) && mineExitDistance < 5f)
            {
             yield return new WaitForSeconds(0.2f);
             mineExit.ExitMine();
             yield return new WaitForSeconds(0.2f);
        }
            toggleBools[2] = mineExitDistance < 5f && mineExitDistance > 0.1f;

            // Wait for 0.2 seconds
            yield return new WaitForSeconds(0.2f);
        
    }
    private IEnumerator CheckIfNearCaveEntranceCoroutine()
    {
            mineEntranceDistance = (player.transform.position - mineEntranceObj.transform.position).sqrMagnitude;
            if (Input.GetKeyDown(KeyCode.E) && mineEntranceDistance < 5f)
            {
                yield return new WaitForSeconds(0.2f);
                mineEntrance.EnterMine();
                yield return new WaitForSeconds(0.2f);
        }
            toggleBools[1] = mineEntranceDistance < 5f && mineEntranceDistance > 0.1f;

            // Wait for 0.2 seconds
            yield return new WaitForSeconds(0.22f);
    }



}
