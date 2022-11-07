using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignNPC : FunctionNPC
{
    public override void OpenNPCUI()
    {
        Managers.UIManager.OpenPopup(POPUP.CampaignPopup);
    }
    public override void CloseNPCUI()
    {
        Managers.UIManager.ClosePopup(POPUP.CampaignPopup);
    }

    public override void ActiveNPCFunctionButton()
    {
        //Managers.UIManager.DialoguePanel.ActiveNPCButton("√‚¡§");
    }
}
