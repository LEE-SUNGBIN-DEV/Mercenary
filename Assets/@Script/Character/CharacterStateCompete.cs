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

    public void Enter(Character character)
    {
        // Effect
        Managers.ObjectPoolManager.RequestObject(Constants.RESOURCE_NAME_EFFECT_PLAYER_COMPETE_START, character.transform.position);

        // Set Compete State
        character.CharacterAnimator.SetTrigger("doCompete");
        competeTime = 0f;
    }

    public void Update(Character character)
    {
        if(competeTime < Constants.TIME_COMPETE)
        {
            competeTime += Time.deltaTime;
        }

        else
        {
            //shield.gameObject.SetActive(false);

        }
    }

    public void Exit(Character character)
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
