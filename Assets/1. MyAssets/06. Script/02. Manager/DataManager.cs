using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;
using Newtonsoft.Json;

// =================== DATA MANAGER CLASS (Singleton) =================================
// 데이터 경로, 세이브, 로드를 관리해주는 클래스
// ====================================================================================

public class DataManager : Singleton<DataManager>
{
    #region Event
    public static event UnityAction<PlayerSaveData> onLoadPlayerData;
    public static event UnityAction<PlayerSaveData> onSavePlayerData;
    #endregion
    private Dictionary<int, float> levelDataDictionary;
    private LevelTable levelData;

    private string playerDataPath;
    private string levelDataPath;

    public override void Initialize()
    {
        playerDataPath = Application.dataPath + "/PlayerSaveData.json";
        levelDataPath = Application.dataPath + "/LevelTable.json";

        LevelDataDictionary = new Dictionary<int, float>();
        LoadLevelData();
        for (int i = 0; i < levelData.MaxLevel; ++i)
        {
            LevelDataDictionary.Add(LevelData.Level[i], LevelData.MaxExperience[i]);
        }

        GameManager.onClickLoadGameButton -= LoadPlayerData;
        GameManager.onClickLoadGameButton += LoadPlayerData;

        GameManager.onSaveGame -= SavePlayerData;
        GameManager.onSaveGame += SavePlayerData;

        PlayerData.onLevelUp -= RefreshExperience;
        PlayerData.onLevelUp += RefreshExperience;
    }

    public void RefreshExperience(PlayerData playerData)
    {
        playerData.MaxExperience = LevelDataDictionary[playerData.Level];
    }

    #region Save & Load Function
    public bool FileCheck()
    {
        FileInfo loadFile = new FileInfo(playerDataPath);

        return loadFile.Exists;
    }

    // Load Level Table
    public void LoadLevelData()
    {
        string jsonLevelData = File.ReadAllText(levelDataPath);
        LevelData = JsonUtility.FromJson<LevelTable>(jsonLevelData);
    }

    // Save & Load Player Data
    public void LoadPlayerData()
    {
        string jsonPlayerData = File.ReadAllText(playerDataPath);
        PlayerSaveData playerSaveData = JsonConvert.DeserializeObject<PlayerSaveData>(jsonPlayerData);
        onLoadPlayerData(playerSaveData);
    }

    public void SavePlayerData()
    {
        PlayerSaveData playerSaveData = new PlayerSaveData();
        onSavePlayerData(playerSaveData);

        string jsonPlayerData = JsonConvert.SerializeObject(playerSaveData, Formatting.Indented);
        File.WriteAllText(playerDataPath, jsonPlayerData);
    }
    #endregion

    #region Property
    public LevelTable LevelData
    {
        get { return levelData; }
        set { levelData = value; }
    }
    public Dictionary<int, float> LevelDataDictionary
    {
        get { return levelDataDictionary; }
        private set { levelDataDictionary = value; }
    }
    #endregion
}
