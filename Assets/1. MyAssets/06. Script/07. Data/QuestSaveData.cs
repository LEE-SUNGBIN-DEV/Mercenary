using UnityEngine;

[System.Serializable]
public class QuestSaveData
{
    public QUEST_STATE questState;
    public uint questID;

    public int taskIndex;
    public int taskSuccessAmount;
}
