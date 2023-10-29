using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    [Header("First selected button")]
    [SerializeField] private GameObject firstSelected;
    protected virtual void OnEnable()
    {
        StartCoroutine(SetFirstSelected(firstSelected)); 
    }
    public IEnumerator SetFirstSelected(GameObject firstSelected)
    {
        EventSystem.current.SetSelectedGameObject(firstSelected);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }
}
