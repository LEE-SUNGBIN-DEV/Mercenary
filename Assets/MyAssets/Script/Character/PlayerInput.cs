using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput
{
    public UnityAction<PlayerInput> OnPlayerInput;

    private Vector3 moveInput;

    [SerializeField] private bool isMouseLeftDown;
    [SerializeField] private bool isMouseLeftUp;
    [SerializeField] private bool isMouseRightDown;
    [SerializeField] private bool isMouseRightUp;

    [SerializeField] private bool isLeftShiftKeyDown;
    [SerializeField] private bool isSpaceKeyDown;
    [SerializeField] private bool isEscapeKeyDown;

    [SerializeField] private bool isRKeyDown;
    [SerializeField] private bool isOKeyDown;
    [SerializeField] private bool isIKeyDown;
    [SerializeField] private bool isTKeyDown;
    [SerializeField] private bool isQKeyDown;
    [SerializeField] private bool isHKeyDown;

    // »ý¼ºÀÚ
    public PlayerInput()
    {
        moveInput = Vector3.zero;
        isMouseLeftDown = false;
        isMouseLeftUp = false;
        isMouseRightDown = false;
        isMouseRightUp = false;

        isLeftShiftKeyDown = false;
        isSpaceKeyDown = false;
        isEscapeKeyDown = false;

        isRKeyDown = false;
        isOKeyDown = false;
        isIKeyDown = false;
        isTKeyDown = false;
        isQKeyDown = false;
        isHKeyDown = false;
    }

    public void GetInput()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = 0;
        moveInput.z = Input.GetAxisRaw("Vertical");

        isMouseLeftDown = Input.GetMouseButton(0);
        isMouseLeftUp = Input.GetMouseButtonUp(0);
        isMouseRightDown = Input.GetMouseButton(1);
        isMouseRightUp = Input.GetMouseButtonUp(1);

        isLeftShiftKeyDown = Input.GetKey(KeyCode.LeftShift);
        isSpaceKeyDown = Input.GetKeyDown(KeyCode.Space);
        isEscapeKeyDown = Input.GetKeyDown(KeyCode.Escape);

        isRKeyDown = Input.GetKey(KeyCode.R);
        isOKeyDown = Input.GetKeyDown(KeyCode.O);
        isIKeyDown = Input.GetKeyDown(KeyCode.I);
        isTKeyDown = Input.GetKeyDown(KeyCode.T);
        isQKeyDown = Input.GetKeyDown(KeyCode.Q);
        isHKeyDown = Input.GetKeyDown(KeyCode.H);

        OnPlayerInput?.Invoke(this);
    }

    #region Property
    public Vector3 MoveInput
    {
        get => moveInput;
    }
    public bool IsMouseLeftDown
    {
        get => isMouseLeftDown;
    }
    public bool IsMouseLeftUp
    {
        get => isMouseLeftUp;
    }
    public bool IsMouseRightDown
    {
        get => isMouseRightDown;
    }
    public bool IsMouseRightUp
    {
        get => isMouseRightUp;
    }
    public bool IsLeftShiftKeyDown
    {
        get => isLeftShiftKeyDown;
    }
    public bool IsSpaceKeyDown
    {
        get => isSpaceKeyDown;
    }
    public bool IsEscapeKeyDown
    {
        get => isEscapeKeyDown;
    }
    public bool IsRKeyDown
    {
        get => isRKeyDown;
    }
    public bool IsOKeyDown
    {
        get => isOKeyDown;
    }
    public bool IsIKeyDown
    {
        get => isIKeyDown;
    }
    public bool IsTKeyDown
    {
        get => isTKeyDown;
    }
    public bool IsQKeyDown
    {
        get => isQKeyDown;
    }
    public bool IsHKeyDown
    {
        get => isHKeyDown;
    }
    #endregion
}
