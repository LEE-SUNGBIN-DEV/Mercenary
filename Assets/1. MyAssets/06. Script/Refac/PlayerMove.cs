using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : IPlayerState
{
    private bool isMove;
    private Vector3 verticalDirection;
    private Vector3 horizontalDirection;
    private Vector3 moveDirection;
    private float moveBlendTreeFloat;

    public void Enter(RPlayer player)
    {
        isMove = false;
        verticalDirection = new Vector3(player.PlayerCamera.transform.forward.x, 0, player.PlayerCamera.transform.forward.z);
        horizontalDirection = new Vector3(player.PlayerCamera.transform.right.x, 0, player.PlayerCamera.transform.right.z);
        moveDirection = Vector3.zero;
        moveBlendTreeFloat = 0;

        return;
    }
    public void Update(RPlayer player)
    {
        verticalDirection.x = player.PlayerCamera.transform.forward.x;
        verticalDirection.z = player.PlayerCamera.transform.forward.z;

        horizontalDirection.x = player.PlayerCamera.transform.right.x;
        horizontalDirection.z = player.PlayerCamera.transform.right.z;

        moveDirection = (verticalDirection * player.PlayerInput.MoveInput.z + horizontalDirection * player.PlayerInput.MoveInput.x).normalized;

        isMove = (moveDirection.magnitude != 0) ? true : false;

        // Move
        if (isMove)
        {
            // Run
            if (player.PlayerInput.IsLeftShiftKeyDown && player.PlayerStats.CurrentStamina >= GameConstants.PLAYER_STAMINA_CONSUMPTION_RUN)
            {
                player.PlayerStats.CurrentStamina -= GameConstants.PLAYER_STAMINA_CONSUMPTION_RUN * Time.deltaTime;
                player.CharacterController.Move(moveDirection * (player.PlayerStats.MoveSpeed * 2) * Time.deltaTime);
                moveBlendTreeFloat = Mathf.Lerp(moveBlendTreeFloat, 2, 10f * Time.deltaTime);
            }

            // Walk
            else
            {
                player.CharacterController.Move(moveDirection * player.PlayerStats.MoveSpeed * Time.deltaTime);
                moveBlendTreeFloat = Mathf.Lerp(moveBlendTreeFloat, 1, 10f * Time.deltaTime);
            }

            // Character Look Direction
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.LookRotation(moveDirection), 10f * Time.deltaTime);
        }

        // Stop
        else
        {
            moveBlendTreeFloat = Mathf.Lerp(moveBlendTreeFloat, 0, 10f * Time.deltaTime);
        }

        player.PlayerAnimator.SetFloat("moveFloat", moveBlendTreeFloat);
    }
    public void Exit(RPlayer player)
    {
    }
}