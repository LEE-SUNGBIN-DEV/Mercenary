using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CharacterStats
{
    public static event UnityAction<CharacterStats> OnCharacterStatsChanged;
    public event UnityAction<CharacterStats> OnDie;

    private Character character;
    private float attackPower;
    private float defensivePower;

    private float maxHitPoint;
    private float currentHitPoint;
    private float maxStamina;
    private float currentStamina;

    private float criticalChance;
    private float criticalDamage;
    private float attackSpeed;
    private float moveSpeed;

    public CharacterStats(Character owner)
    {
        character = owner;

        character.CharacterData.OnPlayerDataChanged -= UpdateStats;
        character.CharacterData.OnPlayerDataChanged += UpdateStats;

        UpdateStats(character.CharacterData);
    }

    public void UpdateStats(CharacterData characterData)
    {
        AttackPower = characterData.Strength * 2;
        DefensivePower = characterData.Strength;

        MaxHitPoint = characterData.Vitality * 10;
        CurrentHitPoint = characterData.Vitality * 10;
        MaxStamina = characterData.Vitality * 10;
        CurrentStamina = characterData.Vitality * 10;

        CriticalChance = characterData.Luck;
        CriticalDamage = Constants.CHARACTER_STAT_CRITICAL_DAMAGE_DEFAULT + characterData.Luck;
        AttackSpeed = Constants.CHARACTER_STAT_ATTACK_SPEED_DEFAULT + characterData.Dexterity * 0.01f;
        MoveSpeed = Constants.CHARACTER_STAT_MOVE_SPEED_DEFAULT + characterData.Dexterity * 0.02f;
    }

    #region Property
    public float AttackPower
    {
        get { return attackPower; }
        set
        {
            attackPower = value;
            if (attackPower < 0)
            {
                attackPower = 0;
            }
            OnCharacterStatsChanged?.Invoke(this);
        }
    }
    public float DefensivePower
    {
        get { return defensivePower; }
        set
        {
            defensivePower = value;
            if (defensivePower < 0)
            {
                defensivePower = 0;
            }
            OnCharacterStatsChanged?.Invoke(this);
        }
    }
    public float MaxHitPoint
    {
        get { return maxHitPoint; }
        set
        {
            maxHitPoint = value;
            if (maxHitPoint <= 0)
            {
                maxHitPoint = 1;
            }
            OnCharacterStatsChanged?.Invoke(this);
        }
    }
    public float CurrentHitPoint
    {
        get { return currentHitPoint; }
        set
        {
            currentHitPoint = value;
            if (currentHitPoint > MaxHitPoint)
            {
                currentHitPoint = MaxHitPoint;
            }

            if (currentHitPoint < 0)
            {
                currentHitPoint = 0;
                OnDie(this);
            }
            OnCharacterStatsChanged?.Invoke(this);
        }
    }
    public float MaxStamina
    {
        get { return maxStamina; }
        set
        {
            maxStamina = value;
            if (maxStamina <= 0)
            {
                maxStamina = 1;
            }
            OnCharacterStatsChanged?.Invoke(this);
        }
    }
    public float CurrentStamina
    {
        get { return currentStamina; }
        set
        {
            currentStamina = value;
            if (currentStamina > MaxStamina)
            {
                currentStamina = MaxStamina;
            }

            if (currentStamina < 0)
            {
                currentStamina = 0;
            }

            OnCharacterStatsChanged?.Invoke(this);
        }
    }
    public float AttackSpeed
    {
        get { return attackSpeed; }
        set
        {
            attackSpeed = value;

            if (attackSpeed < Constants.CHARACTER_STAT_ATTACK_SPEED_MIN)
            {
                attackSpeed = Constants.CHARACTER_STAT_ATTACK_SPEED_MIN;
            }

            if (attackSpeed > Constants.CHARACTER_STAT_ATTACK_SPEED_MAX)
            {
                attackSpeed = Constants.CHARACTER_STAT_ATTACK_SPEED_MAX;
            }

            character.CharacterAnimator.SetFloat("attackSpeed", attackSpeed);
            OnCharacterStatsChanged?.Invoke(this);
        }
    }
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set
        {
            moveSpeed = value;

            if (moveSpeed < Constants.CHARACTER_STAT_MOVE_SPEED_MIN)
            {
                moveSpeed = Constants.CHARACTER_STAT_MOVE_SPEED_MIN;
            }

            if (moveSpeed > Constants.CHARACTER_STAT_MOVE_SPEED_MAX)
            {
                moveSpeed = Constants.CHARACTER_STAT_MOVE_SPEED_MAX;
            }

            OnCharacterStatsChanged?.Invoke(this);
        }
    }
    public float CriticalChance
    {
        get { return criticalChance; }
        set
        {
            criticalChance = value;
            if (criticalChance < Constants.CHARACTER_STAT_CRITICAL_CHANCE_MIN)
            {
                criticalChance = Constants.CHARACTER_STAT_CRITICAL_CHANCE_MIN;
            }

            if (criticalChance > Constants.CHARACTER_STAT_CRITICAL_CHANCE_MAX)
            {
                criticalChance = Constants.CHARACTER_STAT_CRITICAL_CHANCE_MAX;
            }

            OnCharacterStatsChanged?.Invoke(this);
        }
    }
    public float CriticalDamage
    {
        get { return criticalDamage; }
        set
        {
            criticalDamage = value;
            if (criticalDamage < Constants.CHARACTER_STAT_CRITICAL_DAMAGE_MIN)
            {
                criticalDamage = Constants.CHARACTER_STAT_CRITICAL_DAMAGE_MIN;
            }
            OnCharacterStatsChanged?.Invoke(this);
        }
    }
    #endregion
}
