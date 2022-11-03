using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager
{
    public event UnityAction OnSaveGame;

    [Header("Player Data")]
    [SerializeField] private Player player;
    [SerializeField] private Character currentCharacter;

    [Header("Camera")]
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private BaseCamera directingCamera;

    [Header("Cursor")]
    private CURSOR_MODE cursorMode;

    public void Initialize()
    {
        // 해상도
        Screen.SetResolution(GameConstants.RESOLUTION_DEFAULT_WIDTH, GameConstants.RESOLUTION_DEFAULT_HEIGHT, true);

        // 커서
        Managers.ResourceManager.LoadResourceAsync<Texture2D>("Sprite_Cursor", SetCursorTexture);
        SetCursorMode(CURSOR_MODE.UNLOCK);
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
            //CurrentCharacter.CharacterData.MaxExperience = Managers.DataManager.LevelDataDictionary[CurrentCharacter.CharacterData.Level];
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
    public void QuitGame()
    {
        PlayButtonClickSound();
        Application.Quit();
    }
    #endregion

    #region Cursor Control
    public void SetCursorTexture(Texture2D texture)
    {
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
    }
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
    public PlayerCamera PlayerCamera
    {
        get { return playerCamera; }
        set { playerCamera = value; }
    }
    public BaseCamera DirectingCamera
    {
        get { return directingCamera; }
        set { directingCamera = value; }
    }
    #endregion
}
