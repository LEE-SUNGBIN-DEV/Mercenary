using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeSkill : EnemySkill
{
    [SerializeField] private Collider attackCollider;

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
    public void OnAttackCollider()
    {
        if(attackCollider != null)
        {
            attackCollider.enabled = true;
        }
    }
    public void OffAttackCollider()
    {
        if(attackCollider != null)
        {
            attackCollider.enabled = false;
        }
    }
    #endregion
}
