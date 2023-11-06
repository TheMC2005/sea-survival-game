using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class SavedSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] public string profileId = "";
    public bool hasGameData;
    public string days;

    [Header("Content")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;
    [SerializeField] private TextMeshProUGUI dayText;
    private Button saveSlotButton;

    private void Awake()
    {
        saveSlotButton = this.GetComponent<Button>();
    }

    public void SetData(GameData data)
    {
        // there's no data for this profileId
        if (data == null)
        {
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
        }
        // there is data for this profileId
        else
        {
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);

            dayText.text = "DAYS "+data.days;
        }
    }
    private void Update()
    {
        //Debug.Log(EventSystem.current.currentSelectedGameObject);
        //Debug.Log(saveSlotButton.gameObject);
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject == saveSlotButton.gameObject)
        {
            GetInformationOnSavedFile();
        }
    }
    public void GetInformationOnSavedFile()
    {
        Debug.Log(days);
    }

    public string GetProfileId()
    {
        return this.profileId;
    }

    public void SetInteractable(bool interactable)
    {
        saveSlotButton.interactable = interactable;
    }
}
