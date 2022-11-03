using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : Singleton<Managers>
{
    private GameManager gameManager = new GameManager();
    private GameSceneManager gameSceneManager = new GameSceneManager();
    private ResourceManager resourceManager = new ResourceManager();
    private UIManager uiManager = new UIManager();
    private AudioManager audioManager = new AudioManager();
    private DataManager dataManager = new DataManager();
    private NPCManager npcManager = new NPCManager();
    private DialogueManager dialogueManager = new DialogueManager();
    private QuestManager questManager = new QuestManager();
    private ItemManager itemManager = new ItemManager();
    private ObjectPoolManager objectPoolManager = new ObjectPoolManager();

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        //UIManager.Update();
    }

    public override void Initialize()
    {
        gameManager.Initialize();
        gameSceneManager.Initialize();
        resourceManager.Initialize();
        /*
        uiManager.Initialize();
        audioManager.Initialize();
        dataManager.Initialize();
        npcManager.Initialize();
        dialogueManager.Initialize();
        questManager.Initialize();
        itemManager.Initialize();
        objectPoolManager.Initialize(gameObject);
        */
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
    public static ResourceManager ResourceManager
    {
        get { return Instance?.resourceManager; }
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
