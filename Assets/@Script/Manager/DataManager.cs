using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

[System.Serializable]
public class DataManager
{
    private Dictionary<int, float> levelTableDictionary = new Dictionary<int, float>();
    private LevelTable levelTable;

    private string playerDataPath;
    private string levelDataPath;

    [SerializeField] private PlayerData playerData = null;
    private CharacterData currentCharacterData;
    private Character currentCharacter;

    public void Initialize()
    {
        playerDataPath = Application.dataPath + "/PlayerData.json";
        levelDataPath = Application.dataPath + "/LevelTable.json";

        LoadLevelData();
        for (int i = 0; i < levelTable.MaxLevel; ++i)
        {
            LevelDataDictionary.Add(LevelTable.Level[i], LevelTable.MaxExperience[i]);
        }

        LoadPlayerData();
    }

    public bool FileCheck()
    {
        FileInfo loadFile = new FileInfo(playerDataPath);

        return loadFile.Exists;
    }

    // Load Level Table
    public void LoadLevelData()
    {
        string jsonLevelData = File.ReadAllText(levelDataPath);
        levelTable = JsonUtility.FromJson<LevelTable>(jsonLevelData);
    }

    // Save & Load Player Data
    public void LoadPlayerData()
    {
        if (FileCheck())
        {
            string jsonPlayerData = File.ReadAllText(playerDataPath);
            playerData = JsonConvert.DeserializeObject<PlayerData>(jsonPlayerData);
        }
        else
        {
            playerData = new PlayerData(true);
            SavePlayerData();
        }
    }

    public void SavePlayerData()
    {
        string jsonPlayerData = JsonConvert.SerializeObject(playerData, Formatting.Indented);
        File.WriteAllText(playerDataPath, jsonPlayerData);
    }

    #region Property
    public LevelTable LevelTable { get { return levelTable; } }
    public Dictionary<int, float> LevelDataDictionary { get { return levelTableDictionary; } }
    public PlayerData PlayerData { get { return playerData; } }
    public Character CurrentCharacter { get { return currentCharacter; } set { currentCharacter = value; } }
    public CharacterData CurrentCharacterData { get { return currentCharacterData; } set { currentCharacterData = value; } }
    #endregion
}
