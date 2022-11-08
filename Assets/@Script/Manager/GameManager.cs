using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    [Header("Camera")]
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private BaseCamera directingCamera;

    [Header("Cursor")]
    private CURSOR_MODE cursorMode;

    public void Initialize()
    {
        // �ػ�
        Screen.SetResolution(GameConstants.RESOLUTION_DEFAULT_WIDTH, GameConstants.RESOLUTION_DEFAULT_HEIGHT, true);

        // Ŀ��
        Managers.ResourceManager.LoadResourceAsync<Texture2D>("Sprite_Cursor", SetCursorTexture);
        SetCursorMode(CURSOR_MODE.UNLOCK);
    }

    #region Cursor Function
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
