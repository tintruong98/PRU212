using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
	[SerializeField] public string npcName;
	[SerializeField] public string questDescription;
    [SerializeField] public bool questGiven = false;
    [SerializeField] public string[] sentences;

    public event Action OnDialogueComplete; // Event to notify when dialogue is complete

    private int currentSentence = 0; // Tracking the current sentence index

    public int totalLettersToFind = 5;
    public int lettersFound = 0;

    public string GetQuestSummary()
    {
        return $"Letter to find {lettersFound}/{totalLettersToFind}";
    }

    public void TriggerDialogue()
    {
        if (currentSentence < sentences.Length)
        {
            Debug.Log(sentences[currentSentence]); // Display the current sentence
            currentSentence++;
        }
        else
        {
            CompleteDialogue();
        }
    }

    private void CompleteDialogue()
    {
        OnDialogueComplete?.Invoke();
        currentSentence = 0; // Reset for next dialogue
        questGiven = true; // Ensure questGiven is set to true
        Debug.Log("Quest given is now true.");
    }

}
