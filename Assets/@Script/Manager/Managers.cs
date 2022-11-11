using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Managers : Singleton<Managers>
{
    private bool isInitialized = false;
    private bool isCanvasLoaded = false;

    private GameManager gameManager = new GameManager();
    private GameSceneManager gameSceneManager = new GameSceneManager();
    private ResourceManager resourceManager = new ResourceManager();
    private UIManager uiManager = new UIManager();
    private AudioManager audioManager = new AudioManager();
    [SerializeField] private DataManager dataManager = new DataManager();
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
        if(isInitialized == true)
        {
            Debug.Log("[Managers] Already Initialized.");
            return;
        }

        isInitialized = true;

        // UI Canvas ·Îµå
        GameObject canvas = GameObject.Find("@UI Canvas");
        if (canvas == null)
        {
            ResourceManager.InstantiatePrefab("@UI Canvas", this.transform,
                (GameObject gameObject) =>
                {
                    canvas = gameObject;
                    uiManager.Initialize(canvas);
                    isCanvasLoaded = true;
                });
        }
        else
        {
            uiManager.Initialize(canvas);
            canvas.transform.SetParent(this.transform);
            isCanvasLoaded = true;
        }

        

        /*
        audioManager.Initialize();
        npcManager.Initialize();
        dialogueManager.Initialize();
        questManager.Initialize();
        itemManager.Initialize();
        objectPoolManager.Initialize(gameObject);
        */

        StartCoroutine(GameFunction.WaitAsyncOperation(IsLoaded, () =>
        {
            gameManager.Initialize();
            gameSceneManager.Initialize();
            resourceManager.Initialize();
            dataManager.Initialize();

            Debug.Log("[Managers] Initialization Complete!");
        }));
    }

    public bool IsLoaded()
    {
        if (isCanvasLoaded == false)
            return false;

        return true;
    }
    

    #region Property
    public static GameManager GameManager
    {
        get { return Instance?.gameManager; }
    }
    public static GameSceneManager GameSceneManager
    {
        get { return Instance?.gameSceneManager; }
    }
    public static ResourceManager ResourceManager
    {
        get { return Instance?.resourceManager; }
    }
    public static UIManager UIManager
    {
        get { return Instance?.uiManager; }
    }
    public static AudioManager AudioManager
    {
        get { return Instance?.audioManager; }
    }
    public static DataManager DataManager
    {
        get { return Instance?.dataManager; }
    }
    public static NPCManager NPCManager
    {
        get { return Instance?.npcManager; }
    }
    public static DialogueManager DialogueManager
    {
        get { return Instance?.dialogueManager; }
    }
    public static QuestManager QuestManager
    {
        get { return Instance?.questManager; }
    }
    public static ItemManager ItemManager
    {
        get { return Instance?.itemManager; }
    }
    public static ObjectPoolManager ObjectPoolManager
    {
        get { return Instance?.objectPoolManager; }
    }
    #endregion
}
