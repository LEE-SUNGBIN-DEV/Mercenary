using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;


public class ConfirmPanel : Panel
{
    // Private Variable
    [SerializeField] private TextMeshProUGUI confirmText;

    // Confirm Event
    public static UnityAction onConfirm;
    public static UnityAction onCancel;

    // Public Function
    public void ConfirmButton()
    {
        AudioManager.Instance.PlaySFX("Button Click");
        onConfirm();
    }

    public void CancelButton()
    {
        AudioManager.Instance.PlaySFX("Button Click");
        onConfirm = null;
        onCancel();
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
