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
        BindEvent(GetButton((int)BUTTON.StartGameButton).gameObject, StartGameButton, UI_EVENT.CLICK);
        BindEvent(GetButton((int)BUTTON.QuitButton).gameObject, QuitButton, UI_EVENT.CLICK);
        BindEvent(GetButton((int)BUTTON.OptionButton).gameObject, OptionButton, UI_EVENT.CLICK);
    }

    #region Event Function
    public void StartGameButton()
    {
        Debug.Log("Start Game Button Pressed");
    }
    public void QuitButton()
    {
        Debug.Log("Quit Button Pressed");
    }
    public void OptionButton()
    {
        Debug.Log("Option Button Pressed");
    }
    #endregion
}
