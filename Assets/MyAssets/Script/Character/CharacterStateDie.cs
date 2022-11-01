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

    public void Enter(Character character)
    {
        character.CharacterAnimator.SetTrigger("doDie");
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
