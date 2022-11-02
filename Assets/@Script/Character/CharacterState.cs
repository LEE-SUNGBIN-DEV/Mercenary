using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState
{
    [SerializeField] private Character character;
    [SerializeField] private ICharacterState currentState;
    private Dictionary<CHARACTER_STATE, ICharacterState> stateDictionary;
    private Dictionary<CHARACTER_STATE, CHARACTER_STATE_WEIGHT> stateWeightDictionary;

    public CharacterState(Character character)
    {
        this.character = character;
        currentState = null;

        stateDictionary = new Dictionary<CHARACTER_STATE, ICharacterState>
        {
            // Common
            { CHARACTER_STATE.MOVE, new CharacterStateMove() },
            { CHARACTER_STATE.ATTACK, new CharacterStateAttack() },
            { CHARACTER_STATE.SKILL, new CharacterStateSkill() },
            { CHARACTER_STATE.ROLL, new CharacterStateRoll() },
            { CHARACTER_STATE.HIT, new CharacterStateHit() },
            { CHARACTER_STATE.HEAVY_HIT, new CharacterStateHeavyHit() },
            { CHARACTER_STATE.STUN, new CharacterStateStun() },
            { CHARACTER_STATE.COMPETE, new CharacterStateCompete() },
            { CHARACTER_STATE.DIE, new CharacterStateDie() },

            // Lancer
            { CHARACTER_STATE.LANCER_DEFENSE, new LancerStateDefense() }
        };

        stateWeightDictionary = new Dictionary<CHARACTER_STATE, CHARACTER_STATE_WEIGHT>
        {
            // Common
            { CHARACTER_STATE.MOVE, CHARACTER_STATE_WEIGHT.MOVE },
            { CHARACTER_STATE.ATTACK, CHARACTER_STATE_WEIGHT.ATTACK },
            { CHARACTER_STATE.SKILL, CHARACTER_STATE_WEIGHT.SKILL },
            { CHARACTER_STATE.ROLL, CHARACTER_STATE_WEIGHT.ROLL },
            { CHARACTER_STATE.HIT, CHARACTER_STATE_WEIGHT.HIT },
            { CHARACTER_STATE.HEAVY_HIT, CHARACTER_STATE_WEIGHT.HEAVY_HIT },
            { CHARACTER_STATE.STUN, CHARACTER_STATE_WEIGHT.STUN },
            { CHARACTER_STATE.COMPETE, CHARACTER_STATE_WEIGHT.COMPETE },
            { CHARACTER_STATE.DIE, CHARACTER_STATE_WEIGHT.DIE },

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
