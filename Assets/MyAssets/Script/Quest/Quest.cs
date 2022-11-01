using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum QUEST_STATE
{
    INACTIVE = 0,   // 진행 불가능
    ACTIVE = 1,     // 진행 가능
    ACCEPT = 2,     // 진행 중
    COMPLETE = 3    // 완료
}

public enum QUEST_CATEGORY
{
    NONE = 0,
    MAIN = 1,
    SUB = 2
}

[System.Serializable]
public class Quest
{
    #region Event
    public static event UnityAction<Quest> onInactiveQuest;
    public static event UnityAction<Quest> onActiveQuest;
    public static event UnityAction<Quest> onAcceptQuest;
    public static event UnityAction<Quest> onCompleteQuest;
    public static event UnityAction<Quest> onReward;
    public static UnityAction<Quest> onTaskIndexChanged;
    #endregion

    [Header("Quest Infomations")]
    public QUEST_CATEGORY questCategory;
    public QUEST_STATE questState;
    [SerializeField] private uint questID;
    [SerializeField] private string questTitle;

    [Header("Conditions")]
    [SerializeField] private int levelCondition;
    [SerializeField] private uint questCondition;

    [Header("Tasks")]
    [SerializeField] private int taskIndex;
    [SerializeReference, SubclassSelector]
    private QuestTask[] questTasks;

    [Header("Rewards")]
    [SerializeField] private float rewardExperience;
    [SerializeField] private int rewardMoney;
    [SerializeField] private string[] rewardItems;

    public void InactiveQuest()
    {
        questState = QUEST_STATE.INACTIVE;
        onInactiveQuest(this);
    }
    
    public void ActiveQuest()
    {
        questState = QUEST_STATE.ACTIVE;
        for (int i = 0; i < QuestTasks.Length; ++i)
        {
            QuestTasks[i].OwnerQuest = this;
        }
        questTasks[taskIndex].StartTask();
        onActiveQuest(this);
    }

    public void AcceptQuest()
    {
        Managers.AudioManager.PlaySFX("Quest Accept");
        questState = QUEST_STATE.ACCEPT;
        for (int i = 0; i < QuestTasks.Length; ++i)
        {
            QuestTasks[i].OwnerQuest = this;
        }
        questTasks[taskIndex].StartTask();
        onAcceptQuest(this);
    }
    
    public void CompleteQuest()
    {
        Managers.AudioManager.PlaySFX("Quest Complete");
        questState = QUEST_STATE.COMPLETE;
        Reward();
        onCompleteQuest(this);
    }

    public void Reward()
    {
        onReward(this);
    }

    public QuestSaveData SaveQuest()
    {
        if (questState == QUEST_STATE.COMPLETE)
        {
            QuestSaveData questData = new QuestSaveData
            {
                questState = questState,
                questID = questID,
                taskIndex = 0,
                taskSuccessAmount = 0
            };

            return questData;
        }

        else
        {
            QuestSaveData questData = new QuestSaveData
            {
                questState = questState,
                questID = questID,
                taskIndex = taskIndex,
                taskSuccessAmount = questTasks[taskIndex].SuccessAmount
            };

            return questData;
        }
    }
    public void LoadQuest(QuestSaveData questData)
    {
        if (QuestID == questData.questID)
        {
            questState = questData.questState;

            for (int i = 0; i < QuestTasks.Length; ++i)
            {
                QuestTasks[i].OwnerQuest = this;
            }

            switch (questState)
            {
                case QUEST_STATE.ACTIVE:
                    {
                        taskIndex = questData.taskIndex;
                        questTasks[questData.taskIndex].StartTask();
                        questTasks[questData.taskIndex].SuccessAmount = questData.taskSuccessAmount;

                        onActiveQuest(this);
                        break;
                    }
                case QUEST_STATE.ACCEPT:
                    {
                        taskIndex = questData.taskIndex;
                        questTasks[questData.taskIndex].StartTask();
                        questTasks[questData.taskIndex].SuccessAmount = questData.taskSuccessAmount;

                        onActiveQuest(this);
                        onAcceptQuest(this);
                        break;
                    }
                case QUEST_STATE.COMPLETE:
                    {
                        onActiveQuest(this);
                        onAcceptQuest(this);
                        onCompleteQuest(this);
                        break;
                    }
            }
        }
    }

    #region Property
    public uint QuestID
    {
        get { return questID; }
        private set { questID = value; }
    }
    public string QuestTitle
    {
        get { return questTitle; }
        private set { questTitle = value; }
    }
    public int LevelCondition
    {
        get { return levelCondition; }
        private set { levelCondition = value; }
    }
    public uint QuestCondition
    {
        get { return questCondition; }
        private set { questCondition = value; }
    }
    public int TaskIndex
    {
        get { return taskIndex; }
        set
        {
            taskIndex = value;

            onTaskIndexChanged(this);

            if (taskIndex == QuestTasks.Length && questState == QUEST_STATE.ACCEPT)
            {
                CompleteQuest();
            }

            if (taskIndex == 1)
            {
                AcceptQuest();
            }

            if(taskIndex < QuestTasks.Length)
            {
                QuestTasks[taskIndex].StartTask();
            }
        }
    }
    public QuestTask[] QuestTasks
    {
        get { return questTasks; }
        private set { questTasks = value; }
    }
    public float RewardExperience
    {
        get { return rewardExperience; }
        private set { rewardExperience = value; }
    }
    public int RewardMoney
    {
        get { return rewardMoney; }
        private set { rewardMoney = value; }
    }
    public string[] RewardItems
    {
        get { return rewardItems; }
        private set { rewardItems = value; }
    }
    #endregion
}


