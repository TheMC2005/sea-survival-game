using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private List<dialogueString> dialogueStrings = new List<dialogueString>();
    [SerializeField] private Transform NPCTransform;
    [SerializeField] public DialogueManager dialogueManager;

    private bool hasSpoken = false;
    private float distance;


    [SerializeField] NotificationScript notificationScript;
    private void Update()
    {
        distance = (GameManagerSingleton.Instance.player.transform.position - NPCTransform.transform.position).sqrMagnitude;
        if (distance < 5f && !hasSpoken)
        {
            notificationScript.dialogueToggle = true;
            notificationScript.ToggleBoatNotification();
        }
        else
        {
            notificationScript.dialogueToggle = false;
            notificationScript.ToggleBoatNotification();
        }
        if (Input.GetKeyDown(KeyCode.E) && distance < 5f && !hasSpoken)
        {
            dialogueManager.DialogueStart(dialogueStrings, NPCTransform);
            hasSpoken = true;
        }

    }
}

[System.Serializable]
public class dialogueString
{
    public string text;
    public bool isEnd;
    [Header("Branch")]
    public bool isQuestion;
    public string answerOption1;
    public string answerOption2;
    public int option1IndexJump;
    public int option2IndexJump;

    [Header("Triggered Events")]
    public UnityEvent startDialogueEvent;
    public UnityEvent endDialogueEvent;
}