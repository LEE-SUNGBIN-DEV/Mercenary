using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RLancer : RCharacter
{
    [SerializeField] private PlayerAttackController spear;
    [SerializeField] private LancerDefenseController shield;

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        CharacterState.SwitchCharacterStateByWeight(CHARACTER_STATE.MOVE);
    }

    private void Update()
    {
        PlayerInput?.GetInput();
        CharacterState?.SwitchCharacterStateByWeight(DetermineCharacterState());
        CharacterState?.CurrentState?.Update(this);
    }

    public override CHARACTER_STATE DetermineCharacterState()
    {
        CHARACTER_STATE nextState = CHARACTER_STATE.MOVE;

        if (PlayerInput.IsMouseLeftDown)
        {
            nextState = CharacterState.CompareStateWeight(nextState, CHARACTER_STATE.ATTACK);
        }

        if (PlayerInput.IsMouseRightDown)
        {
            nextState = CharacterState.CompareStateWeight(nextState, CHARACTER_STATE.LANCER_DEFENSE);
        }

        if (PlayerInput.IsSpaceKeyDown && CharacterStats.CurrentStamina >= GameConstants.CHARACTER_STAMINA_CONSUMPTION_ROLL)
        {
            nextState = CharacterState.CompareStateWeight(nextState, CHARACTER_STATE.ROLL);
        }

        if (PlayerInput.IsRKeyDown)
        {
            nextState = CharacterState.CompareStateWeight(nextState, CHARACTER_STATE.SKILL);
        }

        return nextState;
    }

    #region Property
    public PlayerAttackController Spear
    {
        get => spear;
    }
    public LancerDefenseController Shield
    {
        get => shield;
    }
    #endregion
}
