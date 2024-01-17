using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationScript : MonoBehaviour
{
    Canvas notificationCanvas;
    public GameObject BoatNotificationPanel;

    //Local Variable
    public bool boatToggle = false;
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
    }

}
