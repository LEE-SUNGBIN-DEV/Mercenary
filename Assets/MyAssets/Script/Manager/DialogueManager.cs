using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager
{ 
    private Dictionary<uint, string[]> dialogueDictionary;

    public void Initialize()
    {
        DialogueDictionary = new Dictionary<uint, string[]>();

        QuestTask.onTaskStart -= AddDialogue;
        QuestTask.onTaskStart += AddDialogue;

        QuestTask.onTaskEnd -= RemoveDialogue;
        QuestTask.onTaskEnd += RemoveDialogue;
    }

    public void AddDialogue(QuestTask questTask)
    {
        if(questTask is DialogueTask)
        {
            DialogueTask dialogueTask = questTask as DialogueTask;
            uint dialogueID = dialogueTask.NpcID + dialogueTask.OwnerQuest.QuestID;
            if (!DialogueDictionary.ContainsKey(dialogueID))
            {
                DialogueDictionary.Add(dialogueID, dialogueTask.Dialogues);
            }
        }
    }

    public void RemoveDialogue(QuestTask questTask)
    {
        if (questTask is DialogueTask)
        {
            DialogueTask dialogueTask = questTask as DialogueTask;
            uint dialogueID = dialogueTask.NpcID + dialogueTask.OwnerQuest.QuestID;
            if (DialogueDictionary.ContainsKey(dialogueID))
            {
                DialogueDictionary.Remove(dialogueID);
            }
        }
    }

    public string GetDialogue(uint dialogueID, int dialogueIndex)
    {
        if (dialogueIndex == DialogueDictionary[dialogueID].Length)
        {
            return null;
        }

        else
        {
            return DialogueDictionary[dialogueID][dialogueIndex];
        }
    }

    #region Property
    public Dictionary<uint, string[]> DialogueDictionary
    {
        get { return dialogueDictionary; }
        private set { dialogueDictionary = value; }
    }
    #endregion
}
