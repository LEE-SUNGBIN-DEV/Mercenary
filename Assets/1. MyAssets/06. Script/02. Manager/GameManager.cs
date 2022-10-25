using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum SCENE_LIST
{
    TITLE,
    VILIAGE,
    FOREST,
    TEMPLE
}
public enum CURSOR_MODE
{
    LOCK,
    UNLOCK
}

public class GameManager : Singleton<GameManager>
{
    #region Event
    public static event UnityAction onLoadScene;
    public static event UnityAction onTitleSceneLoaded;
    public static event UnityAction onSceneLoaded;
    public static event UnityAction onCombatSceneLoaded;
    public static event UnityAction onClickNewGameButton;
    public static event UnityAction onClickLoadGameButton;
    public static event UnityAction onSaveGame;
    #endregion
    private CURSOR_MODE cursorMode;
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Player player;
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private SubCamera subCamera;
    private Dictionary<SCENE_LIST, string> sceneDictionary;
    private string currentSceneName;

    public override void Initialize()
    {
        int setWidth = 1920;
        int setHeight = 1080;

        Screen.SetResolution(setWidth, setHeight, true);

        CurrentSceneName = "Title";

        // Cursor
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        SetCursorMode(CURSOR_MODE.UNLOCK);

        // Scene
        sceneDictionary = new Dictionary<SCENE_LIST, string>()
            {
                {SCENE_LIST.TITLE, "Title" },
                {SCENE_LIST.VILIAGE, "Viliage" },
                {SCENE_LIST.FOREST, "Forest" },
                {SCENE_LIST.TEMPLE, "Temple" }
            };

        #region Add Event
        SceneManager.sceneLoaded -= SceneLoaded;
        SceneManager.sceneLoaded += SceneLoaded;

        CampaignPopUp.onClickCampaignButton -= LoadScene;
        CampaignPopUp.onClickCampaignButton += LoadScene;

        PlayerData.onLevelUp += (PlayerData playerData) =>
        {
            Player.LevelUpEffect.SetActive(true);
        };

        ReturnPanel.onReturnViliage += () =>
        {
            LoadScene(SCENE_LIST.VILIAGE);
        };

        MonsterCompeteController.onCompete += (MonsterCompeteController competeController) =>
        {
            SetCharacterTransform(competeController.PlayerCompetePoint);
            SetMainCameraTransform(competeController.PlayerCompetePoint);
            SetSubCameraTransform(competeController.PlayerSubCameraPoint);
            SubCamera.OriginalPosition = competeController.PlayerSubCameraPoint.position;

            PlayerCamera.gameObject.SetActive(false);
            SubCamera.gameObject.SetActive(true);
        };

        #endregion
    }

    private void Start()
    {
        Player.gameObject.SetActive(false);
        PlayerCamera.gameObject.SetActive(false);
        SubCamera.gameObject.SetActive(false);
    }

    #region Game Control
    public void OnClickNewGameButton()
    {
        AudioManager.Instance.PlaySFX("Button Click");

        if (DataManager.Instance.FileCheck())
        {
            ConfirmPanel.onConfirm -= NewGame;
            ConfirmPanel.onConfirm += NewGame;

            onClickNewGameButton();
        }

        else
        {
            NewGame();
        }
    }

    public void OnClickLoadGameButton()
    {
        AudioManager.Instance.PlaySFX("Button Click");

        if (DataManager.Instance.FileCheck())
        {
            onClickLoadGameButton();

            Player.PlayerData.MaxExperience = DataManager.Instance.LevelDataDictionary[Player.PlayerData.Level];
            LoadScene(SCENE_LIST.VILIAGE);
        }

        else
        {
            UIManager.Instance.RequestNotice("플레이 데이터가 없습니다.");
        }
    }

    public void NewGame()
    {
        ConfirmPanel.onConfirm = null;

        Player.PlayerData.MaxExperience = DataManager.Instance.LevelDataDictionary[Player.PlayerData.Level];
        LoadScene(SCENE_LIST.VILIAGE);
    }
    public void SaveGame()
    {
        onSaveGame();
        UIManager.Instance.RequestNotice("저장 완료");
    }
    public void SaveAndQuitGame()
    {
        LoadScene(SCENE_LIST.TITLE);
    }
    public void QuitGame()
    {
        AudioManager.Instance.PlaySFX("Button Click");
        Application.Quit();
    }
    #endregion

    #region Player and Camera Control
    public void SetMainCameraTransform(Transform destinationTransform)
    {
        PlayerCamera.transform.position = destinationTransform.position;
        PlayerCamera.transform.rotation = destinationTransform.rotation;
    }
    public void SetSubCameraTransform(Transform destinationTransform)
    {
        SubCamera.transform.position = destinationTransform.position;
        SubCamera.transform.rotation = destinationTransform.rotation;
    }
    public void SetCharacterTransform(Transform destinationTransform)
    {
        Player.PlayerCharacterController.enabled = false;
        Player.transform.position = destinationTransform.position;
        Player.transform.rotation = destinationTransform.rotation;
        Player.PlayerCharacterController.enabled = true;
    }
    public void SetCharacterPosition(Vector3 destinationPosition)
    {
        Player.PlayerCharacterController.enabled = false;
        Player.transform.position = destinationPosition;
        Player.PlayerCharacterController.enabled = true;
    }
    #endregion

    #region Scene Control
    public void LoadScene(string sceneName)
    {
        onLoadScene();
        if (Player != null)
        {
            Player.gameObject.SetActive(false);
        }
        CurrentSceneName = sceneName;
        LoadingManager.LoadScene(sceneName);
    }
    public void LoadScene(SCENE_LIST requestScene)
    {
        LoadScene(sceneDictionary[requestScene]);
    }
    
    public void SceneLoaded(Scene loadedScene, LoadSceneMode mode)
    {
        onSceneLoaded();

        if (loadedScene.name == "Loading")
        {
            Player.gameObject.SetActive(false);
            PlayerCamera.gameObject.SetActive(false);
            return;
        }

        else if (loadedScene.name == "Title")
        {
            onTitleSceneLoaded();
            SetCursorMode(CURSOR_MODE.UNLOCK);
        }

        else
        {
            Player.gameObject.SetActive(true);
            PlayerCamera.gameObject.SetActive(true);
            Player.InitializeHPAndStamina();
            Player.InitializeAllState();

            Vector3 spawnPosition = GameObject.FindGameObjectWithTag("Player Spawn Point").transform.position;
            SetCharacterPosition(spawnPosition);
            SetMainCameraTransform(Player.transform);
            SetCursorMode(CURSOR_MODE.LOCK);

            onCombatSceneLoaded();
        }

        AudioManager.Instance.PlayBGM(loadedScene.name);
    }
    #endregion

    #region Cursor Control
    public void SetCursorMode(CURSOR_MODE cursorMode)
    {
        switch(cursorMode)
        {
            case CURSOR_MODE.LOCK:
                {
                    this.cursorMode = CURSOR_MODE.LOCK;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    break;
                }
            case CURSOR_MODE.UNLOCK:
                {
                    this.cursorMode = CURSOR_MODE.UNLOCK;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    break;
                }
        }
    }
    public void ToggleCursorMode()
    {
        if(cursorMode == CURSOR_MODE.LOCK)
        {
            SetCursorMode(CURSOR_MODE.UNLOCK);
        }

        else
        {
            SetCursorMode(CURSOR_MODE.LOCK);
        }
    }
    #endregion

    #region Property
    public Player Player
    {
        get { return player; }
        private set { player = value; }
    }
    public PlayerCamera PlayerCamera
    {
        get { return playerCamera; }
        set { playerCamera = value; }
    }
    public SubCamera SubCamera
    {
        get { return subCamera; }
        set { subCamera = value; }
    }
    public string CurrentSceneName
    {
        get { return currentSceneName; }
        set { currentSceneName = value; }
    }
    #endregion
}
