using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CharacterData
{
    #region Event
    public event UnityAction<CharacterData> OnPlayerDataChanged;
    public event UnityAction<CharacterData> OnMainQuestProcedureChanged;
    public event UnityAction<CharacterData> OnSavePlayerData;
    public event UnityAction<CharacterData> OnLoadPlayerData;
    #endregion

    [SerializeField] private string characterClass;
    [SerializeField] private Vector3 characterLocation;

    // Level
    [SerializeField] private int level;
    [SerializeField] private float currentExperience;
    [SerializeField] private float maxExperience;

    // Stats
    [SerializeField] private int statPoint;
    [SerializeField] private int strength;
    [SerializeField] private int vitality;
    [SerializeField] private int dexterity;
    [SerializeField] private int luck;

    // Inventory
    [SerializeField] private int money;
    [SerializeField] private string[] inventoryItemNames;
    [SerializeField] private int[] inventoryItemCounts;

    // Quick Slot
    [SerializeField] private string[] quickSlotItemNames;
    [SerializeField] private int[] quickSlotItemCounts;

    // Equipment Slot
    [SerializeField] private string[] equipmentSlotItemNames;
    [SerializeField] private int[] equipmentSlotItemCounts;

    // Main Quest
    [SerializeField] private uint mainQuestProcedure;

    // Quest Save List
    [SerializeField] private List<QuestSaveData> questSaveList = new List<QuestSaveData>();

    public CharacterData(CHARACTER_CLASS selectedClass)
    {
        Initialize();
        characterClass = System.Enum.GetName(typeof(CHARACTER_CLASS), selectedClass);
    }

    public void Initialize()
    {
        QuestManager.onCompleteQuest -= UpdateMainQuestProcedure;
        QuestManager.onCompleteQuest += UpdateMainQuestProcedure;

        Quest.onReward -= GetQuestReward;
        Quest.onReward += GetQuestReward;

        Enemy.onDie -= GetExperience;
        Enemy.onDie += GetExperience;

        characterClass = null;
        characterLocation = Vector3.zero;

        // Level
        level = Constants.CHARACTER_DATA_DEFALUT_LEVEL;
        currentExperience = Constants.CHARACTER_DATA_DEFALUT_EXPERIENCE;
        maxExperience = Managers.DataManager.LevelDataDictionary[level];

        // Stats
        statPoint = Constants.CHARACTER_DATA_DEFALUT_STATPOINT;
        strength = Constants.CHARACTER_DATA_DEFALUT_STRENGTH;
        vitality = Constants.CHARACTER_DATA_DEFALUT_VITALITY;
        dexterity = Constants.CHARACTER_DATA_DEFALUT_DEXTERITY;
        luck = Constants.CHARACTER_DATA_DEFALUT_LUCK;

        InventoryItemNames = new string[Constants.MAX_INVENTORY_SLOT_NUMBER];
        InventoryItemCounts = new int[Constants.MAX_INVENTORY_SLOT_NUMBER];

        QuickSlotItemNames = new string[Constants.MAX_QUICK_SLOT_NUMBER];
        QuickSlotItemCounts = new int[Constants.MAX_QUICK_SLOT_NUMBER];

        EquipmentSlotItemNames = new string[Constants.MAX_EQUIPMENT_SLOT_NUMBER];
        EquipmentSlotItemCounts = new int[Constants.MAX_EQUIPMENT_SLOT_NUMBER];

        for (int i = 0; i < Constants.MAX_INVENTORY_SLOT_NUMBER; ++i)
        {
            InventoryItemNames[i] = null;
            InventoryItemCounts[i] = 0;
        }

        for (int i = 0; i < Constants.MAX_QUICK_SLOT_NUMBER; ++i)
        {
            QuickSlotItemNames[i] = null;
            QuickSlotItemCounts[i] = 0;
        }

        for (int i = 0; i < Constants.MAX_EQUIPMENT_SLOT_NUMBER; ++i)
        {
            EquipmentSlotItemNames[i] = null;
            EquipmentSlotItemCounts[i] = 0;
        }

        mainQuestProcedure = 0;
    }

    public void RefreshData()
    {
        CharacterClass = characterClass;
        Level = level;
        CurrentExperience = currentExperience;
        MaxExperience = maxExperience;
        StatPoint = statPoint;
        Strength = strength;
        Vitality = vitality;
        Dexterity = dexterity;
        Luck = luck;
        MainQuestProcedure = mainQuestProcedure;
        Money = money;
        InventoryItemNames = inventoryItemNames;
        InventoryItemCounts = inventoryItemCounts;
        QuickSlotItemNames = quickSlotItemNames;
        QuickSlotItemCounts = quickSlotItemCounts;
        EquipmentSlotItemNames = equipmentSlotItemNames;
        EquipmentSlotItemCounts = equipmentSlotItemCounts;
        QuestSaveList = questSaveList;
    }

    public void LevelUp()
    {
        currentExperience -= MaxExperience;
        MaxExperience = Managers.DataManager.LevelDataDictionary[Level];
        ++Level;
        StatPoint += 5;
    }

    public void GetExperience(Enemy monster)
    {
        CurrentExperience += monster.ExperienceAmount;
    }
    public void GetQuestReward(Quest quest)
    {
        Money += quest.RewardMoney;
        CurrentExperience += quest.RewardExperience;
    }

    public void UpdateMainQuestProcedure(Quest quest)
    {
        if (quest.questCategory == QUEST_CATEGORY.MAIN)
        {
            MainQuestProcedure = quest.QuestID;
        }
    }

    // Save & Load
    public void SaveCharacterData(CharacterData characterData)
    {
        characterData.characterClass = characterClass;
        characterData.level = level;
        characterData.currentExperience = currentExperience;
        characterData.maxExperience = maxExperience;
        characterData.statPoint = statPoint;
        characterData.strength = strength;
        characterData.vitality = vitality;
        characterData.dexterity = dexterity;
        characterData.luck = luck;
        characterData.mainQuestProcedure = mainQuestProcedure;
        characterData.money = money;
        characterData.inventoryItemNames = new string[Constants.MAX_INVENTORY_SLOT_NUMBER];
        characterData.inventoryItemCounts = new int[Constants.MAX_INVENTORY_SLOT_NUMBER];
        characterData.quickSlotItemNames = new string[Constants.MAX_QUICK_SLOT_NUMBER];
        characterData.quickSlotItemCounts = new int[Constants.MAX_QUICK_SLOT_NUMBER];
        characterData.equipmentSlotItemNames = new string[Constants.MAX_EQUIPMENT_SLOT_NUMBER];
        characterData.equipmentSlotItemCounts = new int[Constants.MAX_EQUIPMENT_SLOT_NUMBER];
        characterData.questSaveList = new List<QuestSaveData>();

        OnSavePlayerData(characterData);
    }

    public void LoadCharacterData(CharacterData characterData)
    {
        CharacterClass = characterData.characterClass;
        Level = characterData.level;
        CurrentExperience = characterData.currentExperience;
        MaxExperience = characterData.maxExperience;
        StatPoint = characterData.statPoint;
        Strength = characterData.strength;
        Vitality = characterData.vitality;
        Dexterity = characterData.dexterity;
        Luck = characterData.luck;
        MainQuestProcedure = characterData.mainQuestProcedure;
        Money = characterData.money;

        OnLoadPlayerData(characterData);
    }

    #region Property
    public string CharacterClass
    {
        get { return characterClass; }
        set
        {
            characterClass = value;
            OnPlayerDataChanged?.Invoke(this);
        }
    }
    
    public int Level
    {
        get { return level; }
        set
        {
            level = value;
            if (level < 1)
            {
                level = 1;
            }
            OnPlayerDataChanged?.Invoke(this);
        }
    }

    public float CurrentExperience
    {
        get { return currentExperience; }
        set
        {
            currentExperience = value;
            if (currentExperience < 0)
            {
                currentExperience = 0;
            }

            while(currentExperience >= MaxExperience)
            {
                LevelUp();
            }

            OnPlayerDataChanged?.Invoke(this);
        }
    }

    public float MaxExperience
    {
        get { return maxExperience; }
        set
        {
            maxExperience = value;
            if (maxExperience <= 0)
            {
                maxExperience = 1;
            }
            OnPlayerDataChanged?.Invoke(this);
        }
    }

    public int StatPoint
    {
        get { return statPoint; }
        set
        {
            statPoint = value;
            if (statPoint < 0)
            {
                statPoint = 0;
            }
            OnPlayerDataChanged?.Invoke(this);
        }
    }
    public int Strength
    {
        get { return strength; }
        set
        {
            strength = value;
            if (strength < 0)
            {
                strength = 0;
            }
            OnPlayerDataChanged?.Invoke(this);
        }
    }

    public int Vitality
    {
        get { return vitality; }
        set
        {
            vitality = value;
            if (vitality < 0)
            {
                vitality = 0;
            }
            OnPlayerDataChanged?.Invoke(this);
        }
    }

    public int Dexterity
    {
        get { return dexterity; }
        set
        {
            dexterity = value;

            if (dexterity < 0)
            {
                dexterity = 0;
            }
            OnPlayerDataChanged?.Invoke(this);
        }
    }
    public int Luck
    {
        get { return luck; }
        set
        {
            luck = value;
            if (luck < 0)
            {
                luck = 0;
            }
            OnPlayerDataChanged?.Invoke(this);
        }
    }
    public uint MainQuestProcedure
    {
        get { return mainQuestProcedure; }
        set
        {
            mainQuestProcedure = value;
            OnMainQuestProcedureChanged?.Invoke(this);
        }
    }
    public int Money
    {
        get { return money; }
        set
        {
            money = value;
            if(money < 0)
            {
                money = 0;
            }
            OnPlayerDataChanged?.Invoke(this);
        }
    }

    public string[] InventoryItemNames
    {
        get { return inventoryItemNames; }
        set { inventoryItemNames = value; }
    }
    public int[] InventoryItemCounts
    {
        get { return inventoryItemCounts; }
        set { inventoryItemCounts = value; }
    }

    public string[] QuickSlotItemNames
    {
        get { return quickSlotItemNames; }
        set { quickSlotItemNames = value; }
    }
    public int[] QuickSlotItemCounts
    {
        get { return quickSlotItemCounts; }
        set { quickSlotItemCounts = value; }
    }

    public string[] EquipmentSlotItemNames
    {
        get { return equipmentSlotItemNames; }
        set { equipmentSlotItemNames = value; }
    }
    public int[] EquipmentSlotItemCounts
    {
        get { return equipmentSlotItemCounts; }
        set { equipmentSlotItemCounts = value; }
    }
    public List<QuestSaveData> QuestSaveList
    {
        get { return questSaveList; }
        set { questSaveList = value; }
    }
    #endregion
}
