using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotsMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private MainMenu mainMenu;

    [Header("Menu Buttons")]
    [SerializeField] private Button backButton;

    private SavedSlot[] saveSlots;


    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SavedSlot>();
    }

    public void OnSaveSlotClicked(SavedSlot saveSlot)
    {

        DisableMenuButtons();
        DataPersistanceManager.instance.ChangeSelectedProfileID(saveSlot.GetProfileId());
        if(saveSlot.hasGameData == false)
        {
           DataPersistanceManager.instance.NewGame();
        }
        SceneManager.LoadSceneAsync("B-test");
    }

    public void OnBackClicked()
    {
        mainMenu.ActivateMenu();
        this.DeactivateMenu();
    }

    public void ActivateMenu()
    {

        this.gameObject.SetActive(true);
        Dictionary<string, GameData> profilesGameData = DataPersistanceManager.instance.GetAllProfilesGameData();

        GameObject firstSelected = backButton.gameObject;
        foreach (SavedSlot saveSlot in saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);
            if (profileData != null)
            {
                saveSlot.hasGameData = true;
            }
            else
                saveSlot.hasGameData = false;
 
                if (firstSelected.Equals(backButton.gameObject))
                {
                    firstSelected = saveSlot.gameObject;
                }
        }
        StartCoroutine(this.SetFirstSelected(firstSelected));
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

    private void DisableMenuButtons()
    {
        foreach (SavedSlot saveSlot in saveSlots)
        {
            saveSlot.SetInteractable(false);
        }
        backButton.interactable = false;
    }
}
