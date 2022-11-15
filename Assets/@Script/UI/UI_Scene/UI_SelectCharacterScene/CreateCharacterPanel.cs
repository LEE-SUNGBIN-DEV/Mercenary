using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CreateCharacterPanel : UIBase
{
    public event UnityAction OnOpenPanel;
    public event UnityAction OnClosePanel;

    public enum BUTTON
    {
        CreateButton,
        CancelButton,

        LancerButton
    }

    private CharacterSlot selectSlot;
    private CHARACTER_CLASS selectClass;
    private Animator animator;
    private bool isOpen;

    public void Initialize(CharacterSlot targetSlot)
    {
        animator = GetComponent<Animator>();

        BindButton(typeof(BUTTON));

        SetSlot(targetSlot);
        selectClass = CHARACTER_CLASS.Null;

        GetButton((int)BUTTON.LancerButton).onClick.AddListener(() => { OnClickCharacterButton(CHARACTER_CLASS.Lancer); });
        GetButton((int)BUTTON.CreateButton).onClick.AddListener(() => { OnClickCreateButton(); });
        GetButton((int)BUTTON.CancelButton).onClick.AddListener(OnClickCancelButton);

        isOpen = false;
    }

    public void OpenPanel()
    {
        GetButton((int)BUTTON.CreateButton).interactable = false;
        GetButton((int)BUTTON.CancelButton).interactable = true;
        SetAnimation(true);
        OnOpenPanel();
        isOpen = true;
    }
    public void ClosePanel()
    { 
        if (isOpen)
        {
            GetButton((int)BUTTON.CreateButton).interactable = false;
            GetButton((int)BUTTON.CancelButton).interactable = false;
            SetAnimation(false);
            OnClosePanel();
            isOpen = false;
        }
    }

    public void SetSlot(CharacterSlot targetSlot)
    {
        selectSlot = targetSlot;
    }

    public void SetAnimation(bool isOpen)
    {
        if (animator != null)
        {
            animator.SetBool("isOpen", isOpen);
        }
    }

    #region Event Function
    public void OnClickCreateButton()
    {
        Managers.DataManager.PlayerData.CharacterDatas[selectSlot.slotIndex] = new CharacterData(selectClass);
        Managers.DataManager.SavePlayerData();

        ClosePanel();
    }
    public void OnClickCancelButton()
    {
        ClosePanel();
    }
    public void OnClickCharacterButton(CHARACTER_CLASS characterClass)
    {
        selectClass = characterClass;
        GetButton((int)BUTTON.CreateButton).interactable = true;
    }
    #endregion
}
