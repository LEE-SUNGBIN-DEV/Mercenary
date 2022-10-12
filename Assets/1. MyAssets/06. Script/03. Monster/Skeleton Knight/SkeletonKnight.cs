using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum SKELETON_KNIGHT_SKILL
{
    VERTICAL_SLASH,
    HORIZONTAL_SLASH,

    SIZE
}
public class SkeletonKnight : Monster, ICompetable
{
    private Dictionary<int, MonsterSkill> skillDictionary;
    private SkeletonKnightVerticalSlash verticalSlash;
    private SkeletonKnightHorizontalSlash horizontalSlash;

    public override void Awake()
    {
        base.Awake();

        verticalSlash = GetComponent<SkeletonKnightVerticalSlash>();
        horizontalSlash = GetComponent<SkeletonKnightHorizontalSlash>();

        skillDictionary = new Dictionary<int, MonsterSkill>()
        {
            {0, verticalSlash},
            {1, horizontalSlash}
        };
    }

    public override void OnEnable()
    {
        base.OnEnable();
        MonsterCompeteController.onCompete -= Compete;
        MonsterCompeteController.onCompete += Compete;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        MonsterCompeteController.onCompete -= Compete;
    }

    private void Update()
    {
        if (Target == null || IsAttack || IsStun || IsCompete || IsSpawn || IsDie)
            return;

        Move();
        Attack();
    }

    #region Override Function
    public override void Attack()
    {
        int randomNumber = Random.Range(0, (int)SKELETON_KNIGHT_SKILL.SIZE);

        if (skillDictionary[randomNumber].CheckCondition(DistanceFromTarget))
        {
            skillDictionary[randomNumber].ActiveSkill();
        }
    }

    public override void Hit()
    {
    }

    public override void HeavyHit()
    {
        if (IsStun || IsCompete || IsDie)
            return;

        IsHeavyHit = true;
        MonsterAnimator.SetTrigger("doHeavyHit");
    }

    public override void Stun()
    {
        if (IsStun || IsCompete || IsDie)
            return;

        // Initialize Previous State
        IsMove = false;
        IsAttack = false;
        IsHeavyHit = false;

        MonsterAnimator.SetBool("isMove", false);

        // Stun State
        IsStun = true;
        MonsterAnimator.SetBool("isStun", true);

        StartCoroutine(StunTime());
    }
    public override void Die()
    {
        InitializeAllState();

        // Die State
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
        IsCompete = false;

        MonsterAnimator.SetBool("isMove", false);
        MonsterAnimator.SetBool("isStun", false);
    }
    #endregion

    public IEnumerator StunTime(float time = 4f)
    {
        yield return new WaitForSeconds(time);

        IsStun = false;
        MonsterAnimator.SetBool("isStun", false);
    }

    public void Compete(MonsterCompeteController competeController)
    {
        if (IsCompete || IsDie)
            return;

        // Initialize Previous State
        verticalSlash.OffVerticalSlashCollider();

        InitializeAllState();

        // Compete State
        IsCompete = true;
        MonsterAnimator.SetTrigger("doCompete");

        StartCoroutine(CompeteTime());
    }
    public IEnumerator CompeteTime()
    {
        yield return new WaitForSeconds(GameConstant.competeTime);
        MonsterAnimator.SetTrigger("doCompeteAttack");

        yield return new WaitForSeconds(GameConstant.competeAttackTime);
        CurrentHitPoint -= (MaxHitPoint * 0.1f);
        IsCompete = false;
        Stagger();
    }
    public void Stagger()
    {
        if (IsStun || IsCompete || IsDie)
            return;

        // Initialize Previous State
        IsMove = false;
        IsAttack = false;
        IsHeavyHit = false;

        MonsterAnimator.SetBool("isMove", false);

        // Stun State
        IsStun = true;
        MonsterAnimator.SetBool("isStun", true);

        StartCoroutine(StunTime(GameConstant.staggerTime));
    }
    #region Animation Event Function
    public void OutCompete()
    {
        IsMove = false;
        IsAttack = false;
        IsHit = false;
        IsHeavyHit = false;
        IsStun = false;
        IsCompete = false;
    }
    #endregion

    #region Property
    public bool IsCompete { get; set; }
    #endregion
}