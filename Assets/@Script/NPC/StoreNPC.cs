using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreNPC : FunctionNPC
{
    public override void OpenNPCUI()
    {
        Managers.UIManager.UIGameScene.OpenPopup(Managers.UIManager.UIGameScene.InventoryPopup);
        Managers.UIManager.UIGameScene.OpenPopup(Managers.UIManager.UIGameScene.StorePopup);
    }

    public override void CloseNPCUI()
    {
        Managers.UIManager.UIGameScene.ClosePopup(Managers.UIManager.UIGameScene.InventoryPopup);
        Managers.UIManager.UIGameScene.ClosePopup(Managers.UIManager.UIGameScene.StorePopup);
    }

    public override void ActiveNPCFunctionButton()
    {
        //Managers.UIManager.DialoguePanel.ActiveNPCButton("ªÛ¡°");
    }
}
