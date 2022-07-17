using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class FunctionNPC : NPC, ITalkable
{
    #region Event
    public static event UnityAction<FunctionNPC> onDialogueStart;   // ���̾�αװ� ���� �� �� -> ����Ʈ ��ư ����Ʈ ��û
    public static event UnityAction onDialogueEnd;                  // ���̾�αװ� ���� �� �� -> ����Ʈ ��ư ����Ʈ �ʱ�ȭ

    public static event UnityAction<string, string> onTalkStart;    // ��ȭ�� ���۵� �� -> NPC �̸���, ��ȭ �����͸� ���ڷ� �Ѹ� -> ���̾�α� UIâ ����
    public static event UnityAction<uint> onTalkEnd;                // ��ȭ�� ������ �� -> �ش� ��ȭ�� DialogueID ���Ḧ �˸� -> ����Ʈ Ȯ��
    #endregion

    [TextArea]
    [SerializeField] private string[] defaultDialogues;
    [SerializeField] private GameObject questMark;
    private int dialogueIndex;
    private uint questID;

    [SerializeField] private List<Quest> questList;         // ����Ʈ ���

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
        // �⺻ ��ȭ ���
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
