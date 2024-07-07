using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUIManager : MonoBehaviour
{
    public GameObject questPanel; 
    public TextMeshProUGUI questDescriptionText; 

    void Start()
    {
        // Initially hide the quest panel
        questPanel.SetActive(false);
        Debug.Log("QuestUIManager started and questPanel is hidden.");
    }

    public void ShowQuest(string questDescription)
    {
        Debug.Log("ShowQuest called: " + questDescription);
        questDescriptionText.text = questDescription;
        questPanel.SetActive(true);
    }

    public void ShowQuestSummary(string questSummary)
    {
        Debug.Log("ShowQuestSummary called: " + questSummary);
        questDescriptionText.text = questSummary;
        questPanel.SetActive(true);
    }
    public void UpdateQuestSummary(string questSummary)
    {
        Debug.Log("UpdateQuestSummary called: " + questSummary);
        questDescriptionText.text = questSummary;
    }

    public void HideQuest()
    {
        Debug.Log("HideQuest called.");
        questPanel.SetActive(false);
    }
}
