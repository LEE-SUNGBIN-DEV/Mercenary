using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackDragonDoubleAttack : EnemySkill
{
    [SerializeField] private GameObject attackEffect1;
    [SerializeField] private GameObject attackEffect2;
    [SerializeField] private Collider counterableCollider;

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
        Owner.MonsterAnimator.SetTrigger("doDoubleAttack");
        StartCoroutine(SkillCooldown());
    }

    #region Animation Event Function
    public void OnDoubleAttack1()
    {
        if (AttackEffect1 != null)
            AttackEffect1.SetActive(true);
    }

    public void OffDoubleAttack1()
    {
        if (AttackEffect1 != null)
            AttackEffect1.SetActive(false);
    }
    public void OnDoubleAttack2()
    {
        if (AttackEffect2 != null)
            AttackEffect2.SetActive(true);
    }

    public void OffDoubleAttack2()
    {
        if (AttackEffect2 != null)
            AttackEffect2.SetActive(false);
    }
    public void OnCounterableState()
    {
        Owner.MonsterMeshRenderer.material.color = Color.red;
        counterableCollider.gameObject.SetActive(true);
    }
    public void OffCounterableState()
    {
        Owner.MonsterMeshRenderer.material.color = Color.white;
        counterableCollider.gameObject.SetActive(false);
    }
    #endregion

    #region Property
    public GameObject AttackEffect1
    {
        get { return attackEffect1; }
        private set { attackEffect1 = value; }
    }
    public GameObject AttackEffect2
    {
        get { return attackEffect2; }
        private set { attackEffect2 = value; }
    }
    #endregion
}
