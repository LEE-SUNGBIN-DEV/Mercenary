using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRoll : ICharacterState
{
    public int stateWeight;
    private Vector3 moveInput;
    private Vector3 verticalDirection;
    private Vector3 horizontalDirection;
    private Vector3 moveDirection;

    public CharacterRoll()
    {
        stateWeight = (int)CHARACTER_STATE_WEIGHT.ROLL;
    }

    public void Enter(RCharacter character)
    {
        character.CharacterStats.CurrentStamina -= GameConstants.CHARACTER_STAMINA_CONSUMPTION_ROLL;

        // 키보드 입력 방향으로 회피
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        verticalDirection = new Vector3(character.CharacterCamera.transform.forward.x, 0, character.CharacterCamera.transform.forward.z);
        horizontalDirection = new Vector3(character.CharacterCamera.transform.right.x, 0, character.CharacterCamera.transform.right.z);
        moveDirection = (verticalDirection * moveInput.z + horizontalDirection * moveInput.x).normalized;

        character.transform.forward = (moveDirection == Vector3.zero ? character.transform.forward : moveDirection);

        // Set Roll State
        character.gameObject.tag = GameConstants.TAG_INVINCIBILITY;
        character.CharacterAnimator.SetTrigger("doRoll");
    }

    public void Update(RCharacter character)
    {
    }

    public void Exit(RCharacter character)
    {
        character.gameObject.tag = GameConstants.TAG_PLAYER;
    }

    #region Property
    public int StateWeight
    {
        get => stateWeight;
    }
    #endregion
}
