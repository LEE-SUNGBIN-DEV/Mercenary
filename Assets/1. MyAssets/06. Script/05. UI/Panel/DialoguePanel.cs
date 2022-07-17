using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialoguePanel : Panel
{
    #region Event
    public static event UnityAction onClickFunctionButton;
    #endregion

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private Button npcFunctionButton;
    [SerializeField] private TextMeshProUGUI npcFunctionButtonText;
    [SerializeField] private QuestListPanel npcQuestListPanel;

    public void SetNameText(string name)
    {
        NameText.text = name;
    }
    public void SetContentText(string content)
    {
        ContentText.text = content;
    }

    public void SetDialogueText(string name, string content)
    {
        SetNameText(name);
        SetContentText(content);
    }
    
    public void ActiveNPCButton(string buttonName)
    {
        NpcFunctionButtonTexts.text = buttonName;
        NpcFunctionButtons.gameObject.SetActive(true);
    }

    public void OnClickFunctionButton()
    {
        onClickFunctionButton();
    }

    #region Property
    public TextMeshProUGUI NameText
    {
        get { return nameText; }
        private set { nameText = value; }
    }
    public TextMeshProUGUI ContentText
    {
        get { return contentText; }
        private set { contentText = value; }
    }
    public Button NpcFunctionButtons
    {
        get { return npcFunctionButton; }
        private set { npcFunctionButton = value; }
    }
    public TextMeshProUGUI NpcFunctionButtonTexts
    {
        get { return npcFunctionButtonText; }
        private set { npcFunctionButtonText = value; }
    }
    public QuestListPanel NpcQuestListPanel
    {
        get { return npcQuestListPanel; }
        private set { npcQuestListPanel = value; }
    }
    #endregion
}
