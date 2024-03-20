using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillTree : MonoBehaviour
{
    //local variables
    private int numberOfSkills;
    //public variables

    public GameObject panel;

    //for the save
    public List<bool> skillBools = new List<bool>();
    void Start()
    {
        //delete when save is implemented !!!!!!!
        for(int i =0; i < numberOfSkills; i++)
        {
            skillBools.Add(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void DashSkill()
    {
        if(Stats.Instance.mobilityxp > 10 && Stats.Instance.skillPoints >= 1)
        {
            Stats.Instance.skillPoints--;
            skillBools[0] = true;

        }
    }
}
