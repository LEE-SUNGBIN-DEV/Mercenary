using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RLancer : RPlayer
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        PlayerState.SwitchPlayerState(PLAYER_STATE.MOVE);
    }

    private void Update()
    {
        PlayerInput?.GetInput();
        DeterminePlayerState();
        PlayerState?.CurrentState?.Update(this);
    }

    public override PLAYER_STATE DeterminePlayerState()
    {
        throw new System.NotImplementedException();
    }
}
