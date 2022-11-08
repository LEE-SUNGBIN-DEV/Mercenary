using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionScenePanel : UIPanel
{
    public class CharacterSlot
    {
        string className;
        int? level;
        bool isSelect;

        public CharacterSlot()
        {
            className = null;
            level = null;
            isSelect = false;
        }

        public CharacterSlot(CharacterData characterData)
        {
            className = characterData.CharacterClass;
            level = characterData.Level;
            isSelect = false;
        }
    }
    private CharacterData[] characterDatas;
    private CharacterSlot[] characterSlot = new CharacterSlot[GameConstants.MAX_CHARACTER_SLOT_NUMBER];

    public void Initialize()
    {
        characterDatas = Managers.DataManager.PlayerData?.CharacterDatas;
        characterSlot = new CharacterSlot[characterDatas.Length];

        SetCharacterSlot();
    }

    public void SetCharacterSlot()
    {
        for(int i=0; i<characterSlot.Length; ++i)
        {
            if (characterDatas[i] != null)
            {
                characterSlot[i] = new CharacterSlot(characterDatas[i]);
            }
            else
            {
                characterSlot[i] = new CharacterSlot();
            }
        }
    }

    public void CreateCharacter()
    {

    }
    public void RemoveCharacter()
    {

    }
    public void SelectSlot()
    {
    }

    public void StartGame()
    {
        //CurrentCharacter.CharacterData.MaxExperience = Managers.DataManager.LevelDataDictionary[CurrentCharacter.CharacterData.Level];
    }
}
