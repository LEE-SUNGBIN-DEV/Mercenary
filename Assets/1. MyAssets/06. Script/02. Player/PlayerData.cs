using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    #region Event
    public static event UnityAction<PlayerData> onPlayerClassChanged;
    public static event UnityAction<PlayerData> onLevelChanged;
    public static event UnityAction<PlayerData> onCurrentExperienceChanged;
    public static event UnityAction<PlayerData> onMaxExperienceChanged;
    public static event UnityAction<PlayerData> onStatPointChanged;
    public static event UnityAction<PlayerData> onStrengthChanged;
    public static event UnityAction<PlayerData> onVitalityChanged;
    public static event UnityAction<PlayerData> onDexterityChanged;
    public static event UnityAction<PlayerData> onLuckChanged;
    public static event UnityAction<PlayerData> onMoneyChanged;
    public static event UnityAction<PlayerData> onMainQuestProcedureChanged;
    public static event UnityAction<PlayerData> onLevelUp;

    public static event UnityAction<PlayerSaveData> onSavePlayerData;
    public static event UnityAction<PlayerSaveData> onLoadPlayerData;
    #endregion

    // Class
    [SerializeField] private string playerClass;

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
        GameManager.onCombatSceneLoaded -= InitializePlayerData;
        GameManager.onCombatSceneLoaded += InitializePlayerData;

        DataManager.onLoadPlayerData -= LoadPlayerData;
        DataManager.onLoadPlayerData += LoadPlayerData;
        DataManager.onSavePlayerData -= SavePlayerData;
        DataManager.onSavePlayerData += SavePlayerData;

        QuestManager.onCompleteQuest -= UpdateMainQuestProcedure;
        QuestManager.onCompleteQuest += UpdateMainQuestProcedure;

        Quest.onReward -= GetQuestReward;
        Quest.onReward += GetQuestReward;

        Monster.onDie -= GetExperience;
        Monster.onDie += GetExperience;

        InventoryItemNames = new string[Constant.inventorySlotCount];
        InventoryItemCounts = new int[Constant.inventorySlotCount];

        QuickSlotItemNames = new string[Constant.quickSlotCount];
        QuickSlotItemCounts = new int[Constant.quickSlotCount];

        EquipmentSlotItemNames = new string[Constant.equipmentSlotCount];
        EquipmentSlotItemCounts = new int[Constant.equipmentSlotCount];

        questSaveList = new List<QuestSaveData>();

        for (int i = 0; i < Constant.inventorySlotCount; ++i)
        {
            InventoryItemNames[i] = null;
            InventoryItemCounts[i] = 0;
        }

        for (int i = 0; i < Constant.quickSlotCount; ++i)
        {
            QuickSlotItemNames[i] = null;
            QuickSlotItemCounts[i] = 0;
        }

        for (int i = 0; i < Constant.equipmentSlotCount; ++i)
        {
            EquipmentSlotItemNames[i] = null;
            EquipmentSlotItemCounts[i] = 0;
        }

        InitializePlayerData();
    }

    public void InitializePlayerData()
    {
        PlayerClass = playerClass;
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
        GameManager.onCombatSceneLoaded -= InitializePlayerData;
        DataManager.onLoadPlayerData -= LoadPlayerData;
        DataManager.onSavePlayerData -= SavePlayerData;
        QuestManager.onCompleteQuest -= UpdateMainQuestProcedure;
        Quest.onReward -= GetQuestReward;
        Monster.onDie -= GetExperience;
    }

    public void GetExperience(Monster monster)
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
        playerSaveData.playerClass = playerClass;
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
        playerSaveData.inventoryItemNames = new string[Constant.inventorySlotCount];
        playerSaveData.inventoryItemCounts = new int[Constant.inventorySlotCount];
        playerSaveData.quickSlotItemNames = new string[Constant.quickSlotCount];
        playerSaveData.quickSlotItemCounts = new int[Constant.quickSlotCount];
        playerSaveData.equipmentSlotItemNames = new string[Constant.equipmentSlotCount];
        playerSaveData.equipmentSlotItemCounts = new int[Constant.equipmentSlotCount];
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
        get { return playerClass; }
        set
        {
            playerClass = value;
            onPlayerClassChanged(this);
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
            onLevelChanged(this);
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
                currentExperience -= MaxExperience;
                ++Level;
                StatPoint += 5;

                onLevelUp(this);
            }

            onCurrentExperienceChanged(this);
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
            onMaxExperienceChanged(this);
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
            onStatPointChanged(this);
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
            onStrengthChanged(this);
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
            onVitalityChanged(this);
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
            onDexterityChanged(this);
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
            onLuckChanged(this);
        }
    }
    public uint MainQuestProcedure
    {
        get { return mainQuestProcedure; }
        set
        {
            mainQuestProcedure = value;
            onMainQuestProcedureChanged(this);
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
            onMoneyChanged(this);
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
