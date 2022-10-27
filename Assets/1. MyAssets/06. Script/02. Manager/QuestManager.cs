using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : Singleton<QuestManager>
{
    #region Event
    public static event UnityAction<Quest> onCompleteQuest;
    public static event UnityAction<FunctionNPC> onRequestNPCQuestList;
    #endregion

    [SerializeField] private Dictionary<uint, Quest> questDictionary;
    [SerializeField] private Quest[] mainQuestDatabase;
    [SerializeField] private Quest[] subQuestDatabase;

    [SerializeField] private List<Quest> inactiveQuestList;
    [SerializeField] private List<Quest> activeQuestList;
    [SerializeField] private List<Quest> acceptQuestList;
    [SerializeField] private List<Quest> completeQuestList;
    

    private void Start()
    {
        ClassifyByQuestState();
    }

    public override void Initialize()
    {
        QuestDictionary = new Dictionary<uint, Quest>();
        InactiveQuestList = new List<Quest>();
        ActiveQuestList = new List<Quest>();
        AcceptQuestList = new List<Quest>();
        CompleteQuestList = new List<Quest>();

        AddAllQuestToDictionary();

        #region Add Event
        CharacterData.onLoadPlayerData -= LoadQuest;
        CharacterData.onLoadPlayerData += LoadQuest;
        CharacterData.onSavePlayerData -= SaveQuest;
        CharacterData.onSavePlayerData += SaveQuest;

        GameManager.onCombatSceneLoaded -= RequestNPCQuestList;
        GameManager.onCombatSceneLoaded += RequestNPCQuestList;

        Quest.onActiveQuest -= ActiveQuest;
        Quest.onActiveQuest += ActiveQuest;

        Quest.onAcceptQuest -= AcceptQuest;
        Quest.onAcceptQuest += AcceptQuest;

        Quest.onCompleteQuest -= CompleteQuest;
        Quest.onCompleteQuest += CompleteQuest;

        QuestTask.onTaskEnd -= RequestNPCQuestList;
        QuestTask.onTaskEnd += RequestNPCQuestList;

        QuestPopUp.onClickAcceptButton -= RequestAcceptList;
        QuestPopUp.onClickAcceptButton += RequestAcceptList;

        QuestPopUp.onClickCompleteButton -= RequestCompleteList;
        QuestPopUp.onClickCompleteButton += RequestCompleteList;

        CharacterData.onMainQuestProcedureChanged -= RefreshInactiveQuest;
        CharacterData.onMainQuestProcedureChanged += RefreshInactiveQuest;
        #endregion
    }

    private void AddAllQuestToDictionary()
    {
        for (uint i = 0; i < MainQuestDatabase.Length; ++i)
        {
            QuestDictionary.Add(MainQuestDatabase[i].QuestID, MainQuestDatabase[i]);
        }

        for (uint i = 0; i < SubQuestDatabase.Length; ++i)
        {
            QuestDictionary.Add(SubQuestDatabase[i].QuestID, SubQuestDatabase[i]);
        }
    }

    public void ClassifyByQuestState()
    {
        foreach (var quest in QuestDictionary)
        {
            switch (quest.Value.questState)
            {
                case QUEST_STATE.INACTIVE:
                    {
                        InactiveQuestList.Add(quest.Value);
                        break;
                    }
                case QUEST_STATE.ACTIVE:
                    {
                        ActiveQuestList.Add(quest.Value);
                        break;
                    }
                case QUEST_STATE.ACCEPT:
                    {
                        AcceptQuestList.Add(quest.Value);
                        break;
                    }
                case QUEST_STATE.COMPLETE:
                    {
                        CompleteQuestList.Add(quest.Value);
                        break;
                    }
            }
        }
    }

    public void ActiveQuest(Quest quest)
    {
        if(InactiveQuestList.Contains(quest))
        {
            ActiveQuestList.Add(quest);
            InactiveQuestList.Remove(quest);
        }
    }
    public void AcceptQuest(Quest quest)
    {
        if (ActiveQuestList.Contains(quest))
        {
            AcceptQuestList.Add(quest);
            ActiveQuestList.Remove(quest);
        }
    }
    public void CompleteQuest(Quest quest)
    {
        if (AcceptQuestList.Contains(quest))
        {
            CompleteQuestList.Add(quest);
            AcceptQuestList.Remove(quest);
        }
        onCompleteQuest(quest);
    }

    #region Requst NPC Quest List
    // If there is any quest in NPC, Add quest to NPC quest List.
    public void RequestNPCQuestList()
    {
        foreach (var npc in NPCManager.Instance.NpcDictionary)
        {
            FunctionNPC functionNPC = npc.Value as FunctionNPC;
            if (functionNPC != null)
            {
                functionNPC.QuestList.Clear();

                for (int i = 0; i < AcceptQuestList.Count; ++i)
                {
                    DialogueTask dialogueTask = AcceptQuestList[i].QuestTasks[AcceptQuestList[i].TaskIndex] as DialogueTask;

                    if (dialogueTask != null && dialogueTask.NpcID == functionNPC.NpcID)
                    {
                        functionNPC.QuestList.Add(AcceptQuestList[i]);
                    }
                }

                for (int i = 0; i < ActiveQuestList.Count; ++i)
                {
                    DialogueTask dialogueTask = ActiveQuestList[i].QuestTasks[ActiveQuestList[i].TaskIndex] as DialogueTask;

                    if (dialogueTask != null && dialogueTask.NpcID == functionNPC.NpcID)
                    {
                        functionNPC.QuestList.Add(ActiveQuestList[i]);
                    }

                }

                onRequestNPCQuestList(functionNPC);
            }
        }
    }
    // Overloading for Quest Task Event
    public void RequestNPCQuestList(QuestTask questTask)
    {
        RequestNPCQuestList();
    }
    #endregion

    // 비활성화 목록을 검사하여 활성화 가능한 퀘스트가 있으면 활성화 리스트로 이동
    public void RefreshInactiveQuest(CharacterData playerData)
    {
        for (int i = 0; i < InactiveQuestList.Count; ++i)
        {
            if (playerData.Level >= InactiveQuestList[i].LevelCondition
                && playerData.MainQuestProcedure >= InactiveQuestList[i].QuestCondition)
            {
                InactiveQuestList[i].ActiveQuest();
                --i;
            }
        }
        RequestNPCQuestList();
    }

    public void RequestAcceptList(QuestPopUp questPopUp)
    {
        for (int i = 0; i < AcceptQuestList.Count; ++i)
        {
            if (questPopUp.QuestPopUpButtonList.Count < AcceptQuestList.Count)
            {
                questPopUp.CreateQuestButton();
            }

            if (questPopUp.QuestPopUpButtonList[i].isActive == false)
            {
                questPopUp.QuestPopUpButtonList[i].isActive = true;
                questPopUp.QuestPopUpButtonList[i].quest = AcceptQuestList[i];
                questPopUp.QuestPopUpButtonList[i].buttonText.text = questPopUp.QuestPopUpButtonList[i].quest.QuestTitle;
                questPopUp.QuestPopUpButtonList[i].button.onClick.AddListener(questPopUp.QuestPopUpButtonList[i].OnClickButton);
                questPopUp.QuestPopUpButtonList[i].button.gameObject.SetActive(true);
            }
        }
    }

    public void RequestCompleteList(QuestPopUp questPopUp)
    {
        for (int i = 0; i < CompleteQuestList.Count; ++i)
        {
            if (questPopUp.QuestPopUpButtonList.Count < CompleteQuestList.Count)
            {
                questPopUp.CreateQuestButton();
            }

            questPopUp.QuestPopUpButtonList[i].isActive = true;
            questPopUp.QuestPopUpButtonList[i].quest = CompleteQuestList[i];
            questPopUp.QuestPopUpButtonList[i].buttonText.text = questPopUp.QuestPopUpButtonList[i].quest.QuestTitle;
            questPopUp.QuestPopUpButtonList[i].button.onClick.AddListener(questPopUp.QuestPopUpButtonList[i].OnClickButton);
            questPopUp.QuestPopUpButtonList[i].button.gameObject.SetActive(true);
        }
    }

    #region Save & Load
    public void SaveQuest(PlayerSaveData playerSaveData)
    {
        for (int i = 0; i < MainQuestDatabase.Length; ++i)
        {
            playerSaveData.questSaveList.Add(MainQuestDatabase[i].SaveQuest());
        }

        for (int i = 0; i < SubQuestDatabase.Length; ++i)
        {
            playerSaveData.questSaveList.Add(SubQuestDatabase[i].SaveQuest());
        }
    }

    public void LoadQuest(PlayerSaveData playerSaveData)
    {
        for (int i = 0; i < playerSaveData.questSaveList.Count; ++i)
        {
            QuestDictionary[playerSaveData.questSaveList[i].questID].LoadQuest(playerSaveData.questSaveList[i]);
        }
    }
    #endregion

    #region Property
    public Dictionary<uint, Quest> QuestDictionary
    {
        get { return questDictionary; }
        private set { questDictionary = value; }
    }
    public Quest[] MainQuestDatabase
    {
        get { return mainQuestDatabase; }
        set { mainQuestDatabase = value; }
    }
    public Quest[] SubQuestDatabase
    {
        get { return subQuestDatabase; }
        set { subQuestDatabase = value; }
    }
    public List<Quest> InactiveQuestList
    {
        get { return inactiveQuestList; }
        private set { inactiveQuestList = value; }
    }
    public List<Quest> ActiveQuestList
    {
        get { return activeQuestList; }
        private set { activeQuestList = value; }
    }
    public List<Quest> AcceptQuestList
    {
        get { return acceptQuestList; }
        private set { acceptQuestList = value; }
    }
    public List<Quest> CompleteQuestList
    {
        get { return completeQuestList; }
        private set { completeQuestList = value; }
    }
    #endregion
}
