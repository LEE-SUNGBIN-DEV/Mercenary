using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;


public class ConfirmPopup : UIPopup
{
    [SerializeField] private TextMeshProUGUI confirmText;

    public static event UnityAction OnConfirm;
    public static event UnityAction OnCancel;

    public void ConfirmButton()
    {
        Managers.AudioManager.PlaySFX("Button Click");
        OnConfirm();
    }

    public void CancelButton()
    {
        Managers.AudioManager.PlaySFX("Button Click");
        OnConfirm = null;
        Managers.UIManager.ClosePopup(POPUP.ConfirmPopup);
    }

    public void SetConfirmText(string _content)
    {
        confirmText.text = _content;
    }

    #region Property
    public TextMeshProUGUI ConfirmText
    {
        get { return confirmText; }
        private set { confirmText = value; }
    }
    #endregion
}
