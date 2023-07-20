using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class LanternController : MonoBehaviour
{
    [SerializeField] GameObject deActivated;
    [SerializeField] GameObject Activated;
    Light2D light2d;
    public DayNightCycle scriptReference;
    
    private void Start()
    {
        light2d = GetComponent<Light2D>();

    }

    void Update()
    {

        int hours1 = scriptReference.hours;
        int mins1 = scriptReference.mins;

        if (hours1 > 20 || hours1<6)
        {
            light2d.enabled = true;
            deActivated.SetActive(false);
            Activated.SetActive(true);
            if(mins1%3==0 && light2d.intensity<50 && mins1%2==0 && mins1%5==0)
            {
                light2d.intensity += 1;
            }
        }
        else 
        {
            deActivated.SetActive(true);
            Activated.SetActive(false);
            if (mins1 % 3 == 0  && mins1 % 2 == 0 && mins1 % 5 == 0 && light2d.intensity!=0)
            {
                light2d.intensity -= 1;
                if(light2d.intensity==0)
                {
                    light2d.enabled = false;
                }
            }
        }
    }
}
