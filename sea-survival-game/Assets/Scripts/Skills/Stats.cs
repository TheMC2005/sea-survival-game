using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;


public class Stats : MonoBehaviour
{
    //public variables
    public int adminxp;
    public int food = 100;
    public int tempmobilityxp;
    public int tempforagingxp;
    public int tempcraftingxp;
    public int tempforgexp;
    public int mobilityxp;
    public int foragingxp;
    public int craftingxp;
    public int forgexp;
    public int allexp;
    public int skillPoints = 0;
    public int checkpointLevel = 128;

    //References
    [SerializeField] UnityEngine.UI.Slider xpslider;
    [SerializeField] UnityEngine.UI.Slider foodslider;
    
    public CharacterController2D characterController;
    public static Stats Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Found more than one Skills in the scene.Destroying the newest one");
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        StartCoroutine(UpdateSkillPointsRoutine());
        StartCoroutine(UpdateHungerEffects());
        StartCoroutine(UpdateHungerBar());
        foodslider.value = food;
        xpslider.maxValue = checkpointLevel;
        xpslider.value = allexp;
    }
    public void RemoveFood(int numb)
    {
        food -= numb;
    }
    public void GiveMobilityxp(int numb)
    {
        mobilityxp += numb;
        tempmobilityxp += numb;
    }

    public void GiveForagingxp(int numb)
    {
        foragingxp += numb;
        tempforagingxp += numb;
    }
    public void GiveCraftingxp(int numb)
    {
        craftingxp += numb;
        tempcraftingxp += numb;
    }
    public void GiveForgexp(int numb)
    {
        forgexp += numb;
        tempforgexp += numb;

    }

    private void Update()
    {
        allexp = adminxp + tempmobilityxp + tempforagingxp + tempcraftingxp + tempforgexp;
        xpslider.value = allexp;


    }

    private IEnumerator UpdateHungerBar()
    {
        while (true)
        {
            if(food>0)
            {
                food -= 1;
                foodslider.value = food;
            }
            yield return new WaitForSeconds(5f);

        }
    }

    private IEnumerator UpdateHungerEffects()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);

            HungerEffects();
        }
    }

    private void HungerEffects()
    {
        if(food > 75)
        {
            characterController.speed += 0.15f;
        }
        if(food < 15)
        {
            characterController.speed -= 0.5f;
        }
    }

    private IEnumerator UpdateSkillPointsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);

            CheckForSkillPoints();
        }
    }
    private void CheckForSkillPoints()
    {
        if (allexp >= checkpointLevel)
        {
            int additionalSkillPoints = allexp / checkpointLevel;
            skillPoints += additionalSkillPoints;
            allexp -= checkpointLevel;
            checkpointLevel += 128;
            tempforagingxp = 0;
            tempcraftingxp = 0;
            tempforgexp = 0;
            tempmobilityxp = 0;
            xpslider.maxValue = checkpointLevel;
        }
    }
}
