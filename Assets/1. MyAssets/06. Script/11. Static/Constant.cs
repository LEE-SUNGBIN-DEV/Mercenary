using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constant
{
    #region Player Constant
    // Inventory
    public const int inventorySlotCount = 30;
    public const int quickSlotCount = 4;
    public const int equipmentSlotCount = 4;

    // Stamina
    public const float recoverStaminaPercentage = 15f;
    public const float runStamina = 2f;
    public const float defendStamina = 2f;
    public const float rollStamina = 15f;
    public const float counterSkillStamina = 30f;

    // Attack Speed
    public const float minAttackSpeed = 0f;
    public const float defaultAttackSpeed = 1f;
    public const float maxAttackSpeed = 2f;

    // Move SPeed
    public const float minMoveSpeed = 0f;
    public const float defaultMoveSpeed = 3f;
    public const float maxMoveSpeed = 5f;

    // Critical
    public const float minCriticalChance = 0f;
    public const float defaultCriticalChance = 0f;
    public const float maxCriticalChance = 100f;

    public const float mincriticalDamage = 100f;
    public const float defaultCriticalDamage = 100f;
    #endregion

    #region Combat Constant
    // Compete
    public const float competeTime = 4f;
    public const float competeAttackTime = 1f;

    // Stagger
    public const float staggerTime = 6f;
    #endregion

    #region Monster Constant
    // Monster Disapear Time
    public const float monsterDisapearTime = 5f;
    #endregion

    #region Game Constant
    public const float noticeTime = 1.6f;
    #endregion

    #region Tag String
    public const string invincibilityTag = "Invincibility";
    public const string enemyTag = "Enemy";
    public const string playerTag = "Player";
    #endregion
}
