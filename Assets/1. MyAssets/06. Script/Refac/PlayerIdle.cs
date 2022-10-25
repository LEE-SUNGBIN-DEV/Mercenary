using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : IPlayerState
{
    [SerializeField] private PLAYER_STATE currentState;

    public void Enter(RPlayer player)
    {
        currentState = PLAYER_STATE.IDLE;

        return;
    }
    public void Update(RPlayer player)
    {
    }
    public void Exit(RPlayer player)
    {
    }

    #region
    public PLAYER_STATE CurrentState
    {
        get => currentState;
    }
    #endregion
}
