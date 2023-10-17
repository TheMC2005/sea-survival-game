using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenGameObjectManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> gameObjectsList = new List<GameObject>();
    [SerializeField] public List<GameObject> gameObjectsListinsideSettingsmenu = new List<GameObject>();
    [SerializeField] public List<GameObject> audioGameObjects = new List<GameObject>();
    public GameObject objectToToggle;
    private bool isActive = false;

    public void QuitGame()
    {
        Debug.Log("Kilepes");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    public void DisableAllGameObjects()
    {
        foreach (GameObject obj in gameObjectsList)
        {
            obj.SetActive(false);
        }
    }
    public void EnableAllGameObjects()
    {
        foreach (GameObject obj in gameObjectsList)
        {
            obj.SetActive(true);
        }
    }

    public void DisableAllGameObjectsInsideSettings()
    {
        foreach (GameObject obj in gameObjectsListinsideSettingsmenu)
        {
            obj.SetActive(false);
        }
    }
    public void EnableAllGameObjectsInsideSettings()
    {
        foreach (GameObject obj in gameObjectsListinsideSettingsmenu)
        {
            obj.SetActive(true);
        }
    }

    public void DisableAllAudioObjects()
    {
        foreach (GameObject obj in audioGameObjects)
        {
            obj.SetActive(false);
        }
    }
    public void EnableAllAudioObjects()
    {
        foreach (GameObject obj in audioGameObjects)
        {
            obj.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DeactivateObject();
           EnableAllGameObjects();
            DisableAllAudioObjects();
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
