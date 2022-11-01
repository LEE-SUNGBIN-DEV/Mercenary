using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum COMBAT_TYPE
{
    NORMAL,
    SMASH,
    STUN,
    COUNTER,
    COMPETE,

    DEFENSE,
    PARRYING,
    COUNTER_SKILL,

    COUNTERABLE
}

public class BaseCombatController : MonoBehaviour
{
    [SerializeField] private COMBAT_TYPE combatType;
    [SerializeField] private float damageRatio;

    
    #region Property
    public COMBAT_TYPE CombatType
    {
        get { return combatType; }
        set { combatType = value; }
    }
    public float DamageRatio
    {
        get { return damageRatio; }
        set { damageRatio = value; }
    }
    #endregion
}
