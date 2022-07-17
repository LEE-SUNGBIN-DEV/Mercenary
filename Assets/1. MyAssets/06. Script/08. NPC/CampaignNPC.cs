using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignNPC : FunctionNPC
{
    public override void OpenNPCUI()
    {
        UIManager.Instance.OpenPopUp(POPUP_TYPE.CAMPAIGN);
    }
    public override void CloseNPCUI()
    {
        UIManager.Instance.ClosePopUp(POPUP_TYPE.CAMPAIGN);
    }

    public override void ActiveNPCFunctionButton()
    {
        UIManager.Instance.DialoguePanel.ActiveNPCButton("√‚¡§");
    }
}
