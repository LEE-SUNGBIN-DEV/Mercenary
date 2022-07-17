using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRangeSkill : MonsterSkill
{
    [SerializeField] private GameObject muzzle;
    [SerializeField] private EFFECT_POOL objectKey;
    private void Awake()
    {
        IsRotate = false;
        IsReady = true;
    }

    public override void ActiveSkill()
    {
        Owner.StopTrace();
        StartCoroutine(WaitForRotate());
        Owner.MonsterAnimator.SetTrigger("doAttack");
        StartCoroutine(SkillCooldown());
    }

    #region Animation Event Function
    public void OnProjectile()
    {
        GameObject projectile = EffectPoolManager.Instance.RequestObject(objectKey);
        projectile.transform.position = muzzle.transform.position;

        MonsterProjectile monsterProjectile = projectile.GetComponent<MonsterProjectile>();
        monsterProjectile.Owner = GetComponent<Monster>();
        monsterProjectile.transform.forward = transform.forward;
    }
    #endregion
}
