using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    [SerializeField] public DataPersistanceManager dataPersistanceManager;

    public void Start()
    {
        if(!dataPersistanceManager.HasGameData())
        { 
            continueGameButton.interactable = false;
        }
    }
    public void OnNewGameClicked()
    {
        DisableMenuButtons();
        dataPersistanceManager.NewGame();
        SceneManager.LoadSceneAsync("B-test");
    }
    public void OnContinueClicked()
    {
        DisableMenuButtons();
        SceneManager.LoadSceneAsync("B-test");
    }
    private void DisableMenuButtons()
    {
            newGameButton.interactable = false;
        continueGameButton.interactable = false; 
    }
} 
 