using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalMonster : Monster
{
    private Dictionary<int, MonsterSkill> skillDictionary;
    private MonsterSkill[] monsterSkillArray;

    public override void Awake()
    {
        base.Awake();
        monsterSkillArray = GetComponents<MonsterSkill>();

        skillDictionary = new Dictionary<int, MonsterSkill>();
        for (int i = 0; i < monsterSkillArray.Length; ++i)
        {
            skillDictionary.Add(i, monsterSkillArray[i]);
        }
    }

    private void Update()
    {
        if (Target == null || IsAttack || IsHit || IsSpawn || IsDie)
            return;

        Move();
        Attack();
    }

    #region Override Function
    public override void Attack()
    {
        int randomNumber = Random.Range(0, monsterSkillArray.Length);

        if (skillDictionary[randomNumber].CheckCondition(DistanceFromTarget))
        {
            skillDictionary[randomNumber].ActiveSkill();
        }
    }
   
    public override void Hit()
    {
        if (IsStun || IsDie)
            return;

        IsHit = true;
        MonsterAnimator.SetTrigger("doHit");
    }

    public override void HeavyHit()
    {
        if (IsStun || IsDie)
            return;
        IsHit = true;
        MonsterAnimator.SetTrigger("doHit");
    }

    public override void Stun()
    {
        if (IsDie)
            return;
        IsHit = true;
        MonsterAnimator.SetTrigger("doHit");
    }

    public override void Die()
    {
        InitializeAllState();

        IsDie = true;
        MonsterAnimator.SetTrigger("doDie");

        StartCoroutine(WaitForDisapear(GameConstant.monsterDisapearTime));
    }
    public override void InitializeAllState()
    {
        // Initialize Previous State
        IsMove = false;
        IsAttack = false;
        IsHit = false;
        IsHeavyHit = false;
        IsStun = false;

        MonsterAnimator.SetBool("isMove", false);
    }
    #endregion
}
