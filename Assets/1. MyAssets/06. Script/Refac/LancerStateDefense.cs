using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerStateDefense : ICharacterState
{
    private int stateWeight;
    private bool isDefense;
    private RLancer lancer;

    public LancerStateDefense()
    {
        stateWeight = (int)CHARACTER_STATE_WEIGHT.LANCER_DEFENSE;
        isDefense = false;
        lancer = null;
    }

    public void Enter(RCharacter character)
    {
        isDefense = false;
        lancer = character as RLancer;
    }
    public void Update(RCharacter character)
    {
        if (character.PlayerInput.IsMouseRightDown)
        {
            if (!isDefense)
            {
                lancer.Shield?.gameObject.SetActive(true);
            }

            character.gameObject.tag = GameConstants.TAG_INVINCIBILITY;
            isDefense = true;
            character.CharacterAnimator.SetBool("isDefense", true);
        }

        if (character.PlayerInput.IsMouseRightUp && isDefense)
        {
            character.CharacterAnimator.SetBool("isDefense", false);
        }

        if (isDefense)
        {
            if (character.PlayerInput.IsMouseRightDown || character.PlayerInput.IsMouseRightUp)
            {
                character.CharacterAnimator.SetBool("isCounterAttack", !character.PlayerInput.IsMouseRightUp);
            }
        }
    }
    public void Exit(RCharacter character)
    {
        isDefense = false;
        character.CharacterAnimator.SetBool("isDefense", false);
        character.CharacterAnimator.SetBool("isCounterAttack", false);
        lancer.Shield?.gameObject.SetActive(false);
        lancer = null;
    }

    #region Property
    public int StateWeight
    {
        get => stateWeight;
    }
    #endregion
}
