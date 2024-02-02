using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminControll : MonoBehaviour
{
    public GameObject virtualCameraObj;
    public CinemachineVirtualCamera VirtualCamera;
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
}
