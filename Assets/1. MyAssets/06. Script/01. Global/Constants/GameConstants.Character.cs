using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class GameConstants
{
    // Inventory
    public const int CHARACTER_INVENTORY_SLOT_COUNT = 30;
    public const int CHARACTER_QUICK_SLOT_COUNT = 4;
    public const int CHARACTER_EQUIPMENT_SLOT_COUNT = 4;

    // Character Stat
    public const float CHARACTER_STAMINA_RECOVERY_PERCENTAGE = 15f;
    public const float CHARACTER_STAMINA_CONSUMPTION_RUN = 2f;
    public const float CHARACTER_STAMINA_CONSUMPTION_DEFEND = 2f;
    public const float CHARACTER_STAMINA_CONSUMPTION_ROLL = 15f;
    public const float CHARACTER_STAMINA_CONSUMPTION_COUNTER = 30f;

    public const float CHARACTER_STAT_ATTACK_SPEED_MIN = 0.1f;
    public const float CHARACTER_STAT_ATTACK_SPEED_DEFAULT = 1f;
    public const float CHARACTER_STAT_ATTACK_SPEED_MAX = 2f;

    public const float CHARACTER_STAT_MOVE_SPEED_MIN = 0.1f;
    public const float CHARACTER_STAT_MOVE_SPEED_DEFAULT = 3f;
    public const float CHARACTER_STAT_MOVE_SPEED_MAX = 5f;

    public const float CHARACTER_STAT_CRITICAL_CHANCE_MIN = 0f;
    public const float CHARACTER_STAT_CRITICAL_CHANCE_DEFAULT = 0f;
    public const float CHARACTER_STAT_CRITICAL_CHANCE_MAX = 100f;

    public const float CHARACTER_STAT_CRITICAL_DAMAGE_MIN = 100f;
    public const float CHARACTER_STAT_CRITICAL_DAMAGE_DEFAULT = 100f;
}

public enum CHARACTER_STATE
{
    // Common Character
    MOVE = 1,
    ATTACK = 2,
    SKILL = 4,
    ROLL = 5,
    HIT = 6,
    HEAVY_HIT = 7,
    STUN = 8,
    COMPETE = 9,
    DIE = 10,

    // Lancer
    LANCER_DEFENSE = 100

}

public enum CHARACTER_STATE_WEIGHT
{
    // Common Character
    MOVE = 1,
    ATTACK = 2,
    SKILL = 3,
    ROLL = 4,
    HIT = 5,
    HEAVY_HIT = 6,
    STUN = 7,
    COMPETE = 8,
    DIE = 9,

    // Lancer
    LANCER_DEFENSE = 2
}

public enum ATTACK_TYPE
{
    COMBO1,
    COMBO2,
    COMBO3,
    COMBO4,

    SMASH1,
    SMASH2,
    SMASH3,
    SMASH4,

    SKILL
}