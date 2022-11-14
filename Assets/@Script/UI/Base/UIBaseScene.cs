using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBaseScene : UIBase
{
    protected bool isInitialized;
    protected LinkedList<UIPopup> currentPopUpLinkedList = new LinkedList<UIPopup>();

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentPopUpLinkedList.Count > 0)
            {
                ClosePopup(currentPopUpLinkedList.First.Value);
            }
            else
            {
                Managers.GameManager.ToggleCursorMode();
            }
        }
    }

    public abstract void Initialize();

    #region Popup Function
    public void FocusPopup(UIPopup popUp)
    {
        currentPopUpLinkedList.Remove(popUp);
        currentPopUpLinkedList.AddFirst(popUp);
        RefreshPopupOrder();
    }

    public void RefreshPopupOrder()
    {
        foreach (UIPopup popUp in currentPopUpLinkedList)
        {
            popUp.transform.SetAsFirstSibling();
        }
    }

    public void OpenPanel(UIPanel panel)
    {
        panel.gameObject.SetActive(true);
    }
    public void ClosePanel(UIPanel panel)
    {
        panel.gameObject.SetActive(false);
    }

    public void OpenPopup(UIPopup popup)
    {
        if (currentPopUpLinkedList.Contains(popup) == false)
        {
            PlayPopupOpenSFX();
            currentPopUpLinkedList.AddFirst(popup);
            popup.gameObject.SetActive(true);
            if (popup.IsInitialized == false)
            {
                popup.Initialize(FocusPopup);
            }
        }

        RefreshPopupOrder();
        Managers.GameManager.SetCursorMode(CURSOR_MODE.UNLOCK);
    }
    public void ClosePopup(UIPopup popup)
    {
        if (currentPopUpLinkedList.Contains(popup) == true)
        {
            PlayPopupCloseSFX();
            currentPopUpLinkedList.Remove(popup);
            popup.gameObject.SetActive(false);
        }

        RefreshPopupOrder();

        if (currentPopUpLinkedList.Count == 0)
        {
            Managers.GameManager.SetCursorMode(CURSOR_MODE.LOCK);
        }
    }

    public void TogglePopup(UIPopup popup)
    {
        if (popup.gameObject.activeSelf)
        {
            ClosePopup(popup);
        }
        else
        {
            OpenPopup(popup);
        }
    }
    #endregion

    #region Popup SFX Function
    public void PlayPopupOpenSFX()
    {
        Managers.AudioManager.PlaySFX("Audio_Popup_Open");
    }
    public void PlayPopupCloseSFX()
    {
        Managers.AudioManager.PlaySFX("Audio_Popup_Close");
    }
    #endregion
}
