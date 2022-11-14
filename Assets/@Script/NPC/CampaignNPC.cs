using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignNPC : FunctionNPC
{
    public override void OpenNPCUI()
    {
        Managers.UIManager.UIGameScene.OpenPopup(Managers.UIManager.UIGameScene.CampaignPopup);
    }
    public override void CloseNPCUI()
    {
        Managers.UIManager.UIGameScene.ClosePopup(Managers.UIManager.UIGameScene.CampaignPopup);
    }

    public override void ActiveNPCFunctionButton()
    {
        //Managers.UIManager.DialoguePanel.ActiveNPCButton("√‚¡§");
    }
}
