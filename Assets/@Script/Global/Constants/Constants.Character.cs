using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Constants
{
    // Inventory
    public static readonly int MAX_INVENTORY_SLOT_NUMBER = 30;
    public static readonly int MAX_QUICK_SLOT_NUMBER = 4;
    public static readonly int MAX_EQUIPMENT_SLOT_NUMBER = 4;

    // Character
    public static readonly float CHARACTER_STAMINA_AUTO_RECOVERY = 15f;
    public static readonly float CHARACTER_STAMINA_CONSUMPTION_RUN = 2f;
    public static readonly float CHARACTER_STAMINA_CONSUMPTION_DEFEND = 2f;
    public static readonly float CHARACTER_STAMINA_CONSUMPTION_ROLL = 15f;
    public static readonly float CHARACTER_STAMINA_CONSUMPTION_COUNTER = 30f;

    // Character Stat
    public static readonly float CHARACTER_STAT_ATTACK_SPEED_MIN = 0.1f;
    public static readonly float CHARACTER_STAT_ATTACK_SPEED_DEFAULT = 1f;
    public static readonly float CHARACTER_STAT_ATTACK_SPEED_MAX = 2f;

    public static readonly float CHARACTER_STAT_MOVE_SPEED_MIN = 0.1f;
    public static readonly float CHARACTER_STAT_MOVE_SPEED_DEFAULT = 3f;
    public static readonly float CHARACTER_STAT_MOVE_SPEED_MAX = 5f;

    public static readonly float CHARACTER_STAT_CRITICAL_CHANCE_MIN = 0f;
    public static readonly float CHARACTER_STAT_CRITICAL_CHANCE_DEFAULT = 0f;
    public static readonly float CHARACTER_STAT_CRITICAL_CHANCE_MAX = 100f;

    public static readonly float CHARACTER_STAT_CRITICAL_DAMAGE_MIN = 100f;
    public static readonly float CHARACTER_STAT_CRITICAL_DAMAGE_DEFAULT = 100f;

    // Character Data
    public static readonly int CHARACTER_DATA_DEFALUT_LEVEL = 1;
    public static readonly int CHARACTER_DATA_MAX_LEVEL = 30;
    public static readonly float CHARACTER_DATA_DEFALUT_EXPERIENCE = 0f;

    public static readonly int CHARACTER_DATA_DEFALUT_STATPOINT = 0;
    public static readonly int CHARACTER_DATA_DEFALUT_STRENGTH = 10;
    public static readonly int CHARACTER_DATA_DEFALUT_VITALITY = 10;
    public static readonly int CHARACTER_DATA_DEFALUT_DEXTERITY = 10;
    public static readonly int CHARACTER_DATA_DEFALUT_LUCK = 10;

}