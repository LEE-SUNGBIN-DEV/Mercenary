using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PANEL
{
    TitleScenePanel,
    SelectCharacterScenePanel,
    UserPanel,
    DialoguePanel,
    MonsterInformationPanel,
    MapInformationPanel,
    SystemNoticePanel
}

public enum POPUP
{
    ReturnPopup,
    InventoryPopup,
    StatusPopup,
    OptionPopup,
    HelpPopup,
    StorePopup,
    CampaignPopup,
    ConfirmPopup,
    QuestPopup
}

public class UIManager
{
    #region Event
    public static event UnityAction<bool> InteractPlayer;
    public event UnityAction<string> OnRequestNotice;
    public event UnityAction<string> OnRequestConfirm;
    #endregion

    private Canvas canvas;
    private LinkedList<UIPopup> currentPopUpLinkedList = new LinkedList<UIPopup>();
    private Dictionary<System.Type, UIPanel[]> panelDictionary = new Dictionary<System.Type, UIPanel[]>();
    private Dictionary<System.Type, UIPopup[]> popupDictionary = new Dictionary<System.Type, UIPopup[]>();

    public UIManager()
    {
        Quest.onTaskIndexChanged -= NoticeQuestState;
        Quest.onTaskIndexChanged += NoticeQuestState;
    }
    
    public void Initialize(GameObject gameObject)
    {
        canvas = gameObject.GetComponent<Canvas>();

        BindUI<UIPanel>(typeof(PANEL), panelDictionary);
        BindUI<UIPopup>(typeof(POPUP), popupDictionary);

        if (popupDictionary.TryGetValue(typeof(POPUP), out UIPopup[] popups))
        {
            for (int i = 0; i < popups.Length; ++i)
            {
                popups[i].OnFocus -= FocusPopup;
                popups[i].OnFocus += FocusPopup;
            }
        }
    }

    // Called By Managers
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentPopUpLinkedList.Count > 0)
            {
                ClosePopup(currentPopUpLinkedList.First.Value.PopupType);
            }
            else
            {
                Managers.GameManager.ToggleCursorMode();
            }
        }
    }

    public void BindUI<T>(System.Type type, Dictionary<System.Type, T[]> dictionary) where T : UIBase
    {
        string[] uiNameArray = System.Enum.GetNames(type);
        T[] uiArray = new T[uiNameArray.Length];
        dictionary.Add(typeof(T), uiArray);

        for (int i = 0; i < uiArray.Length; i++)
        {
            uiArray[i] = Functions.FindChild<T>(canvas.gameObject, uiNameArray[i], true);
            if (uiArray[i] == null)
            {
                Debug.Log($"Failed to bind({uiNameArray[i]})");
            }
        }
    }
    public T GetUI<T>(int index, Dictionary<System.Type, T[]> dictionary) where T : UIBase
    {
        if (dictionary.TryGetValue(typeof(T), out T[] uiArray) == false)
        {
            return null;
        }
        else
        {
            return uiArray[index];
        }
    }

    public void RequestNotice(string content)
    {
        OnRequestNotice(content);
    }
    public void RequestConfirm(string content, UnityAction action)
    {
        ConfirmPopup.OnConfirm -= action;
        ConfirmPopup.OnConfirm += action;
        OpenPopup(POPUP.ConfirmPopup);
    }

    public void NoticeQuestState(Quest quest)
    {
        if (quest.TaskIndex == 1)
        {
            RequestNotice("Äù½ºÆ® ¼ö¶ô");
        }

        if (quest.TaskIndex == quest.QuestTasks.Length)
        {
            RequestNotice("Äù½ºÆ® ¿Ï·á");
        }
    }

    #region Popup Function
    public void FocusPopup(UIPopup popUp)
    {
        currentPopUpLinkedList.Remove(popUp);
        currentPopUpLinkedList.AddFirst(popUp);
        RefreshPopupOrder();
    }

    public void RefreshPopupOrder()
    {
        foreach (UIPopup popUp in currentPopUpLinkedList)
        {
            popUp.transform.SetAsFirstSibling();
        }
    }
    public void OpenPopup(POPUP popupType)
    {
        UIPopup popup = GetUI((int)popupType, popupDictionary);

        if (currentPopUpLinkedList.Contains(popup) == false)
        {
            PlayPopupOpenSFX();
            currentPopUpLinkedList.AddFirst(popup);
            popup.gameObject.SetActive(true);
        }

        RefreshPopupOrder();
        Managers.GameManager.SetCursorMode(CURSOR_MODE.UNLOCK);
    }

    public void ClosePopup(POPUP popupType)
    {
        UIPopup popup = GetUI((int)popupType, popupDictionary);

        if (currentPopUpLinkedList.Contains(popup) == true)
        {
            PlayPopupCloseSFX();
            currentPopUpLinkedList.Remove(popup);
            popup.gameObject.SetActive(false);
        }

        RefreshPopupOrder();

        if (currentPopUpLinkedList.Count == 0)
        {
            Managers.GameManager.SetCursorMode(CURSOR_MODE.LOCK);
        }
    }
    public void TogglePopup(POPUP popupType)
    {
        UIPopup popup = GetUI((int)popupType, popupDictionary);
        if (popup.gameObject.activeSelf)
        {
            ClosePopup(popupType);
        }
        else
        {
            OpenPopup(popupType);
        }
    }

    public void TogglePopup(UIPopup popUp)
    {
        TogglePopup(popUp.PopupType);
    }
    #endregion

    #region Panel Function
    public void OpenPanel(PANEL panelType)
    {
        UIPanel panel = GetUI((int)panelType, panelDictionary);

        if (panelType == PANEL.DialoguePanel)
        {
            Managers.GameManager.SetCursorMode(CURSOR_MODE.UNLOCK);
        }
        panel.gameObject.SetActive(true);
    }
    public void ClosePanel(PANEL panelType)
    {
        UIPanel panel = GetUI((int)panelType, panelDictionary);

        if (panelType == PANEL.DialoguePanel)
        {
            Managers.GameManager.SetCursorMode(CURSOR_MODE.LOCK);
        }
        panel.gameObject.SetActive(false);
    }
    #endregion


    public void CloseAllUI()
    {
        for (int i = 0; i < panelDictionary.Count; ++i)
        {
            ClosePanel((PANEL)i);
        }

        for (int i = 0; i < popupDictionary.Count; ++i)
        {
            ClosePopup((POPUP)i);
        }
    }

    #region UI SFX Function
    public void PlayPopupOpenSFX()
    {
        Managers.AudioManager.PlaySFX("Popup Open");
    }
    public void PlayPopupCloseSFX()
    {
        Managers.AudioManager.PlaySFX("Popup Close");
    }
    #endregion

    #region Property
    public Canvas Canvas
    {
        get { return canvas; }
    }
    #endregion
}