using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class FunctionNPC : NPC, ITalkable
{
    #region Event
    public static event UnityAction<FunctionNPC> onDialogueStart;   // 다이얼로그가 시작 될 때 -> 퀘스트 버튼 리스트 요청
    public static event UnityAction onDialogueEnd;                  // 다이얼로그가 종료 될 때 -> 퀘스트 버튼 리스트 초기화

    public static event UnityAction<string, string> onTalkStart;    // 대화가 시작될 때 -> NPC 이름과, 대화 데이터를 인자로 뿌림 -> 다이얼로그 UI창 열림
    public static event UnityAction<uint> onTalkEnd;                // 대화가 끝났을 때 -> 해당 대화의 DialogueID 종료를 알림 -> 퀘스트 확인
    #endregion

    [TextArea]
    [SerializeField] private string[] defaultDialogues;
    [SerializeField] private GameObject questMark;
    private int dialogueIndex;
    private uint questID;

    [SerializeField] private List<Quest> questList;         // 퀘스트 목록

    private void Awake()
    {
        QuestManager.onRequestNPCQuestList -= SetQuestMark;
        QuestManager.onRequestNPCQuestList += SetQuestMark;
    }

    public override void OnEnable()
    {
        base.OnEnable();

        CanTalk = false;
        IsTalk = false;
        dialogueIndex = 0;
        questID = 0;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        QuestList.Clear();
        DialoguePanel.onClickFunctionButton -= OpenNPCUI;
    }

    private void Start()
    {
        InitializeNPCData();
    }

    private void Update()
    {
        if (CanTalk == true && Input.GetKeyDown(KeyCode.G))
        {
            OnDialogue();
        }

        else if(IsTalk == true && Input.GetKeyDown(KeyCode.G))
        {
            OnTalk();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            CanTalk = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            OffTalk();
        }
    }
    public void InitializeNPCData()
    {
        // 기본 대화 등록
        if (!DialogueManager.Instance.DialogueDictionary.ContainsKey(NpcID))
        {
            DialogueManager.Instance.DialogueDictionary.Add(NpcID, defaultDialogues);
        }
    }
    public void SetQuestMark(FunctionNPC functionNPC)
    {
        if (functionNPC == this)
        {
            if (functionNPC.questList.Count > 0)
            {
                QuestMark.SetActive(true);
            }

            else
            {
                QuestMark.SetActive(false);
            }
        }
    }

    public void OnDialogue()
    {
        DialoguePanel.onClickFunctionButton -= OpenNPCUI;
        DialoguePanel.onClickFunctionButton += OpenNPCUI;

        CanTalk = false;
        onDialogueStart(this);
        ActiveNPCFunctionButton();

        OnTalk();
    }

    public void OnTalk()
    {
        // Set Talk Infomation
        uint dialogueID = QuestID + NpcID;
        string dialogueData = DialogueManager.Instance.GetDialogue(dialogueID, DialogueIndex);

        // Talk Progress
        if (dialogueData != null)
        {
            IsTalk = true;
            ++DialogueIndex;

            onTalkStart(NpcName, dialogueData);
        }

        // Talk End
        else
        {
            onTalkEnd(dialogueID);
            OffTalk();
            CanTalk = true;
        }
    }

    public void OffTalk()
    {
        CanTalk = false;
        IsTalk = false;
        DialogueIndex = 0;
        QuestID = 0;

        onDialogueEnd();
        CloseNPCUI();

        DialoguePanel.onClickFunctionButton -= OpenNPCUI;
        UIManager.Instance.ClosePanel(PANEL_TYPE.DIALOGUE);
    }

    public abstract void OpenNPCUI();
    public abstract void CloseNPCUI();
    public abstract void ActiveNPCFunctionButton();

    #region Property
    public bool CanTalk { get; set; }
    public bool IsTalk { get; set; }
    public string[] DefaultTalk
    {
        get { return defaultDialogues; }
        private set { defaultDialogues = value; }
    }
    public int DialogueIndex
    {
        get { return dialogueIndex; }
        set { dialogueIndex = value; }
    }

    public uint QuestID
    {
        get { return questID; }
        set { questID = value; }
    }
    public GameObject QuestMark
    {
        get { return questMark; }
    }
    public List<Quest> QuestList
    {
        get { return questList; }
    }
    #endregion
}
