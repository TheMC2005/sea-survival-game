using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivation : MonoBehaviour
{
    public GameObject objectToToggle;

    private bool isActive = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DeactivateObject();
        }
    }

    public void ToggleObject()
    {
        if (isActive)
        {
            DeactivateObject();
        }
        else
        {
            ActivateObject();
        }
    }

    private void ActivateObject()
    {
        if (objectToToggle != null)
        {
            objectToToggle.SetActive(true);
            isActive = true;
        }
    }

    private void DeactivateObject()
    {
        if (objectToToggle != null)
        {
            objectToToggle.SetActive(false);
            isActive = false;
        }
    }
}
