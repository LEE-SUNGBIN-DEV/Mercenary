using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateSkill : ICharacterState
{
    private int stateWeight;
    private Vector3 moveInput;
    private Vector3 verticalDirection;
    private Vector3 horizontalDirection;
    private Vector3 moveDirection;

    public CharacterStateSkill()
    {
        stateWeight = (int)CHARACTER_STATE_WEIGHT.SKILL;
    }

    public void Enter(Character character)
    {
            character.CharacterStats.CurrentStamina -= Constants.CHARACTER_STAMINA_CONSUMPTION_COUNTER;

            // 키보드 입력 방향으로 공격
            moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
            verticalDirection = new Vector3(character.PlayerCamera.transform.forward.x, 0, character.PlayerCamera.transform.forward.z);
            horizontalDirection = new Vector3(character.PlayerCamera.transform.right.x, 0, character.PlayerCamera.transform.right.z);

            moveDirection = (verticalDirection * moveInput.z + horizontalDirection * moveInput.x).normalized;
            character.transform.forward = (moveDirection == Vector3.zero ? character.transform.forward : moveDirection);

            character.CharacterAnimator.SetTrigger("doCounterSkill");
    }

    public void Update(Character character)
    {
    }

    public void Exit(Character character)
    {
    }

    #region Property
    public int StateWeight
    {
        get => stateWeight;
    }
    #endregion
}
