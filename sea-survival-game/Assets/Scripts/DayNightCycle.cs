using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class DayNightCycle : MonoBehaviour
{
    public TextMeshProUGUI timeDisplay; 
    public TextMeshProUGUI dayDisplay; 
    public Volume ppv; // post processing volume

    public float tick = 20000; 
    public float seconds;
    public int mins;
    public int hours;
    public int days = 1;


    void Start()
    {
        ppv = gameObject.GetComponent<Volume>();
    }


    void FixedUpdate()
    {
        CalcTime();
        DisplayTime();

    }

    public void CalcTime() 
    {
        seconds += Time.fixedDeltaTime * tick; 

        if (seconds >= 60) 
        {
            seconds = 0;
            mins += 1;
        }

        if (mins >= 60)
        {
            mins = 0;
            hours += 1;
        }

        if (hours >= 24)
        {
            hours = 0;
            days += 1;
        }
        ControlPPV(); 
    }

    public void ControlPPV() 
    {

        if (hours >= 20 && hours < 21) 
        {
            ppv.weight = (float)mins / 60; 
        }


        if (hours >= 6 && hours < 7) 
        {
            ppv.weight = 1 - (float)mins / 60;
        }
    }

    public void DisplayTime() 
    {

        timeDisplay.text = string.Format("{0:00}:{1:00}", hours, mins);
        dayDisplay.text = "Day: " + days; 
    }
   

}
