using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState
{
    [SerializeField] private RCharacter character;
    [SerializeField] private ICharacterState currentState;
    private Dictionary<CHARACTER_STATE, ICharacterState> stateDictionary;
    private Dictionary<CHARACTER_STATE, CHARACTER_STATE_WEIGHT> stateWeightDictionary;

    public CharacterState(RCharacter character)
    {
        this.character = character;
        currentState = null;
        
        stateDictionary = new Dictionary<CHARACTER_STATE, ICharacterState>
        {
            // Common
            { CHARACTER_STATE.MOVE, new CharacterMove() },
            { CHARACTER_STATE.ATTACK, new CharacterAttack() },
            { CHARACTER_STATE.ROLL, new CharacterRoll() },

            // Lancer
            { CHARACTER_STATE.LANCER_DEFENSE, new LancerDefense() }
        };

        stateWeightDictionary = new Dictionary<CHARACTER_STATE, CHARACTER_STATE_WEIGHT>
        {
            // Common
            { CHARACTER_STATE.MOVE, CHARACTER_STATE_WEIGHT.MOVE },
            { CHARACTER_STATE.ATTACK, CHARACTER_STATE_WEIGHT.ATTACK },
            { CHARACTER_STATE.ROLL, CHARACTER_STATE_WEIGHT.ROLL },

            // Lancer
            { CHARACTER_STATE.LANCER_DEFENSE, CHARACTER_STATE_WEIGHT.LANCER_DEFENSE }
        };
    }

    public void SwitchCharacterState(CHARACTER_STATE targetState)
    {
        currentState?.Exit(character);
        currentState = stateDictionary[targetState];
        currentState?.Enter(character);
    }

    public void SwitchCharacterStateByWeight(CHARACTER_STATE targetState)
    {
        if (currentState?.StateWeight >= (int)stateWeightDictionary[targetState])
        {
            return;
        }
        SwitchCharacterState(targetState);
    }

    public CHARACTER_STATE CompareStateWeight(CHARACTER_STATE targetStateA, CHARACTER_STATE targetStateB)
    {
        if ((int)StateWeightDictionary[targetStateA] < (int)StateWeightDictionary[targetStateB])
        {
            return targetStateB;
        }

        else
        {
            return targetStateA;
        }
    }

    #region Property
    public ICharacterState CurrentState
    {
        get => currentState;
    }
    public Dictionary<CHARACTER_STATE, ICharacterState> StateDictionary
    {
        get => stateDictionary;
    }
    public Dictionary<CHARACTER_STATE, CHARACTER_STATE_WEIGHT> StateWeightDictionary
    {
        get => stateWeightDictionary;
    }
    #endregion
}
