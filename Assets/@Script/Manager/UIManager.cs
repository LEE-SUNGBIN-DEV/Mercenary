using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PANEL_TYPE
{
    TITLE = 0,
    DIALOGUE = 1,
    USER = 2,
    SYSTEM_NOTICE = 3,
    CONFIRM = 4,
    ENTRANCE = 5,
    BOSS = 6,

    SIZE
}

public enum POPUP_TYPE
{
    INVENTORY = 0,
    STATUS = 1,
    SETTING = 2,
    HELP = 3,
    QUEST = 4,
    STORE = 5,
    CAMPAIGN = 6,
    RETURN = 7,

    SIZE
}

public class UIManager
{
    #region Event
    public static event UnityAction<bool> InteractPlayer;
    #endregion

    private GameObject rootObject;
    private List<Panel> panelList;
    private List<Popup> popUpList;
    private LinkedList<Popup> currentPopUpLinkedList;
    private Dictionary<PANEL_TYPE, Panel> panelDictionary;
    private Dictionary<POPUP_TYPE, Popup> popupDictionary;

    // Panel
    [SerializeField] private TitlePanel titlePanel;
    [SerializeField] private DialoguePanel dialoguePanel;
    [SerializeField] private UserPanel userPanel;
    [SerializeField] private SystemNoticePanel systemNoticePanel;
    [SerializeField] private ConfirmPanel confirmPanel;
    [SerializeField] private EntrancePanel entrancePanel;
    [SerializeField] private ReturnPopup returnPanel;
    [SerializeField] private BossPanel bossPanel;

    // PopUp
    [SerializeField] private InventoryPopup inventoryPopUp;
    [SerializeField] private StatusPopup statusPopUp;
    [SerializeField] private SettingPopup settingPopUp;
    [SerializeField] private HelpPopup helpPopUp;
    [SerializeField] private QuestPopup questPopUp;
    [SerializeField] private StorePopup storePopUp;
    [SerializeField] private CampaignPopup campaignPopUp;

    private bool isNotice;
    private float noticeTime;
    private Queue<string> systemNoticeQueue;

    public UIManager()
    {
        isNotice = false;
        noticeTime = 0f;
        systemNoticeQueue = new Queue<string>();

        panelList = new List<Panel>()
            {
                TitlePanel,
                DialoguePanel,
                UserPanel,
                SystemNoticePanel,
                ConfirmPanel,
                EntrancePanel,
                BossPanel
            };

        popUpList = new List<Popup>()
            {
                StatusPopUp,
                InventoryPopUp,
                SettingPopUp,
                HelpPopUp,
                QuestPopUp,
                StorePopUp,
                CampaignPopUp,
                ReturnPopup,
            };

        panelDictionary = new Dictionary<PANEL_TYPE, Panel>()
            {
                {PANEL_TYPE.TITLE, TitlePanel },
                {PANEL_TYPE.DIALOGUE, DialoguePanel },
                {PANEL_TYPE.USER, UserPanel },
                {PANEL_TYPE.SYSTEM_NOTICE, SystemNoticePanel },
                {PANEL_TYPE.CONFIRM, ConfirmPanel },
                {PANEL_TYPE.ENTRANCE, EntrancePanel },
                {PANEL_TYPE.BOSS, BossPanel }
            };

        popupDictionary = new Dictionary<POPUP_TYPE, Popup>()
            {
                { POPUP_TYPE.HELP, HelpPopUp },
                { POPUP_TYPE.INVENTORY, InventoryPopUp},
                { POPUP_TYPE.QUEST, QuestPopUp },
                { POPUP_TYPE.SETTING, SettingPopUp },
                { POPUP_TYPE.STATUS, StatusPopUp },
                { POPUP_TYPE.STORE, StorePopUp },
                { POPUP_TYPE.CAMPAIGN, CampaignPopUp },
                { POPUP_TYPE.RETURN, ReturnPopup },
            };

        currentPopUpLinkedList = new LinkedList<Popup>();
    }

    public void Initialize()
    {
        #region Add Event

        Popup.onFocus -= FocusPopUp;
        Popup.onFocus += FocusPopUp;

        Quest.onTaskIndexChanged -= NoticeQuestState;
        Quest.onTaskIndexChanged += NoticeQuestState;

        ConfirmPanel.onCancel += () =>
        {
            ClosePanel(PANEL_TYPE.CONFIRM);
        };

        TitlePanel.onToggleHelpPopUp += () =>
        {
            TogglePopUp(POPUP_TYPE.HELP);
        };

        Slot.onItemDestroy += () =>
        {
            ConfirmPanel.SetConfirmText("아이템을 파괴하시겠습니까?");
            OpenPanel(PANEL_TYPE.CONFIRM);
        };
        #endregion

        #region Function NPC Event
        FunctionNPC.onTalkStart -= SetDialougeContent;
        FunctionNPC.onTalkStart += SetDialougeContent;

        FunctionNPC.onDialogueStart -= DialoguePanel.NpcQuestListPanel.ActiveQuestButton;
        FunctionNPC.onDialogueStart += DialoguePanel.NpcQuestListPanel.ActiveQuestButton;

        FunctionNPC.onDialogueEnd -= DialoguePanel.NpcQuestListPanel.InavtiveQuestButton;
        FunctionNPC.onDialogueEnd += DialoguePanel.NpcQuestListPanel.InavtiveQuestButton;
        #endregion

        // PlayerData Event
        CharacterData.OnPlayerDataChanged += (CharacterData playerData) =>
        {
            StatusPopUp.ClassText.text = playerData.PlayerClass;
            StatusPopUp.LevelText.text = playerData.Level.ToString();

            float expRatio = playerData.CurrentExperience / playerData.MaxExperience;
            UserPanel.SetUserExpBar(expRatio);

            StatusPopUp.StatPointText.text = playerData.StatPoint.ToString();
            StatusPopUp.StrengthText.text = playerData.Strength.ToString();
            StatusPopUp.VitalityText.text = playerData.Vitality.ToString();
            StatusPopUp.DexterityText.text = playerData.Dexterity.ToString();
            StatusPopUp.LuckText.text = playerData.Luck.ToString();
        };

        // CharacterStats Event
        CharacterStats.OnCharacterStatsChanged += (CharacterStats characterStats) =>
        {
            StatusPopUp.AttackPowerText.text = characterStats.AttackPower.ToString();
            StatusPopUp.DefensivePowerText.text = characterStats.DefensivePower.ToString();

            StatusPopUp.HitPointText.text = characterStats.CurrentHitPoint.ToString("F1") + "/" + characterStats.MaxHitPoint.ToString();
            float ratio = characterStats.CurrentHitPoint / characterStats.MaxHitPoint;
            UserPanel.SetUserHPBar(ratio);

            StatusPopUp.StaminaText.text = characterStats.CurrentStamina.ToString("F1") + "/" + characterStats.MaxStamina.ToString();
            ratio = characterStats.CurrentStamina / characterStats.MaxStamina;
            UserPanel.SetUserStaminaBar(ratio);

            StatusPopUp.AttackSpeedText.text = characterStats.AttackSpeed.ToString();
            StatusPopUp.MoveSpeedText.text = characterStats.MoveSpeed.ToString();
            StatusPopUp.CriticalChanceText.text = characterStats.CriticalChance.ToString();
            StatusPopUp.CriticalDamageText.text = characterStats.CriticalDamage.ToString();
        };

        OpenAllUI();
        CloseAllUI();
    }

    public void Update()
    {
        if (isNotice == false && systemNoticeQueue.Count != 0)
        {
            isNotice = true;
            noticeTime += Time.deltaTime;
            SystemNoticePanel.SystemNoticeText.text = systemNoticeQueue.Dequeue();
            OpenPanel(PANEL_TYPE.SYSTEM_NOTICE);

            if(noticeTime >= GameConstants.TIME_CLIENT_NOTICE)
            {
                isNotice = false;
                noticeTime = 0f;
                ClosePanel(PANEL_TYPE.SYSTEM_NOTICE);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentPopUpLinkedList.Count > 0)
            {
                ClosePopUp(currentPopUpLinkedList.First.Value.PopupType);
            }

            else
            {
                Managers.GameManager.ToggleCursorMode();
            }
        }

        if (Managers.GameSceneManager.CurrentScene.SceneType == SCENE_TYPE.VILIAGE
            || Managers.GameSceneManager.CurrentScene.SceneType == SCENE_TYPE.DUNGEON)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                TogglePopUp(POPUP_TYPE.SETTING);
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                TogglePopUp(POPUP_TYPE.INVENTORY);
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                TogglePopUp(POPUP_TYPE.STATUS);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                TogglePopUp(POPUP_TYPE.QUEST);
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                TogglePopUp(POPUP_TYPE.HELP);
            }
        }
    }

    public void RequestNotice(string content)
    {
        systemNoticeQueue.Enqueue(content);
    }

    public void NoticeQuestState(Quest quest)
    {
        if (quest.TaskIndex == 1)
        {
            RequestNotice("퀘스트 수락");
        }

        if (quest.TaskIndex == quest.QuestTasks.Length)
        {
            RequestNotice("퀘스트 완료");
        }
    }

    public void SetDialougeContent(string npcName, string dialogueData)
    {
        DialoguePanel.SetDialogueText(npcName, dialogueData);
        OpenPanel(PANEL_TYPE.DIALOGUE);
    }

    #region PopUp Function
    public void FocusPopUp(Popup popUp)
    {
        currentPopUpLinkedList.Remove(popUp);
        currentPopUpLinkedList.AddFirst(popUp);
        RefreshPopUpOrder();
    }

    public void RefreshPopUpOrder()
    {
        foreach (Popup popUp in currentPopUpLinkedList)
        {
            popUp.transform.SetAsFirstSibling();
        }
    }

    public void TogglePopUp(POPUP_TYPE popUpType)
    {
        if (popupDictionary[popUpType].gameObject.activeSelf)
        {
            ClosePopUp(popUpType);
        }
        else
        {
            OpenPopUp(popUpType);
        }
    }

    public void TogglePopUp(Popup popUp)
    {
        TogglePopUp(popUp.PopupType);
    }
    #endregion

    #region UI Open & Close Function
    public void OpenPopUp(POPUP_TYPE popUpType)
    {
        if (Managers.GameManager.CurrentCharacter.isActiveAndEnabled)
        {
            InteractPlayer(true);
        }

        if(currentPopUpLinkedList.Contains(popupDictionary[popUpType]) == false)
        {
            Managers.AudioManager.PlaySFX("PopUp Open");
            currentPopUpLinkedList.AddFirst(popupDictionary[popUpType]);
            popupDictionary[popUpType].gameObject.SetActive(true);
        }
        
        RefreshPopUpOrder();
        Managers.GameManager.SetCursorMode(CURSOR_MODE.UNLOCK);
    }

    public void ClosePopUp(POPUP_TYPE popUpType)
    {
        if (currentPopUpLinkedList.Contains(popupDictionary[popUpType]) == true)
        {
            Managers.AudioManager.PlaySFX("PopUp Close");
            currentPopUpLinkedList.Remove(popupDictionary[popUpType]);
            popupDictionary[popUpType].gameObject.SetActive(false);
        }
        
        RefreshPopUpOrder();

        if (currentPopUpLinkedList.Count == 0)
        {
            Managers.GameManager.SetCursorMode(CURSOR_MODE.LOCK);
            if (Managers.GameManager.CurrentCharacter.isActiveAndEnabled)
            {
                InteractPlayer(false);
            }
        }
    }
    public void OpenPanel(PANEL_TYPE panelType)
    {
        if(panelType == PANEL_TYPE.DIALOGUE)
        {
            if (Managers.GameManager.CurrentCharacter.isActiveAndEnabled)
            {
                InteractPlayer(true);
            }
            Managers.GameManager.SetCursorMode(CURSOR_MODE.UNLOCK);
        }
        panelDictionary[panelType].gameObject.SetActive(true);
    }
    public void ClosePanel(PANEL_TYPE panelType)
    {
        if (panelType == PANEL_TYPE.DIALOGUE)
        {
            if (Managers.GameManager.CurrentCharacter.isActiveAndEnabled)
            {
                InteractPlayer(false);
            }
            Managers.GameManager.SetCursorMode(CURSOR_MODE.LOCK);
        }
        panelDictionary[panelType].gameObject.SetActive(false);
    }    

    public void OpenAllUI()
    {
        for(int i=0; i<(int)PANEL_TYPE.SIZE; ++i)
        {
            OpenPanel((PANEL_TYPE)i);
        }

        for (int i=0; i<(int)POPUP_TYPE.SIZE; ++i)
        {
            OpenPopUp((POPUP_TYPE)i);
        }
    }
    public void CloseAllUI()
    {
        for (int i = 0; i < panelList.Count; ++i)
        {
            ClosePanel((PANEL_TYPE)i);
        }

        for (int i = 0; i < popUpList.Count; ++i)
        {
            ClosePopUp((POPUP_TYPE)i);
        }
    }
    #endregion

    #region Property
    public GameObject RootObject
    {
        get => rootObject;
    }
    public TitlePanel TitlePanel
    {
        get { return titlePanel; }
        private set { titlePanel = value; }
    }
    public DialoguePanel DialoguePanel
    {
        get { return dialoguePanel; }
        private set { dialoguePanel = value; }
    }
    public UserPanel UserPanel
    {
        get { return userPanel; }
        private set { userPanel = value; }
    }
    public CampaignPopup CampaignPopUp
    {
        get { return campaignPopUp; }
        private set { campaignPopUp = value; }
    }
    public SystemNoticePanel SystemNoticePanel
    {
        get { return systemNoticePanel; }
        private set { systemNoticePanel = value; }
    }
    public ConfirmPanel ConfirmPanel
    {
        get { return confirmPanel; }
        private set { confirmPanel = value; }
    }
    public EntrancePanel EntrancePanel
    {
        get { return entrancePanel; }
        private set { entrancePanel = value; }
    }
    public ReturnPopup ReturnPopup
    {
        get { return returnPanel; }
        private set { returnPanel = value; }
    }
    public BossPanel BossPanel
    {
        get { return bossPanel; }
        private set { bossPanel = value; }
    }
    public StatusPopup StatusPopUp
    {
        get { return statusPopUp; }
        private set { statusPopUp = value; }
    }
    public InventoryPopup InventoryPopUp
    {
        get { return inventoryPopUp; }
        private set { inventoryPopUp = value; }
    }
    public SettingPopup SettingPopUp
    {
        get { return settingPopUp; }
        private set { settingPopUp = value; }
    }
    public HelpPopup HelpPopUp
    {
        get { return helpPopUp; }
        private set { helpPopUp = value; }
    }
    public QuestPopup QuestPopUp
    {
        get { return questPopUp; }
        private set { questPopUp = value; }
    }
    public StorePopup StorePopUp
    {
        get { return storePopUp; }
        private set { storePopUp = value; }
    }
    #endregion
}