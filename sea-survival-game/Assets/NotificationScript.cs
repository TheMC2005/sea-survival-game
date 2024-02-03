using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationScript : MonoBehaviour
{
    Canvas notificationCanvas;
    public GameObject BoatNotificationPanel;

    //Local Variable
    public bool boatToggle = false;
    public bool dialogueToggle = false;
    private void Start()
    {
        notificationCanvas = GetComponent<Canvas>();
    }

    public void ToggleBoatNotification()
    {
        if(boatToggle)
        {
            BoatNotificationPanel.SetActive(true);
        }
        else
        {
            BoatNotificationPanel.SetActive(false);
        }

        if(dialogueToggle)
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
        Debug.Log(Cursor.lockState);
    }

}
