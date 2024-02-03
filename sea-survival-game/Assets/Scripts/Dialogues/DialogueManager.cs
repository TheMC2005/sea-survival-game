using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueParent;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button option1Button;
    [SerializeField] private Button option2Button;

    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float turnSpeed = 2f;

    public List<dialogueString> dialogueList;

    [Header("Player")]
    [SerializeField] private CharacterController2D characterController;
    private int currentDialogueIndex = 0;
    private Transform playerCamera;

    private void Start()
    {
        dialogueParent.SetActive(false);
        playerCamera = Camera.main.transform;
    }
    public void DialogueStart(List<dialogueString> textToPrint, Transform NPC)
    {
        dialogueParent.SetActive(false);
        characterController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        dialogueList = textToPrint;
        currentDialogueIndex = 0;

        DisableButtons();
        StartCoroutine(PrintDialogue());
    }
    private void DisableButtons()
    {
        option1Button.interactable = false;
        option2Button.interactable = false;
        option1Button.GetComponentInChildren<TMP_Text>().text = "No Option";
        option2Button.GetComponentInChildren<TMP_Text>().text = "No Option";
    }
    private bool optionSelected = false;
    private IEnumerator PrintDialogue()
    {
        while(currentDialogueIndex < dialogueList.Count)
        {
          dialogueString line = dialogueList[currentDialogueIndex];
            line.startDialogueEvent?.Invoke();
            if(line.isQuestion) 
            {
                yield return StartCoroutine(TypeText(line.text));

                option1Button.interactable = true;
                option2Button.interactable = true;
                option1Button.GetComponentInChildren<TMP_Text>().text = line.answerOption1;
                option2Button.GetComponentInChildren<TMP_Text>().text = line.answerOption2;
                option1Button.onClick.AddListener(() => HandleOptionSelected(line.option1IndexJump));
                option2Button.onClick.AddListener(() => HandleOptionSelected(line.option2IndexJump));
                yield return new WaitUntil(() => optionSelected);
            }

            else
            {
              yield return StartCoroutine(TypeText(line.text));
            }
            line.endDialogueEvent?.Invoke();
            optionSelected = false;
        }
        DialogueStop();
    }
    private void HandleOptionSelected(int indexJump)
    {
        optionSelected = false;
        DisableButtons();
        currentDialogueIndex = indexJump;
    }
    private IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        foreach(char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        if (!dialogueList[currentDialogueIndex].isQuestion)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }
        if (dialogueList[currentDialogueIndex].isEnd)
        {
            DialogueStop();
        }
        currentDialogueIndex++;
    }

    private void DialogueStop()
    {
        StopAllCoroutines();
        dialogueText.text = "";
        dialogueParent.SetActive(false);
        dialogueParent.SetActive(false);

        characterController.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
