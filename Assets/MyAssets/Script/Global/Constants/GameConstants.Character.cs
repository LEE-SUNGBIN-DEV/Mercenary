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