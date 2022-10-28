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

    public void Enter(RCharacter character)
    {
        character.CharacterAnimator.SetTrigger("doHit");
    }

    public void Update(RCharacter character)
    {
    }

    public void Exit(RCharacter character)
    {
    }

    #region Property
    public int StateWeight
    {
        get => stateWeight;
    }
    #endregion
}
