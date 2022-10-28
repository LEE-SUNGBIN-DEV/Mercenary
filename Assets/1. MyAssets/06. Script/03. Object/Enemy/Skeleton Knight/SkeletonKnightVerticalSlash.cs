using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonKnightVerticalSlash : EnemySkill
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
        Owner.MonsterAnimator.SetTrigger("doAttack1");
        StartCoroutine(SkillCooldown());
    }

    #region Animation Event Function
    public void OnVerticalSlashCollider()
    {
        if (attackCollider != null)
        {
            attackCollider.enabled = true;
        }
    }
    public void OffVerticalSlashCollider()
    {
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
        }
    }
    #endregion
}
