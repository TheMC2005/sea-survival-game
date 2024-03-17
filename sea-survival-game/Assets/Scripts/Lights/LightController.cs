using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class LanternController : MonoBehaviour
{
    [SerializeField] float TargetIntensity = 40; // meddig menjen fel a light ereje
    [SerializeField] GameObject deActivated;
    [SerializeField] GameObject Activated;
    [SerializeField] AnimationCurve curveTurnOn;
    [SerializeField] int hoursOn = 20;
    [SerializeField] int minsOn = 15;
    [SerializeField] float speedTurnOn = 0.1f; //a felkapcsolasnal milyen gyors legyen az anim
    [SerializeField] float LightPlaceHolderTurnOn;  //csak azert rakom be, hogyha tesztelunk akkor lassuk, hogy mennyi ido alatt mennyire megy fel a light ereje
    [SerializeField] int LightIntensityTurnOn; // az elozo csak int-es formaban
    [SerializeField] AnimationCurve curveTurnOff;
    [SerializeField] int hoursOff = 6;
    [SerializeField] int minsOff = 10;
    [SerializeField] float speedTurnOff = 0.2f;
    [SerializeField] float LightPlaceHolderTurnOff;
    [SerializeField] int LightIntensityTurnOff;
    bool start = true; // azert kell, hogy ne fusson addig a program amig az ifek ervenyesek (pl eleri a max intensityt a feny 40-kor de 59 ig futna a program)
    Light2D light2d;
    int _targetTurnOn = 1;
    float _currentTurnOn = 0;
    int _targetTurnOff = 1;
    float _currentTurnOff = 0;
    
    private void Start()
    {
        light2d = GetComponent<Light2D>();

    }

    void Update()
    {
        int hours1 = DayNightCycle.Instance.hours;
        int mins1 = DayNightCycle.Instance.mins;
        if(hours1 >= hoursOn && hours1 <= hoursOn+2 && mins1 > minsOn && start==true)
        {
            light2d.enabled = true;
            deActivated.SetActive(false);
            Activated.SetActive(true);
            _currentTurnOn = Mathf.MoveTowards(_currentTurnOn, _targetTurnOn, speedTurnOn * Time.deltaTime);
            LightPlaceHolderTurnOn = Mathf.Lerp(0, TargetIntensity, curveTurnOn.Evaluate(_currentTurnOn));
            if (Convert.ToInt32(LightPlaceHolderTurnOn) != 0)
            {
                LightIntensityTurnOn = Convert.ToInt32(LightPlaceHolderTurnOn);
                light2d.intensity = LightIntensityTurnOn;
            }
            if(LightIntensityTurnOn == TargetIntensity)
            {
                start = false;
            }
        }
        if(hours1>=hoursOff && hours1<=hoursOff+2 && mins1>minsOff && start == false)
        {
            _currentTurnOff = Mathf.MoveTowards(_currentTurnOff, _targetTurnOff, speedTurnOff * Time.deltaTime);
            LightPlaceHolderTurnOff = Mathf.Lerp(TargetIntensity, 1, curveTurnOff.Evaluate(_currentTurnOff));
            if (Convert.ToInt32(LightPlaceHolderTurnOff) != 0)
            {
                LightIntensityTurnOff = Convert.ToInt32(LightPlaceHolderTurnOff);
                light2d.intensity = LightIntensityTurnOff;
            }
            if (light2d.intensity < 3)
            {
                deActivated.SetActive(true);
                Activated.SetActive(false);
            }
            if(LightIntensityTurnOff == 1)
            {
                light2d.enabled = false;
                start = true;
                LightPlaceHolderTurnOn = 0;
                LightPlaceHolderTurnOff = 0;
                LightIntensityTurnOff = 0;
                LightIntensityTurnOn = 0; 
                _currentTurnOff = 0;
                _currentTurnOn = 0;
            }
        }
    }
}
