using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private DialogueTrigger dialogueTrigger;
    private Dialogue dialogue;
    private bool playerInRange= false;
    private QuestUIManager questUIManager;
    void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        dialogue = GetComponent<Dialogue>();
        questUIManager = FindAnyObjectByType<QuestUIManager>();
        
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < 3f)
        {
            playerInRange = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogueTrigger.TriggerDialogue();
                if (!dialogue.questGiven)
                {
                    GiveQuestToPlayer();
                }
            }
        }
        else
        {
            playerInRange = false;
        }
    }

    private void GiveQuestToPlayer()
    {
        dialogue.questGiven = true;
        Debug.Log($"Quest Given: {dialogue.questDescription}");
        questUIManager.ShowQuest(dialogue.questDescription);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            // Optionally, display UI prompt to press 'E' to interact
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
