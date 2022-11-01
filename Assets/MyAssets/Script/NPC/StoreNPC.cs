using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreNPC : FunctionNPC
{
    public override void OpenNPCUI()
    {
        Managers.UIManager.OpenPopUp(POPUP_TYPE.INVENTORY);
        Managers.UIManager.OpenPopUp(POPUP_TYPE.STORE);
    }

    public override void CloseNPCUI()
    {
        Managers.UIManager.ClosePopUp(POPUP_TYPE.INVENTORY);
        Managers.UIManager.ClosePopUp(POPUP_TYPE.STORE);
    }

    public override void ActiveNPCFunctionButton()
    {
        Managers.UIManager.DialoguePanel.ActiveNPCButton("ªÛ¡°");
    }
}
