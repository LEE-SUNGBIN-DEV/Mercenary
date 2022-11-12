using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class QuestPopup : UIPopup
{
    #region Event
    public static event UnityAction<QuestPopup> onClickAcceptButton;
    public static event UnityAction<QuestPopup> onClickCompleteButton;
    #endregion

    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject buttonCanvas;

    [SerializeField] private int questCount;
    [SerializeField] private List<QuestPopUpButton> questPopUpButtonList;

    [Header("Select Quest Information")]
    [SerializeField] private TextMeshProUGUI selectQuestTitleText;
    [SerializeField] private TextMeshProUGUI selectQuestDescription;
    [SerializeField] private TextMeshProUGUI selectMoneyRewardText;
    [SerializeField] private TextMeshProUGUI selectExperienceRewardText;

    [System.Serializable]
    public class QuestPopUpButton
    {
        #region Event
        public static event UnityAction<QuestPopUpButton> onClickButton;
        #endregion

        public bool isActive;
        public Quest quest;
        public Button button;
        public TextMeshProUGUI buttonText;

        public void OnClickButton()
        {
            onClickButton(this);
        }
    }

    private void Awake()
    {
        QuestPopUpButton.onClickButton -= SetQuestInformation;
        QuestPopUpButton.onClickButton += SetQuestInformation;

        for (int i=0; i< questCount; ++i)
        {
            CreateQuestButton();
        }
    }

    private void OnEnable()
    {
        SetAcceptList();
    }

    public void SetQuestInformation(QuestPopUpButton questPopUpButton)
    {
        if (questPopUpButton.quest.questState == QUEST_STATE.COMPLETE)
        {
            SelectQuestDescription.text = "완료한 퀘스트입니다.";
        }
        else
        {
            SelectQuestDescription.text = questPopUpButton.quest.QuestTasks[questPopUpButton.quest.TaskIndex].TaskDescription;
        }

        SelectQuestTitleText.text = questPopUpButton.quest.QuestTitle;
        SelectMoneyRewardText.text = questPopUpButton.quest.RewardMoney.ToString();
        SelectExperienceRewardText.text = questPopUpButton.quest.RewardExperience.ToString();
    }

    public void CreateQuestButton()
    {
        Button newButton = Instantiate(buttonPrefab).GetComponent<Button>();
        RectTransform rectTransform = newButton.GetComponent<RectTransform>();
        rectTransform.SetParent(buttonCanvas.transform);
        rectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        newButton.gameObject.SetActive(false);

        QuestPopUpButton newQuestPopUpButton = new QuestPopUpButton();
        newQuestPopUpButton.isActive = false;
        newQuestPopUpButton.quest = null;
        newQuestPopUpButton.button = newButton;
        newQuestPopUpButton.buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();

        QuestPopUpButtonList.Add(newQuestPopUpButton);
    }

    public void InitializeQuestPopUp()
    {
        for(int i=0; i<QuestPopUpButtonList.Count; ++i)
        {
            QuestPopUpButtonList[i].isActive = false;
            QuestPopUpButtonList[i].quest = null;
            QuestPopUpButtonList[i].buttonText.text = null;
            QuestPopUpButtonList[i].button.gameObject.SetActive(false);
        }

        SelectQuestTitleText.text = null;
        SelectQuestDescription.text = null;
        SelectMoneyRewardText.text = null;
        SelectExperienceRewardText.text = null;
    }

    public void SetAcceptList()
    {
        InitializeQuestPopUp();
        onClickAcceptButton(this);

    }
    public void SetCompleteList()
    {
        InitializeQuestPopUp();
        onClickCompleteButton(this);
    }

    #region Property
    public List<QuestPopUpButton> QuestPopUpButtonList
    {
        get { return questPopUpButtonList; }
        private set { questPopUpButtonList = value; }
    }
    public TextMeshProUGUI SelectQuestTitleText
    {
        get { return selectQuestTitleText; }
        private set { selectQuestTitleText = value; }
    }
    public TextMeshProUGUI SelectQuestDescription
    {
        get { return selectQuestDescription; }
        private set { selectQuestDescription = value; }
    }
    public TextMeshProUGUI SelectMoneyRewardText
    {
        get { return selectMoneyRewardText; }
        private set { selectMoneyRewardText = value; }
    }
    public TextMeshProUGUI SelectExperienceRewardText
    {
        get { return selectExperienceRewardText; }
        private set { selectExperienceRewardText = value; }
    }
    #endregion
}
