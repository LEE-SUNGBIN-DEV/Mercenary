using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignNPC : FunctionNPC
{
    public override void OpenNPCUI()
    {
        Managers.UIManager.OpenPopUp(POPUP_TYPE.CAMPAIGN);
    }
    public override void CloseNPCUI()
    {
        Managers.UIManager.ClosePopUp(POPUP_TYPE.CAMPAIGN);
    }

    public override void ActiveNPCFunctionButton()
    {
        Managers.UIManager.DialoguePanel.ActiveNPCButton("√‚¡§");
    }
}
