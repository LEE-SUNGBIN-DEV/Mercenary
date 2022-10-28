using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateHeavyHit : ICharacterState
{
    private int stateWeight;

    public CharacterStateHeavyHit()
    {
        stateWeight = (int)CHARACTER_STATE_WEIGHT.HEAVY_HIT;
    }

    public void Enter(RCharacter character)
    {
        character.CharacterAnimator.SetTrigger("doHeavyHit");
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
