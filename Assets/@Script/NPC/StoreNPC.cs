using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreNPC : FunctionNPC
{
    public override void OpenNPCUI()
    {
        Managers.UIManager.OpenPopup(POPUP.InventoryPopup);
        Managers.UIManager.OpenPopup(POPUP.StorePopup);
    }

    public override void CloseNPCUI()
    {
        Managers.UIManager.ClosePopup(POPUP.InventoryPopup);
        Managers.UIManager.ClosePopup(POPUP.StorePopup);
    }

    public override void ActiveNPCFunctionButton()
    {
        //Managers.UIManager.DialoguePanel.ActiveNPCButton("ªÛ¡°");
    }
}
