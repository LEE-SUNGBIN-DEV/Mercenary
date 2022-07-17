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
    RETURN = 6,
    BOSS = 7,

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

    SIZE
}

public class UIManager : Singleton<UIManager>
{
    #region Event
    public static event UnityAction<bool> InteractPlayer;
    #endregion
    private List<Panel> panelList;
    private List<PopUp> popUpList;
    private LinkedList<PopUp> currentPopUpLinkedList;
    private Dictionary<PANEL_TYPE, Panel> panelDictionary;
    private Dictionary<POPUP_TYPE, PopUp> popupDictionary;

    // Panel
    [SerializeField] private TitlePanel titlePanel;
    [SerializeField] private DialoguePanel dialoguePanel;
    [SerializeField] private UserPanel userPanel;
    [SerializeField] private SystemNoticePanel systemNoticePanel;
    [SerializeField] private ConfirmPanel confirmPanel;
    [SerializeField] private EntrancePanel entrancePanel;
    [SerializeField] private ReturnPanel returnPanel;
    [SerializeField] private BossPanel bossPanel;

    // PopUp
    [SerializeField] private InventoryPopUp inventoryPopUp;
    [SerializeField] private StatusPopUp statusPopUp;
    [SerializeField] private SettingPopUp settingPopUp;
    [SerializeField] private HelpPopUp helpPopUp;
    [SerializeField] private QuestPopUp questPopUp;
    [SerializeField] private StorePopUp storePopUp;
    [SerializeField] private CampaignPopUp campaignPopUp;

    private bool isNotice;
    private Queue<string> systemNoticeQueue;

    public override void Initialize()
    {
        isNotice = false;
        systemNoticeQueue = new Queue<string>();

        panelList = new List<Panel>()
            {
                TitlePanel,
                DialoguePanel,
                UserPanel,
                SystemNoticePanel,
                ConfirmPanel,
                EntrancePanel,
                ReturnPanel,
                BossPanel
            };

        popUpList = new List<PopUp>()
            {
                StatusPopUp,
                InventoryPopUp,
                SettingPopUp,
                HelpPopUp,
                QuestPopUp,
                StorePopUp,
                CampaignPopUp,
            };

        panelDictionary = new Dictionary<PANEL_TYPE, Panel>()
            {
                {PANEL_TYPE.TITLE, TitlePanel },
                {PANEL_TYPE.DIALOGUE, DialoguePanel },
                {PANEL_TYPE.USER, UserPanel },
                {PANEL_TYPE.SYSTEM_NOTICE, SystemNoticePanel },
                {PANEL_TYPE.CONFIRM, ConfirmPanel },
                {PANEL_TYPE.ENTRANCE, EntrancePanel },
                {PANEL_TYPE.RETURN, ReturnPanel },
                {PANEL_TYPE.BOSS, BossPanel }
            };

        popupDictionary = new Dictionary<POPUP_TYPE, PopUp>()
            {
                { POPUP_TYPE.HELP, HelpPopUp },
                { POPUP_TYPE.INVENTORY, InventoryPopUp},
                { POPUP_TYPE.QUEST, QuestPopUp },
                { POPUP_TYPE.SETTING, SettingPopUp },
                { POPUP_TYPE.STATUS, StatusPopUp },
                { POPUP_TYPE.STORE, StorePopUp },
                { POPUP_TYPE.CAMPAIGN, CampaignPopUp },
            };

        currentPopUpLinkedList = new LinkedList<PopUp>();

        #region Add Event

        PopUp.onFocus -= FocusPopUp;
        PopUp.onFocus += FocusPopUp;

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

        GameManager.onClickNewGameButton += () =>
        {
            ConfirmPanel.SetConfirmText("플레이 데이터가 존재합니다.\n새로 시작하시겠습니까?");
            OpenPanel(PANEL_TYPE.CONFIRM);
        };

        #region Function NPC Event
        FunctionNPC.onTalkStart -= SetDialougeContent;
        FunctionNPC.onTalkStart += SetDialougeContent;

        FunctionNPC.onDialogueStart -= DialoguePanel.NpcQuestListPanel.ActiveQuestButton;
        FunctionNPC.onDialogueStart += DialoguePanel.NpcQuestListPanel.ActiveQuestButton;

        FunctionNPC.onDialogueEnd -= DialoguePanel.NpcQuestListPanel.InavtiveQuestButton;
        FunctionNPC.onDialogueEnd += DialoguePanel.NpcQuestListPanel.InavtiveQuestButton;
        #endregion

        #region PlayerData Event
        PlayerData.onPlayerClassChanged += (PlayerData playerData) =>
        {
            StatusPopUp.ClassText.text = playerData.PlayerClass;
        };
        PlayerData.onLevelChanged += (PlayerData playerData) =>
        {
            StatusPopUp.LevelText.text = playerData.Level.ToString();
        };
        PlayerData.onCurrentExperienceChanged += (PlayerData playerData) =>
        {
            float ratio = playerData.CurrentExperience / playerData.MaxExperience;
            UserPanel.SetUserExpBar(ratio);
        };
        PlayerData.onMaxExperienceChanged += (PlayerData playerData) =>
        {
            float ratio = playerData.CurrentExperience / playerData.MaxExperience;
            UserPanel.SetUserExpBar(ratio);
        };
        PlayerData.onStatPointChanged += (PlayerData playerData) =>
        {
            StatusPopUp.StatPointText.text = playerData.StatPoint.ToString();
        };
        PlayerData.onStrengthChanged += (PlayerData playerData) =>
        {
            StatusPopUp.StrengthText.text = playerData.Strength.ToString();
        };
        PlayerData.onVitalityChanged += (PlayerData playerData) =>
        {
            StatusPopUp.VitalityText.text = playerData.Vitality.ToString();
        };
        PlayerData.onDexterityChanged += (PlayerData playerData) =>
        {
            StatusPopUp.DexterityText.text = playerData.Dexterity.ToString();
        };
        PlayerData.onLuckChanged += (PlayerData playerData) =>
        {
            StatusPopUp.LuckText.text = playerData.Luck.ToString();
        };
        #endregion

        #region Player Event
        Player.onDie += (Player player) =>
        {
            OpenPanel(PANEL_TYPE.RETURN);
        };
        Player.onAttackPowerChanged += (Player player) =>
        {
            StatusPopUp.AttackPowerText.text = player.AttackPower.ToString();
        };
        Player.onDefensivePowerChanged += (Player player) =>
        {
            StatusPopUp.DefensivePowerText.text = player.DefensivePower.ToString();
        };
        Player.onMaxHitPointChanged += (Player player) =>
        {
            StatusPopUp.HitPointText.text = player.CurrentHitPoint.ToString("F1") + "/" + player.MaxHitPoint.ToString();
            float ratio = player.CurrentHitPoint / player.MaxHitPoint;
            UserPanel.SetUserHPBar(ratio);
        };
        Player.onCurrentHitPointChanged += (Player player) =>
        {
            StatusPopUp.HitPointText.text = player.CurrentHitPoint.ToString("F1") + "/" + player.MaxHitPoint.ToString();
            float ratio = player.CurrentHitPoint / player.MaxHitPoint;
            UserPanel.SetUserHPBar(ratio);
        };
        Player.onMaxStaminaChanged += (Player player) =>
        {
            StatusPopUp.StaminaText.text = player.CurrentStamina.ToString("F1") + "/" + player.MaxStamina.ToString();
            float ratio = player.CurrentStamina / player.MaxStamina;
            UserPanel.SetUserStaminaBar(ratio);
        };
        Player.onCurrentStaminaChanged += (Player player) =>
        {
            StatusPopUp.StaminaText.text = player.CurrentStamina.ToString("F1") + "/" + player.MaxStamina.ToString();
            float ratio = player.CurrentStamina / player.MaxStamina;
            UserPanel.SetUserStaminaBar(ratio);
        };
        Player.onAttackSpeedChanged += (Player player) =>
        {
            StatusPopUp.AttackSpeedText.text = player.AttackSpeed.ToString();
        };
        Player.onMoveSpeedChanged += (Player player) =>
        {
            StatusPopUp.MoveSpeedText.text = player.MoveSpeed.ToString();
        };
        Player.onCriticalChanceChanged += (Player player) =>
        {
            StatusPopUp.CriticalChanceText.text = player.CriticalChance.ToString();
        };
        Player.onCriticalDamageChanged += (Player player) =>
        {
            StatusPopUp.CriticalDamageText.text = player.CriticalDamage.ToString();
        };
        #endregion

        #region Scene Loaded Event
        GameManager.onSceneLoaded += () =>
        {
            CloseAllUI();
        };
        GameManager.onTitleSceneLoaded += () =>
        {
            OpenPanel(PANEL_TYPE.TITLE);
        };
        GameManager.onCombatSceneLoaded += () =>
        {
            OpenPanel(PANEL_TYPE.USER);
        };
        #endregion
        #endregion

        OpenAllUI();
        CloseAllUI();
    }

    private void Update()
    {
        if (isNotice == false && systemNoticeQueue.Count != 0)
        {
            isNotice = true;
            SystemNoticePanel.SystemNoticeText.text = systemNoticeQueue.Dequeue();
            OpenPanel(PANEL_TYPE.SYSTEM_NOTICE);
            StartCoroutine(WaitForNotice());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentPopUpLinkedList.Count > 0)
            {
                ClosePopUp(currentPopUpLinkedList.First.Value.PopUpType);
            }

            else
            {
                GameManager.Instance.ToggleCursorMode();
            }
        }

        if (GameManager.Instance.CurrentSceneName != "Title")
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

    private IEnumerator WaitForNotice()
    {
        yield return new WaitForSeconds(Constant.noticeTime);
        isNotice = false;
        ClosePanel(PANEL_TYPE.SYSTEM_NOTICE);
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
    public void FocusPopUp(PopUp popUp)
    {
        currentPopUpLinkedList.Remove(popUp);
        currentPopUpLinkedList.AddFirst(popUp);
        RefreshPopUpOrder();
    }

    public void RefreshPopUpOrder()
    {
        foreach (PopUp popUp in currentPopUpLinkedList)
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

    public void TogglePopUp(PopUp popUp)
    {
        TogglePopUp(popUp.PopUpType);
    }
    #endregion

    #region UI Open & Close Function
    public void OpenPopUp(POPUP_TYPE popUpType)
    {
        if (GameManager.Instance.Player.isActiveAndEnabled)
        {
            InteractPlayer(true);
        }

        if(currentPopUpLinkedList.Contains(popupDictionary[popUpType]) == false)
        {
            AudioManager.Instance.PlaySFX("PopUp Open");
            currentPopUpLinkedList.AddFirst(popupDictionary[popUpType]);
            popupDictionary[popUpType].gameObject.SetActive(true);
        }
        
        RefreshPopUpOrder();
        GameManager.Instance.SetCursorMode(CURSOR_MODE.UNLOCK);
    }

    public void ClosePopUp(POPUP_TYPE popUpType)
    {
        if (currentPopUpLinkedList.Contains(popupDictionary[popUpType]) == true)
        {
            AudioManager.Instance.PlaySFX("PopUp Close");
            currentPopUpLinkedList.Remove(popupDictionary[popUpType]);
            popupDictionary[popUpType].gameObject.SetActive(false);
        }
        
        RefreshPopUpOrder();

        if (currentPopUpLinkedList.Count == 0)
        {
            GameManager.Instance.SetCursorMode(CURSOR_MODE.LOCK);
            if (GameManager.Instance.Player.isActiveAndEnabled)
            {
                InteractPlayer(false);
            }
        }
    }
    public void OpenPanel(PANEL_TYPE panelType)
    {
        if(panelType == PANEL_TYPE.DIALOGUE)
        {
            if (GameManager.Instance.Player.isActiveAndEnabled)
            {
                InteractPlayer(true);
            }
            GameManager.Instance.SetCursorMode(CURSOR_MODE.UNLOCK);
        }
        panelDictionary[panelType].gameObject.SetActive(true);
    }
    public void ClosePanel(PANEL_TYPE panelType)
    {
        if (panelType == PANEL_TYPE.DIALOGUE)
        {
            if (GameManager.Instance.Player.isActiveAndEnabled)
            {
                InteractPlayer(false);
            }
            GameManager.Instance.SetCursorMode(CURSOR_MODE.LOCK);
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
    public CampaignPopUp CampaignPopUp
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
    public ReturnPanel ReturnPanel
    {
        get { return returnPanel; }
        private set { returnPanel = value; }
    }
    public BossPanel BossPanel
    {
        get { return bossPanel; }
        private set { bossPanel = value; }
    }
    public StatusPopUp StatusPopUp
    {
        get { return statusPopUp; }
        private set { statusPopUp = value; }
    }
    public InventoryPopUp InventoryPopUp
    {
        get { return inventoryPopUp; }
        private set { inventoryPopUp = value; }
    }
    public SettingPopUp SettingPopUp
    {
        get { return settingPopUp; }
        private set { settingPopUp = value; }
    }
    public HelpPopUp HelpPopUp
    {
        get { return helpPopUp; }
        private set { helpPopUp = value; }
    }
    public QuestPopUp QuestPopUp
    {
        get { return questPopUp; }
        private set { questPopUp = value; }
    }
    public StorePopUp StorePopUp
    {
        get { return storePopUp; }
        private set { storePopUp = value; }
    }
    #endregion
}
