using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISelectCharacterScene : UIBaseScene
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
        OptionButton
    }
    public enum TEXT
    {
        // !! Slot text must come before the any other text. (For Initializing)
        SlotText1,
        SlotText2,
        SlotText3,

        // Texts
    }

    private CharacterData[] characterDatas;
    private CharacterSlot[] characterSlots = new CharacterSlot[Constants.MAX_CHARACTER_SLOT_NUMBER];
    private CharacterSlot selectSlot;

    private CreateCharacterPanel createCharacterPanel;

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        Refresh();
    }

    public override void Initialize()
    {
        if (isInitialized == true)
        {
            Debug.Log($"{this}: Already Initialized.");
            return;
        }
        isInitialized = true;

        characterDatas = Managers.DataManager.PlayerData?.CharacterDatas;

        BindButton(typeof(BUTTON));
        BindText(typeof(TEXT));

        selectSlot = null;

        GetButton((int)BUTTON.StartGameButton).onClick.AddListener(() => { OnClickStartGameButton(selectSlot.slotIndex); });
        GetButton((int)BUTTON.CharacterRemoveButton).onClick.AddListener(() => { OnClickRemoveCharacter(selectSlot.slotIndex); });
        GetButton((int)BUTTON.QuitButton).onClick.AddListener(OnClickQuitGameButton);
        GetButton((int)BUTTON.OptionButton).onClick.AddListener(OnClickOptionButton);

        for (int i = 0; i < characterSlots.Length; ++i)
        {
            characterSlots[i] = new CharacterSlot
            {
                slotIndex = i,
                selectionCharacter = null,
                characterPoint = Constants.SELECTION_CHARACTER_POINT[i],
                slotButton = GetButton(i),
                slotText = GetText(i)
            };
        }

        // Sub Panel Initialize
        createCharacterPanel = GetComponentInChildren<CreateCharacterPanel>(true);
        createCharacterPanel.Initialize(selectSlot);
        createCharacterPanel.OnOpenPanel += () =>
        {
            for (int i = 0; i < characterSlots.Length; ++i)
            {
                characterSlots[i].slotButton.interactable = false;
            }
        };
        createCharacterPanel.OnClosePanel += () =>
        {
            Refresh();
        };
    }

    public void Refresh()
    {
        selectSlot = null;

        GetButton((int)BUTTON.StartGameButton).interactable = false;
        GetButton((int)BUTTON.CharacterRemoveButton).interactable = false;

        for (int i=0; i<characterSlots.Length; ++i)
        {
            characterSlots[i].slotButton.onClick.RemoveAllListeners();

            int index = i;
            // Exist CharacterData
            if (characterDatas[i] != null)
            {
                if (characterSlots[i].selectionCharacter == null)
                {
                    CreateCharacterObject(i, characterDatas[i].CharacterClass, characterSlots[i].characterPoint);
                }
                characterSlots[i].slotText.text = characterDatas[i].CharacterClass + "\n" + "Lv. " + characterDatas[i].Level;
                characterSlots[i].slotButton.onClick.AddListener(() => { OnClickCharacterSlot(index); });
            }
            // Don't Exist CharacterData
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
        characterSlots[slotIndex].selectionCharacter = Managers.ResourceManager.InstantiatePrefabSync("Prefab_" + className + "_Slot").GetComponent<SelectionCharacter>();
        characterSlots[slotIndex].selectionCharacter.transform.position = position;
    }

    public void ReleaseSelect(int slotIndex)
    {
        if (selectSlot != null && selectSlot != characterSlots[slotIndex])
        {
            selectSlot.selectionCharacter?.ReleaseCharacter();
        }
    }

    #region Event Function
    // Selection Scene Panel
    public void OnClickCharacterSlot(int slotIndex)
    {
        createCharacterPanel.ClosePanel();
        ReleaseSelect(slotIndex);

        selectSlot = characterSlots[slotIndex];
        selectSlot.selectionCharacter.SelectCharacter();

        GetButton((int)BUTTON.StartGameButton).interactable = true;
        GetButton((int)BUTTON.CharacterRemoveButton).interactable = true;
    }
    public void OnClickCreateCharacter(int slotIndex)
    {
        ReleaseSelect(slotIndex);

        selectSlot = characterSlots[slotIndex];
        createCharacterPanel.SetSlot(selectSlot);
        createCharacterPanel.OpenPanel();

        GetButton((int)BUTTON.StartGameButton).interactable = false;
        GetButton((int)BUTTON.CharacterRemoveButton).interactable = false;
    }
    public void OnClickRemoveCharacter(int slotIndex)
    {
        Destroy(characterSlots[slotIndex].selectionCharacter.gameObject);
        characterDatas[slotIndex] = null;
        Managers.DataManager.SavePlayerData();

        Refresh();
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
        Managers.UIManager.UICommonScene.TogglePopup(Managers.UIManager.UICommonScene.OptionPopup);
    }
    #endregion
}