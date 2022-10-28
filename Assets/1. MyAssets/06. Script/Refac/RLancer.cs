using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RLancer : RCharacter
{
    [SerializeField] private LancerSpear spear;
    [SerializeField] private LancerShield shield;

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

        if (PlayerInput.IsRKeyDown && CharacterStats.CurrentStamina >= GameConstants.CHARACTER_STAMINA_CONSUMPTION_COUNTER)
        {
            nextState = CharacterState.CompareStateWeight(nextState, CHARACTER_STATE.SKILL);
        }

        return nextState;
    }

    #region Animation Event Function
    // Weapon
    private void OnAttack(ATTACK_TYPE attackType)
    {
        switch (attackType)
        {
            case ATTACK_TYPE.COMBO1:
            case ATTACK_TYPE.COMBO2:
            case ATTACK_TYPE.COMBO3:
            case ATTACK_TYPE.COMBO4:
                {
                    spear.DamageRatio = 1f;
                    spear.CombatType = COMBAT_TYPE.NORMAL;
                    break;
                }

            case ATTACK_TYPE.SMASH1:
                {
                    spear.DamageRatio = 1.5f;
                    spear.CombatType = COMBAT_TYPE.SMASH;
                    break;
                }
            case ATTACK_TYPE.SMASH2:
                {
                    spear.DamageRatio = 2.5f;
                    spear.CombatType = COMBAT_TYPE.SMASH;
                    break;
                }
            case ATTACK_TYPE.SMASH3:
                {
                    spear.DamageRatio = 4f;
                    spear.CombatType = COMBAT_TYPE.SMASH;
                    break;
                }
            case ATTACK_TYPE.SMASH4:
                {
                    spear.DamageRatio = 3f;
                    spear.CombatType = COMBAT_TYPE.SMASH;
                    break;
                }
        }
        spear.WeaponCollider.enabled = true;
    }
    private void OffAttack()
    {
        spear.WeaponCollider.enabled = false;
    }
    private void OnDefense(COMBAT_TYPE combatType)
    {
        shield.CombatType = combatType;
    }
    private void OffDefense()
    {
        shield.CombatType = COMBAT_TYPE.DEFENSE;
        shield.WeaponCollider.enabled = false;
    }
    #endregion

    #region Property
    public LancerSpear Spear
    {
        get => spear;
    }
    public LancerShield Shield
    {
        get => shield;
    }
    #endregion
}
