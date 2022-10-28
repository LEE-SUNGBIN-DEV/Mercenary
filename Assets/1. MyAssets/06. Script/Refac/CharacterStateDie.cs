using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateDie : ICharacterState
{
    private int stateWeight;

    public CharacterStateDie()
    {
        stateWeight = (int)CHARACTER_STATE_WEIGHT.DIE;
    }

    public void Enter(RCharacter character)
    {
        character.CharacterAnimator.SetTrigger("doDie");
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
