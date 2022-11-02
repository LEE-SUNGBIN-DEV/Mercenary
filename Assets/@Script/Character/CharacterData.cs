using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CharacterData : MonoBehaviour
{
    #region Event
    public static event UnityAction<CharacterData> OnPlayerDataChanged;
    public static event UnityAction<CharacterData> onMainQuestProcedureChanged;

    public static event UnityAction<PlayerSaveData> onSavePlayerData;
    public static event UnityAction<PlayerSaveData> onLoadPlayerData;
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

    // Main Quest
    [SerializeField] private uint mainQuestProcedure;

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

    // Quest Save List
    [SerializeField] private List<QuestSaveData> questSaveList;

    private void Awake()
    {
        DataManager.onLoadPlayerData -= LoadPlayerData;
        DataManager.onLoadPlayerData += LoadPlayerData;
        DataManager.onSavePlayerData -= SavePlayerData;
        DataManager.onSavePlayerData += SavePlayerData;

        QuestManager.onCompleteQuest -= UpdateMainQuestProcedure;
        QuestManager.onCompleteQuest += UpdateMainQuestProcedure;

        Quest.onReward -= GetQuestReward;
        Quest.onReward += GetQuestReward;

        Enemy.onDie -= GetExperience;
        Enemy.onDie += GetExperience;

        InventoryItemNames = new string[GameConstants.CHARACTER_INVENTORY_SLOT_COUNT];
        InventoryItemCounts = new int[GameConstants.CHARACTER_INVENTORY_SLOT_COUNT];

        QuickSlotItemNames = new string[GameConstants.CHARACTER_QUICK_SLOT_COUNT];
        QuickSlotItemCounts = new int[GameConstants.CHARACTER_QUICK_SLOT_COUNT];

        EquipmentSlotItemNames = new string[GameConstants.CHARACTER_EQUIPMENT_SLOT_COUNT];
        EquipmentSlotItemCounts = new int[GameConstants.CHARACTER_EQUIPMENT_SLOT_COUNT];

        questSaveList = new List<QuestSaveData>();

        for (int i = 0; i < GameConstants.CHARACTER_INVENTORY_SLOT_COUNT; ++i)
        {
            InventoryItemNames[i] = null;
            InventoryItemCounts[i] = 0;
        }

        for (int i = 0; i < GameConstants.CHARACTER_QUICK_SLOT_COUNT; ++i)
        {
            QuickSlotItemNames[i] = null;
            QuickSlotItemCounts[i] = 0;
        }

        for (int i = 0; i < GameConstants.CHARACTER_EQUIPMENT_SLOT_COUNT; ++i)
        {
            EquipmentSlotItemNames[i] = null;
            EquipmentSlotItemCounts[i] = 0;
        }

        RefreshData();
    }

    public void RefreshData()
    {
        PlayerClass = characterClass;
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
    private void OnDestroy()
    {
        DataManager.onLoadPlayerData -= LoadPlayerData;
        DataManager.onSavePlayerData -= SavePlayerData;
        QuestManager.onCompleteQuest -= UpdateMainQuestProcedure;
        Quest.onReward -= GetQuestReward;
        Enemy.onDie -= GetExperience;
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
    public void SavePlayerData(PlayerSaveData playerSaveData)
    {
        playerSaveData.playerClass = characterClass;
        playerSaveData.level = level;
        playerSaveData.currentExperience = currentExperience;
        playerSaveData.maxExperience = maxExperience;
        playerSaveData.statPoint = statPoint;
        playerSaveData.strength = strength;
        playerSaveData.vitality = vitality;
        playerSaveData.dexterity = dexterity;
        playerSaveData.luck = luck;
        playerSaveData.mainQuestProcedure = mainQuestProcedure;
        playerSaveData.money = money;
        playerSaveData.inventoryItemNames = new string[GameConstants.CHARACTER_INVENTORY_SLOT_COUNT];
        playerSaveData.inventoryItemCounts = new int[GameConstants.CHARACTER_INVENTORY_SLOT_COUNT];
        playerSaveData.quickSlotItemNames = new string[GameConstants.CHARACTER_QUICK_SLOT_COUNT];
        playerSaveData.quickSlotItemCounts = new int[GameConstants.CHARACTER_QUICK_SLOT_COUNT];
        playerSaveData.equipmentSlotItemNames = new string[GameConstants.CHARACTER_EQUIPMENT_SLOT_COUNT];
        playerSaveData.equipmentSlotItemCounts = new int[GameConstants.CHARACTER_EQUIPMENT_SLOT_COUNT];
        playerSaveData.questSaveList = new List<QuestSaveData>();

        onSavePlayerData(playerSaveData);
    }

    public void LoadPlayerData(PlayerSaveData playerSaveData)
    {
        PlayerClass = playerSaveData.playerClass;
        Level = playerSaveData.level;
        CurrentExperience = playerSaveData.currentExperience;
        MaxExperience = playerSaveData.maxExperience;
        StatPoint = playerSaveData.statPoint;
        Strength = playerSaveData.strength;
        Vitality = playerSaveData.vitality;
        Dexterity = playerSaveData.dexterity;
        Luck = playerSaveData.luck;
        MainQuestProcedure = playerSaveData.mainQuestProcedure;
        Money = playerSaveData.money;

        onLoadPlayerData(playerSaveData);
    }

    #region Property
    public string PlayerClass
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
            onMainQuestProcedureChanged?.Invoke(this);
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
