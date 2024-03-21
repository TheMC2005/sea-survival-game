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
    [Header("Teleports")]
    public GameObject teleportone;
    public GameObject teleporttwo;
    public GameObject teleportthree;
    public GameObject teleportfour;
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
    public void FirstTeleport()
    {
        GameManagerSingleton.Instance.player.transform.position = teleportone.transform.position;
    }
    public void SecondTeleport()
    {
        GameManagerSingleton.Instance.player.transform.position = teleporttwo.transform.position;

    }
    public void ThirdTeleport()
    {
        GameManagerSingleton.Instance.player.transform.position = teleportthree.transform.position;
    }
    public void FourthTeleport()
    {
        GameManagerSingleton.Instance.player.transform.position = teleportfour.transform.position;
    }
}
