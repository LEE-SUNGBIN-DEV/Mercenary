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
        if (Managers.DataManager.CurrentCharacterData != null)
        {
            Functions.CreateCharacterWithCamera(spawnPosition);
            Managers.UIManager.OpenUI(UI.UI_GameScene);
        }
    }

    public override void ExitScene()
    {
        base.ExitScene();
        Managers.UIManager.CloseUI(UI.UI_GameScene);
    }
}
