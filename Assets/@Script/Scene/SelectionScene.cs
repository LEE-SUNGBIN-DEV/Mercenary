using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionScene : BaseScene
{
    protected override void Awake()
    {
        base.Awake();
        sceneType = SCENE_TYPE.SELECTION;
        scene = SCENE_LIST.Selection;
    }

    public override void Initialize()
    {
        base.Initialize();
        Managers.UIManager.OpenPanel(PANEL.SelectCharacterScenePanel);
    }

    public override void ExitScene()
    {
        base.ExitScene();
        Managers.UIManager.ClosePanel(PANEL.SelectCharacterScenePanel);
    }
}
