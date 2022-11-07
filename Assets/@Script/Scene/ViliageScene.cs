using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViliageScene : BaseScene
{
    protected override void Awake()
    {
        base.Awake();
        sceneType = SCENE_TYPE.VILIAGE;
    }

    public override void Initialize()
    {
        base.Initialize();
        Managers.UIManager.OpenPanel(PANEL.UserPanel);
    }
}
