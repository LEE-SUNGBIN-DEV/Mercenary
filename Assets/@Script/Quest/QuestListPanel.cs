using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class QuestListPanel : UIBase
{
    [System.Serializable]
    public class NPCQuestButton
    {
        public bool isActive;
        public uint questID;
        public FunctionNPC functionNPC;
        public Button button;
        public TextMeshProUGUI buttonText;
        
        public void OnClickQuestButton()
        {
            functionNPC.DialogueIndex = 0;
            functionNPC.QuestID = questID;
            functionNPC.OnTalk();
        }
    }
    
    [SerializeField] private List<NPCQuestButton> buttonInformations;

    public void ActiveQuestButton(FunctionNPC functionNPC)
    {
        int length = buttonInformations.Count > functionNPC.QuestList.Count ? functionNPC.QuestList.Count : buttonInformations.Count;

        for (int i = 0; i < length; ++i)
        {
            if (buttonInformations[i].isActive == false)
            {
                buttonInformations[i].isActive = true;
                buttonInformations[i].questID = functionNPC.QuestList[i].QuestID;
                buttonInformations[i].functionNPC = functionNPC;
                buttonInformations[i].buttonText.text = functionNPC.QuestList[i].QuestTitle;
                buttonInformations[i].button.onClick.AddListener(buttonInformations[i].OnClickQuestButton);
                buttonInformations[i].button.gameObject.SetActive(true);
            }
        }
    }

    public void InavtiveQuestButton()
    {
        for (int i = 0; i < buttonInformations.Count; ++i)
        {
            buttonInformations[i].isActive = false;
            buttonInformations[i].button.gameObject.SetActive(false);
        }
    }
}
