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
        Debug.Log("Start Game Button Pressed");
    }
    public void OnClickQuitButton()
    {
        Debug.Log("Quit Button Pressed");
    }
    public void OnClickOptionButton()
    {
        Debug.Log("Option Button Pressed");
    }
    #endregion
}
