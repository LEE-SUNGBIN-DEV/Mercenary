using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager
{
    #region Event
    public static event UnityAction<Quest> onCompleteQuest;
    public static event UnityAction<FunctionNPC> onRequestNPCQuestList;
    #endregion

    [SerializeField] private Dictionary<uint, Quest> questDictionary = new Dictionary<uint, Quest>();
    [SerializeField] private Quest[] mainQuestDatabase;
    [SerializeField] private Quest[] subQuestDatabase;

    [SerializeField] private List<Quest> inactiveQuestList = new List<Quest>();
    [SerializeField] private List<Quest> activeQuestList = new List<Quest>();
    [SerializeField] private List<Quest> acceptQuestList = new List<Quest>();
    [SerializeField] private List<Quest> completeQuestList = new List<Quest>();
    
    private void Start()
    {
        ClassifyByQuestState();
    }

    public void Initialize()
    {
        AddAllQuestToDictionary();

        #region Add Event
        Managers.DataManager.CurrentCharacter.CharacterData.OnLoadPlayerData -= LoadQuest;
        Managers.DataManager.CurrentCharacter.CharacterData.OnLoadPlayerData += LoadQuest;
        Managers.DataManager.CurrentCharacter.CharacterData.OnSavePlayerData -= SaveQuest;
        Managers.DataManager.CurrentCharacter.CharacterData.OnSavePlayerData += SaveQuest;

        Managers.GameSceneManager.OnSceneEnter -= RequestNPCQuestList;
        Managers.GameSceneManager.OnSceneEnter += RequestNPCQuestList;

        Quest.onActiveQuest -= ActiveQuest;
        Quest.onActiveQuest += ActiveQuest;

        Quest.onAcceptQuest -= AcceptQuest;
        Quest.onAcceptQuest += AcceptQuest;

        Quest.onCompleteQuest -= CompleteQuest;
        Quest.onCompleteQuest += CompleteQuest;

        QuestTask.onTaskEnd -= RequestNPCQuestList;
        QuestTask.onTaskEnd += RequestNPCQuestList;

        QuestPopup.onClickAcceptButton -= RequestAcceptList;
        QuestPopup.onClickAcceptButton += RequestAcceptList;

        QuestPopup.onClickCompleteButton -= RequestCompleteList;
        QuestPopup.onClickCompleteButton += RequestCompleteList;

        Managers.DataManager.CurrentCharacter.CharacterData.OnMainQuestProcedureChanged -= RefreshInactiveQuest;
        Managers.DataManager.CurrentCharacter.CharacterData.OnMainQuestProcedureChanged += RefreshInactiveQuest;
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
        foreach (var npc in Managers.NPCManager.NpcDictionary)
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
    public void RefreshInactiveQuest(CharacterData chracterData)
    {
        for (int i = 0; i < InactiveQuestList.Count; ++i)
        {
            if (chracterData.Level >= InactiveQuestList[i].LevelCondition
                && chracterData.MainQuestProcedure >= InactiveQuestList[i].QuestCondition)
            {
                InactiveQuestList[i].ActiveQuest();
                --i;
            }
        }
        RequestNPCQuestList();
    }

    public void RequestAcceptList(QuestPopup questPopUp)
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

    public void RequestCompleteList(QuestPopup questPopUp)
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
    public void SaveQuest(CharacterData characterData)
    {
        for (int i = 0; i < MainQuestDatabase.Length; ++i)
        {
            characterData.QuestSaveList.Add(MainQuestDatabase[i].SaveQuest());
        }

        for (int i = 0; i < SubQuestDatabase.Length; ++i)
        {
            characterData.QuestSaveList.Add(SubQuestDatabase[i].SaveQuest());
        }
    }

    public void LoadQuest(CharacterData characterData)
    {
        for (int i = 0; i < characterData.QuestSaveList.Count; ++i)
        {
            QuestDictionary[characterData.QuestSaveList[i].questID].LoadQuest(characterData.QuestSaveList[i]);
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
