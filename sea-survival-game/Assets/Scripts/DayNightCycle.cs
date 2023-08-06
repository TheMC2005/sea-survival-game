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

        if (hours >= 20 && hours < 21 && mins>15) 
        {
            ppv.weight = (float)mins / 60; 
        }

        if (hours >= 6 && hours < 7) 
        {
            if(ppv.weight != 0.2f)
            {
                ppv.weight = 1 - (float)mins / 60;
            }
        }
        if(hours >= 21 || hours<6) // ez csak azert kell ha veletlen tesztelsz akkor ne legyen buggos pl, hogy 19:30 van es este van
        {
            ppv.weight = 1;
        }
        if(hours >=7 && hours<20) // same here
        {
            ppv.weight = 0.2f;
        }
    }

    public void DisplayTime() 
    {

        timeDisplay.text = string.Format("{0:00}:{1:00}", hours, mins);
        dayDisplay.text = "Day: " + days; 
    }
   

}
