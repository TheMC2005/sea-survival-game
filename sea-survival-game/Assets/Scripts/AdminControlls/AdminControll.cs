using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminControll : MonoBehaviour
{
    [Header("Virtual Camera")]
    public GameObject virtualCameraObj;
    public CinemachineVirtualCamera VirtualCamera;
    [Header("Boat")]
    public GameObject boat;
    public void IncreaseVision()
    {
        VirtualCamera = virtualCameraObj.GetComponent<CinemachineVirtualCamera>();
        VirtualCamera.m_Lens.OrthographicSize = 200;
    }
    public void DecreaseVision()
    {
        VirtualCamera = virtualCameraObj.GetComponent<CinemachineVirtualCamera>();
        VirtualCamera.m_Lens.OrthographicSize = 5;
    }
    public void EnableBoat()
    {
        boat.SetActive(true);
    }
    public void GiveSkillPoints()
    {
        Stats.Instance.GiveCraftingxp(1000);
        Stats.Instance.GiveForagingxp(1000);
        Stats.Instance.GiveForgexp(1000);
        Stats.Instance.GiveMobilityxp(1000);
    }
}
