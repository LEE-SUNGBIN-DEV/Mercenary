using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UserInput
{
    private Vector3 moveInput;

    private bool isMouseLeftDown;
    private bool isMouseLeftUp;
    private bool isMouseRightDown;
    private bool isMouseRightUp;

    private bool isLeftShiftKeyDown;
    private bool isSpaceKeyDown;
    private bool isEscapeKeyDown;

    private bool isRKeyDown;
    private bool isOKeyDown;
    private bool isIKeyDown;
    private bool isTKeyDown;
    private bool isQKeyDown;
    private bool isHKeyDown;

    // »ý¼ºÀÚ
    public UserInput()
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

    public void GetUserInput()
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
