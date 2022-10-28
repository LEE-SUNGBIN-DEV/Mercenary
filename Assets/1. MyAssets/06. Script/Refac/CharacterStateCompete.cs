using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateCompete : ICharacterState
{
    private int stateWeight;
    private float competeTime;

    public CharacterStateCompete()
    {
        stateWeight = (int)CHARACTER_STATE_WEIGHT.COMPETE;
    }

    public void Enter(RCharacter character)
    {
        // Effect
        EffectPoolManager.Instance.RequestObject(EFFECT_POOL.PLAYER_COMPETE_START, character.transform.position);

        // Set Compete State
        character.CharacterAnimator.SetTrigger("doCompete");
        competeTime = 0f;
    }

    public void Update(RCharacter character)
    {
        if(competeTime < GameConstants.TIME_COMPETE)
        {
            competeTime += Time.deltaTime;
        }

        else
        {
            //shield.gameObject.SetActive(false);
            character.CharacterCamera.gameObject.SetActive(true);
            character.SubCamera.gameObject.SetActive(false);
        }
    }

    public void Exit(RCharacter character)
    {
        character.CharacterAnimator.SetTrigger("doCompeteAttack");
    }

    #region Property
    public int StateWeight
    {
        get => stateWeight;
    }
    #endregion
}
