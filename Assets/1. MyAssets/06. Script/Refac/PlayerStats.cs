using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerStats
{
    #region Event
    public static event UnityAction<PlayerStats> OnPlayerStatsChanged;
    #endregion

    [SerializeField] private float attackPower;
    [SerializeField] private float defensivePower;

    [SerializeField] private float maxHitPoint;
    [SerializeField] private float currentHitPoint;
    [SerializeField] private float maxStamina;
    [SerializeField] private float currentStamina;

    [SerializeField] private float criticalChance;
    [SerializeField] private float criticalDamage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float moveSpeed;

    // »ý¼ºÀÚ
    public PlayerStats()
    {
        AttackPower = 0f;
        DefensivePower = 0f;

        MaxHitPoint = 0f;
        CurrentHitPoint = 0f;
        MaxStamina = 0f;
        CurrentStamina = 0f;

        CriticalChance = 0f;
        CriticalDamage = 0f;
        AttackSpeed = 0f;
        MoveSpeed = 0f;

        PlayerData.OnPlayerDataChanged -= UpdateStats;
        PlayerData.OnPlayerDataChanged += UpdateStats;
    }

    public void UpdateStats(PlayerData playerData)
    {
        AttackPower = playerData.Strength * 2;
        DefensivePower = playerData.Strength;

        MaxHitPoint = playerData.Vitality * 10;
        CurrentHitPoint = playerData.Vitality * 10;
        MaxStamina = playerData.Vitality * 10;
        CurrentStamina = playerData.Vitality * 10;

        CriticalChance = playerData.Luck;
        CriticalDamage = GameConstants.PLAYER_STAT_CRITICAL_DAMAGE_DEFAULT + playerData.Luck;
        AttackSpeed = GameConstants.PLAYER_STAT_ATTACK_SPEED_DEFAULT + playerData.Dexterity * 0.01f;
        MoveSpeed = GameConstants.PLAYER_STAT_MOVE_SPEED_DEFAULT + playerData.Dexterity * 0.02f;
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
            OnPlayerStatsChanged?.Invoke(this);
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
            OnPlayerStatsChanged?.Invoke(this);
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
            OnPlayerStatsChanged?.Invoke(this);
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
            }
            OnPlayerStatsChanged?.Invoke(this);
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
            OnPlayerStatsChanged?.Invoke(this);
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

            OnPlayerStatsChanged?.Invoke(this);
        }
    }
    public float AttackSpeed
    {
        get { return attackSpeed; }
        set
        {
            attackSpeed = value;

            if (attackSpeed < GameConstants.PLAYER_STAT_ATTACK_SPEED_MIN)
            {
                attackSpeed = GameConstants.PLAYER_STAT_ATTACK_SPEED_MIN;
            }

            if (attackSpeed > GameConstants.PLAYER_STAT_ATTACK_SPEED_MAX)
            {
                attackSpeed = GameConstants.PLAYER_STAT_ATTACK_SPEED_MAX;
            }

            OnPlayerStatsChanged?.Invoke(this);
        }
    }
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set
        {
            moveSpeed = value;

            if (moveSpeed < GameConstants.PLAYER_STAT_MOVE_SPEED_MIN)
            {
                moveSpeed = GameConstants.PLAYER_STAT_MOVE_SPEED_MIN;
            }

            if (moveSpeed > GameConstants.PLAYER_STAT_MOVE_SPEED_MAX)
            {
                moveSpeed = GameConstants.PLAYER_STAT_MOVE_SPEED_MAX;
            }

            OnPlayerStatsChanged?.Invoke(this);
        }
    }
    public float CriticalChance
    {
        get { return criticalChance; }
        set
        {
            criticalChance = value;
            if (criticalChance < GameConstants.PLAYER_STAT_CRITICAL_CHANCE_MIN)
            {
                criticalChance = GameConstants.PLAYER_STAT_CRITICAL_CHANCE_MIN;
            }

            if (criticalChance > GameConstants.PLAYER_STAT_CRITICAL_CHANCE_MAX)
            {
                criticalChance = GameConstants.PLAYER_STAT_CRITICAL_CHANCE_MAX;
            }

            OnPlayerStatsChanged?.Invoke(this);
        }
    }
    public float CriticalDamage
    {
        get { return criticalDamage; }
        set
        {
            criticalDamage = value;
            if (criticalDamage < GameConstants.PLAYER_STAT_CRITICAL_DAMAGE_MIN)
            {
                criticalDamage = GameConstants.PLAYER_STAT_CRITICAL_DAMAGE_MIN;
            }
            OnPlayerStatsChanged?.Invoke(this);
        }
    }
    #endregion
}
