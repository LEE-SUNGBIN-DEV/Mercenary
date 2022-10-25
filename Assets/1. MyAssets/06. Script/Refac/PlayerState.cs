using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PLAYER_STATE
{
    IDLE,
    MOVE,
    ROLL,
    ATTACK,
    SMASH,
    DEFEND,
    PERFECT_DEFEND,
    SKILL,
    HIT,
    HEAVY_HIT,
    STUN,
    DOWN,
    GETTING_UP,
    COMPETE,
    DIE,

    LENGTH
}

public class PlayerState
{
    [SerializeField] private RPlayer player;
    [SerializeField] private IPlayerState currentState;
    private Dictionary<PLAYER_STATE, IPlayerState> stateDictionary;

    public PlayerState(RPlayer player)
    {
        this.player = player;
        currentState = null;
        
        stateDictionary = new Dictionary<PLAYER_STATE, IPlayerState>
        {
            { PLAYER_STATE.MOVE, new PlayerMove() },
            { PLAYER_STATE.ATTACK, new PlayerAttack() },
            { PLAYER_STATE.SMASH, new PlayerSmash() }
        };
    }

    public void SwitchPlayerState(PLAYER_STATE targetState)
    {
        currentState?.Exit(player);
        currentState = stateDictionary[targetState];
        currentState?.Enter(player);
    }

    public void ForcedSwitchState(PLAYER_STATE targetState)
    {
        currentState?.Exit(player);
        currentState = stateDictionary[targetState];
        currentState?.Enter(player);
    }

    #region Property
    public IPlayerState CurrentState
    {
        get => currentState;
    }
    #endregion
}
