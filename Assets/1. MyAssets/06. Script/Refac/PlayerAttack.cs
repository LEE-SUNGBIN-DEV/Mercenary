using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : IPlayerState
{
    private Vector3 lookDirection;
    private IEnumerator attackCoroutine;

    public void Enter(RPlayer player)
    {
        player.PlayerAnimator.SetBool("isComboAttack", true);
    }

    public void Update(RPlayer player)
    {
        // Combo Attack
        if (player.PlayerInput.IsMouseLeftDown || player.PlayerInput.IsMouseLeftUp)
        {
            // 방향 전환
            lookDirection = player.PlayerCamera.transform.forward;
            lookDirection.y = 0f;
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.LookRotation(lookDirection), 10f * Time.deltaTime);

            player.PlayerAnimator.SetBool("isComboAttack", !player.PlayerInput.IsMouseLeftUp);
        }
    }

    public void Exit(RPlayer player)
    {
        player.PlayerAnimator.SetBool("isComboAttack", false);
    }
}
