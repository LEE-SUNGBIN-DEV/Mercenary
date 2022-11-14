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
        Managers.UIManager.OpenUI(UI.UI_TitleScene);
    }

    public override void ExitScene()
    {
        base.ExitScene();
        Managers.UIManager.CloseUI(UI.UI_TitleScene);
    }

    public void PlayButtonClickSound()
    {
        Managers.AudioManager.PlaySFX("Button Click");
    }

    public void StartGame()
    {
        PlayButtonClickSound();
    }

    public void QuitGame()
    {
        PlayButtonClickSound();
        Application.Quit();
    }
}
