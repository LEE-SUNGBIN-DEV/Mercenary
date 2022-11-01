using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager
{
    #region Event
    public event UnityAction OnLoadScene;
    public event UnityAction OnSceneLoaded;
    public event UnityAction OnStartGame;
    public event UnityAction OnClickLoadGameButton;
    public event UnityAction OnSaveGame;
    #endregion

    [Header("Player Data")]
    [SerializeField] private Player player;
    [SerializeField] private Character currentCharacter;

    [Header("Camera")]
    [SerializeField] private CharacterCamera playerCamera;
    [SerializeField] private BaseCamera directingCamera;

    [Header("Cursor")]
    private CURSOR_MODE cursorMode;
    [SerializeField] private Texture2D cursorTexture;

    [Header("Scene")]
    private Dictionary<SCENE_LIST, string> sceneDictionary;
    private SCENE_TYPE currentSceneType;

    public void Initialize()
    {
        Screen.SetResolution(GameConstants.RESOLUTION_DEFAULT_WIDTH, GameConstants.RESOLUTION_DEFAULT_HEIGHT, true);

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
        CampaignPopUp.OnClickCampaignButton -= LoadScene;
        CampaignPopUp.OnClickCampaignButton += LoadScene;

        ReturnPanel.onReturnViliage += () =>
        {
            LoadScene(SCENE_LIST.VILIAGE);
        };
        #endregion
    }

    #region Game Control
    public void PlayButtonClickSound()
    {
        Managers.AudioManager.PlaySFX("Button Click");
    }

    public void StartGame()
    {
        PlayButtonClickSound();

        if (Managers.DataManager.FileCheck())
        {
            StartGame();

            CurrentCharacter.CharacterData.MaxExperience = Managers.DataManager.LevelDataDictionary[CurrentCharacter.CharacterData.Level];
            LoadScene(SCENE_LIST.VILIAGE);
        }

        else
        {
            Managers.UIManager.RequestNotice("플레이 데이터가 없습니다.");
        }
    }

    public void SaveGame()
    {
        OnSaveGame();
    }
    public void SaveAndQuitGame()
    {
        LoadScene(SCENE_LIST.TITLE);
    }
    public void QuitGame()
    {
        PlayButtonClickSound();
        Application.Quit();
    }
    #endregion

    #region Scene Control
    public void LoadScene(string sceneName)
    {
        OnLoadScene();
        if (CurrentCharacter != null)
        {
            CurrentCharacter.gameObject.SetActive(false);
        }
        LoadingManager.LoadScene(sceneName);
    }
    public void LoadScene(SCENE_LIST requestScene)
    {
        LoadScene(sceneDictionary[requestScene]);
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
    public Character CurrentCharacter
    {
        get { return currentCharacter; }
        private set { currentCharacter = value; }
    }
    public CharacterCamera PlayerCamera
    {
        get { return playerCamera; }
        set { playerCamera = value; }
    }
    public BaseCamera DirectingCamera
    {
        get { return directingCamera; }
        set { directingCamera = value; }
    }
    public SCENE_TYPE CurrentSceneType
    {
        get { return currentSceneType; }
        set { currentSceneType = value; }
    }
    #endregion
}
