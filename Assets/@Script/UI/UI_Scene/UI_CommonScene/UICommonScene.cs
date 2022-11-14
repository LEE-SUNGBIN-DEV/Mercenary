using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UICommonScene : UIBaseScene
{
    private ConfirmPanel confirmPanel;
    private NoticePanel noticePanel;

    private OptionPopup optionPopup;

    public override void Initialize()
    {
        if (isInitialized == true)
        {
            Debug.Log($"{this}: Already Initialized.");
            return;
        }
        isInitialized = true;

        // Panel
        confirmPanel = gameObject.GetComponentInChildren<ConfirmPanel>(true);
        noticePanel = gameObject.GetComponentInChildren<NoticePanel>(true);

        // Popup
        optionPopup = gameObject.GetComponentInChildren<OptionPopup>(true);
    }

    public void RequestNotice()
    {

    }
    public void RequestConfirm(string content, UnityAction action)
    {
        confirmPanel.SetConfirmPanel(content, action);
        OpenPanel(confirmPanel);
    }

    #region Property
    public ConfirmPanel ConfirmPanel { get { return confirmPanel; } }
    public NoticePanel NoticePanel { get { return noticePanel; } }
    public OptionPopup OptionPopup { get { return optionPopup; } }
    #endregion
}
