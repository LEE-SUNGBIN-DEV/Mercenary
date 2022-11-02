using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueTask : QuestTask
{
    [SerializeField] private uint npcID;
    [SerializeField] [TextArea] private string[] dialogues;

    public override void StartTask()
    {
        onTaskStart(this);
        FunctionNPC.onTalkEnd -= Action;
        FunctionNPC.onTalkEnd += Action;
    }

    public override void EndTask()
    {
        FunctionNPC.onTalkEnd -= Action;
        ++OwnerQuest.TaskIndex;
        onTaskEnd(this);
    }

    public void Action(uint dialogueID)
    {
        if (dialogueID == OwnerQuest.QuestID + NpcID)
        {
            ++SuccessAmount;
        }
    }

    #region Property
    public uint NpcID
    {
        get { return npcID; }
    }
    public string[] Dialogues
    {
        get { return dialogues; }
    }
    #endregion
}
