using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private Button resumeGameButton;
    [SerializeField] public GameObject MainMenuPanel;
    [SerializeField] private GameObject gameDayDisplay;
    [SerializeField] private GameObject gameTimeDisplay;
    private TextMeshProUGUI dayDisplay;
    private TextMeshProUGUI timeDisplay;
    public Color dayDisplayColor;
    public Color timeDisplayColor;



    private void Start()
    {
        MainMenuPanel.SetActive(false);
        dayDisplay = gameDayDisplay.GetComponent<TextMeshProUGUI>();
        timeDisplay = gameTimeDisplay.GetComponent<TextMeshProUGUI>();

        timeDisplayColor = timeDisplay.color;
        dayDisplayColor = dayDisplay.color;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenuPanel.SetActive(true);
            resumeGameButton.interactable = true;
            Time.timeScale = 0f;

            timeDisplayColor.a = 0.5f;
            timeDisplay.color = timeDisplayColor;

            dayDisplayColor.a = 0.5f;
            dayDisplay.color = dayDisplayColor;
        }
    }

    public void ResumeGame()
    {
        MainMenuPanel.SetActive(false);
        DisableMenuButtons();
        Time.timeScale = 1.0f;
        timeDisplayColor.a = 1f;
        timeDisplay.color = timeDisplayColor;

        dayDisplayColor.a = 1f;
        dayDisplay.color = dayDisplayColor;
    }

    private void DisableMenuButtons()
    {
        resumeGameButton.interactable = false;
    }
}
