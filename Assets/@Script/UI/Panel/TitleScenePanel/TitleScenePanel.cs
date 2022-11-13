using UnityEngine;
using UnityEngine.Events;

public class TitleScenePanel : UIPanel
{
    enum TEXT
    {
        TitleText,
    }
    enum BUTTON
    {
        StartGameButton,
        QuitButton,
        OptionButton
    }

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        BindText(typeof(TEXT));
        BindButton(typeof(BUTTON));
        BindEvent(GetButton((int)BUTTON.StartGameButton).gameObject, OnClickStartGameButton, UI_EVENT.CLICK);
        BindEvent(GetButton((int)BUTTON.QuitButton).gameObject, OnClickQuitButton, UI_EVENT.CLICK);
        BindEvent(GetButton((int)BUTTON.OptionButton).gameObject, OnClickOptionButton, UI_EVENT.CLICK);
    }

    #region Event Function
    public void OnClickStartGameButton()
    {
        Managers.GameSceneManager.LoadScene(SCENE_LIST.Selection);
    }
    public void OnClickQuitButton()
    {
        Managers.GameManager.SaveAndQuit();
    }
    public void OnClickOptionButton()
    {
        Managers.UIManager.TogglePopup(POPUP.OptionPopup);
    }
    #endregion
}
