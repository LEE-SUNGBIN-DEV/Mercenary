using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreNPC : FunctionNPC
{
    public override void OpenNPCUI()
    {
        UIManager.Instance.OpenPopUp(POPUP_TYPE.INVENTORY);
        UIManager.Instance.OpenPopUp(POPUP_TYPE.STORE);
    }

    public override void CloseNPCUI()
    {
        UIManager.Instance.ClosePopUp(POPUP_TYPE.INVENTORY);
        UIManager.Instance.ClosePopUp(POPUP_TYPE.STORE);
    }

    public override void ActiveNPCFunctionButton()
    {
        UIManager.Instance.DialoguePanel.ActiveNPCButton("ªÛ¡°");
    }
}
