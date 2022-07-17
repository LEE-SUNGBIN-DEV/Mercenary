using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackDragonFireBall : MonsterSkill
{
    [SerializeField] private float minRange;
    [SerializeField] private GameObject muzzle;

    private void Awake()
    {
        IsRotate = false;
        IsReady = true;
        Owner = GetComponent<BlackDragon>();
    }
    public override bool CheckCondition(float targetDistance)
    {
        return (IsReady && (targetDistance <= MaxRange) && (targetDistance >= minRange));
    }
    public override void ActiveSkill()
    {
        Owner.StopTrace();
        StartCoroutine(WaitForRotate());
        Owner.MonsterAnimator.SetTrigger("doFireBall");
        StartCoroutine(SkillCooldown());
    }

    #region Animation Event Function
    public void OnFireBall()
    {
        GameObject fireBall = EffectPoolManager.Instance.RequestObject(EFFECT_POOL.BLACK_DRAGON_FIRE_BALL, Muzzle.transform.position);

        MonsterProjectile projectile = fireBall.GetComponent<MonsterProjectile>();
        projectile.Owner = GetComponent<Monster>();
        projectile.transform.forward = transform.forward;
    }
    #endregion

    #region Property
    private GameObject Muzzle
    {
        get { return muzzle; }
        set { muzzle = value; }
    }
    #endregion
}
