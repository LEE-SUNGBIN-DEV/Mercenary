using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateHit : ICharacterState
{
    private int stateWeight;

    public CharacterStateHit()
    {
        stateWeight = (int)CHARACTER_STATE_WEIGHT.HIT;
    }

    public void Enter(Character character)
    {
        character.CharacterAnimator.SetTrigger("doHit");
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
