using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeNPC : FunctionNPC
{
    public override void OpenNPCUI()
    {

    }

    public override void CloseNPCUI()
    {
    }

    public override void ActiveNPCFunctionButton()
    {
        UIManager.Instance.DialoguePanel.ActiveNPCButton("¥Î¿Â∞£");
    }
}
