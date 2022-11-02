using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackDragonLandBreath : EnemySkill
{
    [SerializeField] private GameObject attackEffect;

    private void Awake()
    {
        IsRotate = false;
        IsReady = true;
        Owner = GetComponent<BlackDragon>();
    }

    public override void ActiveSkill()
    {
        Owner.StopTrace();
        StartCoroutine(WaitForRotate());
        Owner.MonsterAnimator.SetTrigger("doLandBreath");
        StartCoroutine(SkillCooldown());
    }

    #region Animation Event Function
    public void OnLandBreath()
    {
        if (AttackEffect != null)
            AttackEffect.SetActive(true);
    }

    public void OffLandBreath()
    {
        if (AttackEffect != null)
            AttackEffect.SetActive(false);
    }
    #endregion

    #region Property
    public GameObject AttackEffect
    {
        get { return attackEffect; }
        private set { attackEffect = value; }
    }
    #endregion
}
