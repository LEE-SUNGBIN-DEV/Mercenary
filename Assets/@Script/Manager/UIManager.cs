using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum UI
{
    UI_TitleScene,
    UI_SelectCharacterScene,
    UI_GameScene,
    UI_CommonScene
}

public class UIManager
{
    #region Event
    public event UnityAction<bool> InteractPlayer;
    public event UnityAction<string> OnRequestNotice;
    #endregion

    private Canvas canvas;
    private Dictionary<UI, UIBaseScene> uiDictionary = new Dictionary<UI, UIBaseScene>();

    private UITitleScene uiTitleScene;
    private UISelectCharacterScene uiSelectCharacterScene;
    private UIGameScene uiGameScene;
    private UICommonScene uiCommonScene;

    public UIManager()
    {
        Quest.onTaskIndexChanged -= NoticeQuestState;
        Quest.onTaskIndexChanged += NoticeQuestState;
    }
    
    public void Initialize(GameObject canvasObject)
    {
        canvas = canvasObject.GetComponent<Canvas>();

        uiTitleScene = canvas.GetComponentInChildren<UITitleScene>(true);
        uiSelectCharacterScene = canvas.GetComponentInChildren<UISelectCharacterScene>(true);
        uiGameScene = canvas.GetComponentInChildren<UIGameScene>(true);
        uiCommonScene = canvas.GetComponentInChildren<UICommonScene>(true);

        uiDictionary.Add(UI.UI_TitleScene, uiTitleScene);
        uiDictionary.Add(UI.UI_SelectCharacterScene, uiSelectCharacterScene);
        uiDictionary.Add(UI.UI_GameScene, uiGameScene);
        uiDictionary.Add(UI.UI_CommonScene, uiCommonScene);

        OpenUI(UI.UI_CommonScene);
    }

    public void RequestNotice(string content)
    {
        OnRequestNotice(content);
    }

    public void RequestConfirm(string content, UnityAction action)
    {
        uiCommonScene.RequestConfirm(content, action);
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

    public void OpenUI(UI uiType)
    {
        uiDictionary.TryGetValue(uiType, out UIBaseScene targetUI);
        if(targetUI != null)
        {
            targetUI.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log($"{uiType}가 존재하지 않습니다.");
        }
    }
    public void CloseUI(UI uiType)
    {
        uiDictionary.TryGetValue(uiType, out UIBaseScene targetUI);
        if (targetUI != null)
        {
            targetUI.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log($"{uiType}가 존재하지 않습니다.");
        }
    }

    #region Property
    public Canvas Canvas { get { return canvas; } }
    public Dictionary<UI, UIBaseScene> UIDictionary { get { return uiDictionary; } }
    public UITitleScene UITitleScene { get { return uiTitleScene; } }
    public UISelectCharacterScene UISelectCharacterScene { get { return uiSelectCharacterScene; } }
    public UIGameScene UIGameScene { get { return uiGameScene; } }
    public UICommonScene UICommonScene { get { return uiCommonScene; } }
    #endregion
}