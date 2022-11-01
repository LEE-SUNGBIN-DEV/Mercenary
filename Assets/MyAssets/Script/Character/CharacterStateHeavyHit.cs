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

    public void Enter(Character character)
    {
        character.CharacterAnimator.SetTrigger("doHeavyHit");
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
