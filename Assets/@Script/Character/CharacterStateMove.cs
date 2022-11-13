using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMove : ICharacterState
{
    private int stateWeight;
    private bool isMove;
    private Vector3 verticalDirection;
    private Vector3 horizontalDirection;
    private Vector3 moveDirection;
    private float moveBlendTreeFloat;

    public CharacterStateMove()
    {
        stateWeight = (int)CHARACTER_STATE_WEIGHT.MOVE;
        isMove = false;
    }

    public void Enter(Character character)
    {
        isMove = false;
        verticalDirection = new Vector3(character.PlayerCamera.transform.forward.x, 0, character.PlayerCamera.transform.forward.z);
        horizontalDirection = new Vector3(character.PlayerCamera.transform.right.x, 0, character.PlayerCamera.transform.right.z);
        moveDirection = (verticalDirection * character.PlayerInput.MoveInput.z + horizontalDirection * character.PlayerInput.MoveInput.x).normalized;
        moveBlendTreeFloat = 0;

        return;
    }
    public void Update(Character character)
    {
        verticalDirection.x = character.PlayerCamera.transform.forward.x;
        verticalDirection.z = character.PlayerCamera.transform.forward.z;

        horizontalDirection.x = character.PlayerCamera.transform.right.x;
        horizontalDirection.z = character.PlayerCamera.transform.right.z;

        moveDirection = (verticalDirection * character.PlayerInput.MoveInput.z + horizontalDirection * character.PlayerInput.MoveInput.x).normalized;

        isMove = (moveDirection.magnitude != 0) ? true : false;

        // Move
        if (isMove)
        {
            // Run
            if (character.PlayerInput.IsLeftShiftKeyDown && character.CharacterStats.CurrentStamina >= Constants.CHARACTER_STAMINA_CONSUMPTION_RUN)
            {
                character.CharacterStats.CurrentStamina -= Constants.CHARACTER_STAMINA_CONSUMPTION_RUN * Time.deltaTime;
                character.CharacterController.Move(moveDirection * (character.CharacterStats.MoveSpeed * 2) * Time.deltaTime);
                moveBlendTreeFloat = Mathf.Lerp(moveBlendTreeFloat, 2, 10f * Time.deltaTime);
            }

            // Walk
            else
            {
                character.CharacterController.Move(moveDirection * character.CharacterStats.MoveSpeed * Time.deltaTime);
                moveBlendTreeFloat = Mathf.Lerp(moveBlendTreeFloat, 1, 10f * Time.deltaTime);
            }

            // Character Look Direction
            character.transform.rotation = Quaternion.Lerp(character.transform.rotation, Quaternion.LookRotation(moveDirection), 10f * Time.deltaTime);
        }

        // Stop
        else
        {
            moveBlendTreeFloat = Mathf.Lerp(moveBlendTreeFloat, 0, 10f * Time.deltaTime);
        }

        character.CharacterAnimator.SetFloat("moveFloat", moveBlendTreeFloat);
    }
    public void Exit(Character character)
    {
        moveBlendTreeFloat = 0f;
        character.CharacterAnimator.SetFloat("moveFloat", moveBlendTreeFloat);
    }

    #region Property
    public int StateWeight
    {
        get => stateWeight;
    }
    #endregion
}