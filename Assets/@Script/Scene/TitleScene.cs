using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    protected override void Awake()
    {
        base.Awake();
        sceneType = SCENE_TYPE.TITLE;
        scene = SCENE_LIST.Title;
    }
    public override void Initialize()
    {
        base.Initialize();
        Managers.UIManager.OpenPanel(PANEL.TitleScenePanel);
    }
}
