using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectionScenePanel : UIPanel
{
    public enum BUTTON
    {
        // !! Slot button must come before the any other button. (For Initializing)
        SlotButton1,
        SlotButton2,
        SlotButton3,

        // Buttons
        StartGameButton,
        CharacterRemoveButton,
        QuitButton,
        OptionButton,

        // Sub Panel Buttons
        CreateButton,
        CancelButton,

        LancerButton
    }
    public enum TEXT
    {
        // !! Slot text must come before the any other text. (For Initializing)
        SlotText1,
        SlotText2,
        SlotText3,

        // Texts
    }

    public enum SUB_PANEL
    {
        CreateCharacterSubPanel
    }

    private CharacterData[] characterDatas;
    private CharacterSlot[] characterSlots = new CharacterSlot[GameConstants.MAX_CHARACTER_SLOT_NUMBER];
    private CharacterSlot selectSlot = null;

    private SubPanel createCharacterSubPanel;
    private CHARACTER_CLASS selectClass;

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        RefreshCharacterSlot();
    }

    public void Initialize()
    {
        BindButton(typeof(BUTTON));
        BindText(typeof(TEXT));
        BindObject<SubPanel>(typeof(SUB_PANEL));

        selectSlot = null;
        selectClass = CHARACTER_CLASS.Null;
        createCharacterSubPanel = GetObject<SubPanel>((int)SUB_PANEL.CreateCharacterSubPanel);

        // Selection Scene Panel
        GetButton((int)BUTTON.StartGameButton).onClick.AddListener(() => { OnClickStartGameButton(selectSlot.slotIndex); });
        GetButton((int)BUTTON.CharacterRemoveButton).onClick.AddListener(() => { OnClickRemoveCharacter(selectSlot.slotIndex); });
        GetButton((int)BUTTON.QuitButton).onClick.AddListener(OnClickQuitGameButton);
        GetButton((int)BUTTON.OptionButton).onClick.AddListener(OnClickOptionButton);

        // Create Character Sub Panel
        GetButton((int)BUTTON.LancerButton).onClick.AddListener(() => { OnClickCharacterButton(CHARACTER_CLASS.Lancer); });
        GetButton((int)BUTTON.CreateButton).onClick.AddListener(() => { OnClickCreateButton(selectSlot.slotIndex, selectClass); });
        GetButton((int)BUTTON.CancelButton).onClick.AddListener(OnClickCancelButton);

        for (int i = 0; i < characterSlots.Length; ++i)
        {
            characterSlots[i] = new CharacterSlot();
            characterSlots[i].slotIndex = i;
            characterSlots[i].selectionCharacter = null;
            characterSlots[i].characterPoint = GameConstants.SELECTION_CHARACTER_POINT[i];
            characterSlots[i].slotButton = GetButton(i);
            characterSlots[i].slotText = GetText(i);
        }
    }

    public void RefreshCharacterSlot()
    {
        characterDatas = Managers.DataManager.PlayerData?.CharacterDatas;
        selectSlot = null;

        GetButton((int)BUTTON.StartGameButton).interactable = false;
        GetButton((int)BUTTON.CharacterRemoveButton).interactable = false;

        GetButton((int)BUTTON.CreateButton).interactable = false;
        GetButton((int)BUTTON.CancelButton).interactable = false;

        for (int i=0; i<characterSlots.Length; ++i)
        {
            characterSlots[i].slotButton.onClick.RemoveAllListeners();

            int index = i;
            if (characterDatas[i] != null)
            {
                if (characterSlots[i].selectionCharacter == null)
                {
                    CreateCharacterObject(i, characterDatas[i].CharacterClass, characterSlots[i].characterPoint);
                }
                characterSlots[i].slotText.text = characterDatas[i].CharacterClass + "\n" + "Lv. " + characterDatas[i].Level;
                characterSlots[i].slotButton.onClick.AddListener(() => { OnClickCharacterSlot(index); });
            }
            else
            {
                if (characterSlots[i].selectionCharacter != null)
                {
                    Destroy(characterSlots[i].selectionCharacter.gameObject);
                }
                characterSlots[i].slotText.text = "캐릭터 생성";
                characterSlots[i].slotButton.onClick.AddListener(() => { OnClickCreateCharacter(index); });
            }

            characterSlots[i].slotButton.interactable = true;
        }
    }

    public void CreateCharacterObject(int slotIndex, string className, Vector3 position)
    {
        Managers.ResourceManager.InstantiatePrefab("Prefab_" + className + "_Slot", null,
            (GameObject targetObject) =>
            {
                characterSlots[slotIndex].selectionCharacter = targetObject.GetComponent<SelectionCharacter>();
                targetObject.transform.position = position;
            });
    }

    public void ReleaseSelect(int slotIndex)
    {
        if (selectSlot != null && selectSlot != characterSlots[slotIndex])
        {
            selectSlot.selectionCharacter.SetAnimation(false);
            selectSlot.selectionCharacter.SetMaterial(MATERIAL.Material_Lancer_Default);
        }
    }

    #region Event Function
    // Selection Scene Panel
    public void OnClickCharacterSlot(int slotIndex)
    {
        ReleaseSelect(slotIndex);

        selectSlot = characterSlots[slotIndex];
        selectSlot.selectionCharacter.SetAnimation(true);
        selectSlot.selectionCharacter.SetMaterial(MATERIAL.Material_Lancer_Outline);

        GetButton((int)BUTTON.StartGameButton).interactable = true;
        GetButton((int)BUTTON.CharacterRemoveButton).interactable = true;
    }
    public void OnClickCreateCharacter(int slotIndex)
    {
        ReleaseSelect(slotIndex);

        selectSlot = characterSlots[slotIndex];

        for (int i=0; i< characterSlots.Length; ++i)
        {
            characterSlots[i].slotButton.interactable = false;
        }

        createCharacterSubPanel.SetAnimation(true);

        GetButton((int)BUTTON.CreateButton).interactable = false;
        GetButton((int)BUTTON.CancelButton).interactable = true;
    }
    public void OnClickRemoveCharacter(int slotIndex)
    {
        Destroy(characterSlots[slotIndex].selectionCharacter.gameObject);

        characterDatas[slotIndex] = null;

        RefreshCharacterSlot();
    }
    public void OnClickStartGameButton(int slotIndex)
    {
        Managers.DataManager.SavePlayerData();
        Managers.DataManager.CurrentCharacterData = characterDatas[slotIndex];

        Managers.GameSceneManager.LoadSceneAsync(SCENE_LIST.Forestia);
    }
    public void OnClickQuitGameButton()
    {
        Managers.GameManager.SaveAndQuit();
    }
    public void OnClickOptionButton()
    {
        Managers.UIManager.TogglePopup(POPUP.OptionPopup);
    }

    // Create Character SubPanel
    public void OnClickCreateButton(int slotIndex, CHARACTER_CLASS selectClass)
    {
        characterDatas[slotIndex] = new CharacterData(selectClass);
        Managers.DataManager.SavePlayerData();

        createCharacterSubPanel.SetAnimation(false);
        RefreshCharacterSlot();
    }
    public void OnClickCancelButton()
    {
        selectSlot = null;

        GetButton((int)BUTTON.CreateButton).interactable = false;
        GetButton((int)BUTTON.CancelButton).interactable = false;

        for (int i = 0; i < characterSlots.Length; ++i)
        {
            characterSlots[i].slotButton.interactable = true;
        }
        createCharacterSubPanel.SetAnimation(false);
    }
    public void OnClickCharacterButton(CHARACTER_CLASS characterClass)
    {
        selectClass = characterClass;
        GetButton((int)BUTTON.CreateButton).interactable = true;
        GetButton((int)BUTTON.CancelButton).interactable = true;
    }
    #endregion
}