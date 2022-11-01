using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateStun : ICharacterState
{
    private int stateWeight;

    public CharacterStateStun()
    {
        stateWeight = (int)CHARACTER_STATE_WEIGHT.STUN;
    }

    public void Enter(Character character)
    {
        character.CharacterAnimator.SetTrigger("doStun");
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
