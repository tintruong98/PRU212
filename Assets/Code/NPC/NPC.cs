using UnityEngine;

public class NPC : MonoBehaviour
{
    private DialogueTrigger dialogueTrigger;
    private Dialogue dialogue;
    private bool playerInRange = false;
    private QuestUIManager questUIManager;
    private DialogueManager dialogueManager;

    void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        dialogue = GetComponent<Dialogue>();
        questUIManager = FindObjectOfType<QuestUIManager>(); 
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.OnDialogueEnd += GiveQuestToPlayer;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < 3f)
        {
            playerInRange = true;
            if (Input.GetKeyDown(KeyCode.E) && !dialogue.questGiven)
            {
                dialogueTrigger.TriggerDialogue();
                dialogueManager.StartDialogue(dialogue); 
            }
        }
        else
        {
            playerInRange = false;
        }
    }

    private void GiveQuestToPlayer()
    {
        if (!dialogue.questGiven)
        {
            dialogue.questGiven = true;
            Debug.Log($"Quest Given: {dialogue.questDescription}");

            string questSummary = dialogue.GetQuestSummary();
            questUIManager.ShowQuestSummary(questSummary);
            //questUIManager.ShowQuest(dialogue.questDescription);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        if (dialogueManager != null)
        {
            dialogueManager.OnDialogueEnd -= GiveQuestToPlayer;
        }
    }
}
