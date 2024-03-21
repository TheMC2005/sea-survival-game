using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillTree : MonoBehaviour
{
    //local variables
    private int numberOfSkills = 10;
    //public variables

    public GameObject panel;
    public GameObject abilityNotification;
    public GameObject abilityNotificationFail;
    public ShieldAbility shieldAbility;
    public GameObject shieldAbilityButton;

    public GameObject sprintSkillButton;
    public GameObject portalSkillButton;
    public GameObject strengthSkillButton;

    //for the save
    public List<bool> skillBools = new List<bool>();
    void Start()
    {
        shieldAbilityButton.SetActive(false);
        sprintSkillButton.SetActive(false);
        portalSkillButton.SetActive(false);
        strengthSkillButton.SetActive(false);
        panel.SetActive(false);
        shieldAbility.enabled = false;
        abilityNotification.SetActive(false);
        abilityNotificationFail.SetActive(false);
        //delete when save is implemented !!!!!!!
        for(int i =0; i < numberOfSkills; i++)
        {
            skillBools.Add(false);
        }

    }
    private void Update()
    {
        if(Stats.Instance.mobilityxp > 10)
        {
            shieldAbilityButton.SetActive(true);
        }
        if(Stats.Instance.mobilityxp > 500)
        {
            sprintSkillButton.SetActive(true);
            portalSkillButton.SetActive(true);
            strengthSkillButton.SetActive(true);
        }
    }
    public void ShowSkillPanel()
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
        }
    }

    public void ShieldAbilityUnlock()
    {
        if(Stats.Instance.mobilityxp > 10 && Stats.Instance.skillPoints >= 1)
        {
            Stats.Instance.skillPoints--;
            skillBools[0] = true;
            shieldAbility.enabled=true;
            abilityNotification.SetActive(true);
            StartCoroutine(DelayNotification());

        }
        else
        {
            abilityNotificationFail.SetActive(true);
            StartCoroutine(DelayFailNotification());
        }
    }

    private IEnumerator DelayNotification()
    {
        yield return new WaitForSeconds(0.5f);
        abilityNotification.SetActive(false);
    }
    private IEnumerator DelayFailNotification()
    {
        yield return new WaitForSeconds(0.5f);
        abilityNotificationFail.SetActive(false );
    }
}
