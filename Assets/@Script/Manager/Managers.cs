using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : Singleton<Managers>
{
    private GameObject rootObject;
    private GameManager gameManager;
    private GameSceneManager gameSceneManager;
    private UIManager uiManager;
    private AudioManager audioManager;
    private DataManager dataManager;
    private NPCManager npcManager;
    private DialogueManager dialogueManager;
    private QuestManager questManager;
    private ItemManager itemManager;
    private ObjectPoolManager objectPoolManager;

    private void Awake()
    {
        rootObject = gameObject;
        gameManager = new GameManager();
        gameSceneManager = new GameSceneManager();
        uiManager = new UIManager();
        audioManager = new AudioManager();
        dataManager = new DataManager();
        npcManager = new NPCManager();
        dialogueManager = new DialogueManager();
        questManager = new QuestManager();
        itemManager = new ItemManager();
        objectPoolManager = new ObjectPoolManager();

        Initialize();
    }

    private void Update()
    {
        UIManager.Update();
    }

    public override void Initialize()
    {
        gameManager.Initialize();
        gameSceneManager.Initialize();
        uiManager.Initialize();
        audioManager.Initialize();
        dataManager.Initialize();
        npcManager.Initialize();
        dialogueManager.Initialize();
        questManager.Initialize();
        itemManager.Initialize();
        objectPoolManager.Initialize(gameObject);
    }

    #region Property
    public static GameManager GameManager
    {
        get => Instance?.gameManager;
    }
    public static GameSceneManager GameSceneManager
    {
        get => Instance?.gameSceneManager;
    }
    public static UIManager UIManager
    {
        get => Instance?.uiManager;
    }
    public static AudioManager AudioManager
    {
        get => Instance?.audioManager;
    }
    public static DataManager DataManager
    {
        get => Instance?.dataManager;
    }
    public static NPCManager NPCManager
    {
        get => Instance?.npcManager;
    }
    public static DialogueManager DialogueManager
    {
        get => Instance?.dialogueManager;
    }
    public static QuestManager QuestManager
    {
        get => Instance?.questManager;
    }
    public static ItemManager ItemManager
    {
        get => Instance?.itemManager;
    }
    public static ObjectPoolManager ObjectPoolManager
    {
        get => Instance?.objectPoolManager;
    }
    #endregion
}
