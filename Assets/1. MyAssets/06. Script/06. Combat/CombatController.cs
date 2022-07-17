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

    DEFENCE,
    PERFECT_DEFENCE,
    COUNTER_SKILL,

    COUNTERABLE
}

public class CombatController : MonoBehaviour
{
    [SerializeField] private COMBAT_TYPE combatType;
    [SerializeField] private float damageRatio;

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public IEnumerator SlowTime(float _time)
    {
        Time.timeScale = _time;
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1f;
    }

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
