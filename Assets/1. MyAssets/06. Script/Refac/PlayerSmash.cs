using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmash : IPlayerState
{
    private IEnumerator smashCoroutine;

    public void Enter(RPlayer player)
    {
        player.PlayerAnimator.SetBool("isSmashAttack", true);
    }

    public void Update(RPlayer player)
    {
        if (player.PlayerInput.IsMouseRightDown || player.PlayerInput.IsMouseRightUp)
        {
            player.PlayerAnimator.SetBool("isSmashAttack", !player.PlayerInput.IsMouseRightUp);
        }
    }

    public void Exit(RPlayer player)
    {
        player.PlayerAnimator.SetBool("isSmashAttack", false);
    }
}
