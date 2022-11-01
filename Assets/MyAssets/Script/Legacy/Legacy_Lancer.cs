using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// ======================================
//              Legacy Script
// ======================================

/*
public class Lancer : Character, IDefendable, ICompetable
{
    [SerializeField] private LancerSpear spear;
    [SerializeField] private LancerShield shield;
    [SerializeField] private LancerSpear leg;
    [SerializeField] private LancerSpear counterSkill;
    [SerializeField] private GameObject competeAttackEffect;

    [SerializeField] private bool isRun;
    [SerializeField] private bool isDefend;
    [SerializeField] private bool isCompete;
    [SerializeField] private bool isSkill;

    private float moveBlendTreeFloat; // [ IDLE - WALK - RUN ]
    private Vector3 moveDirection;

    public override void Awake()
    {
        base.Awake();
        PlayerCharacterController = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();

        moveBlendTreeFloat = 0.0f;
        moveDirection = Vector3.zero;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        EnemyCompeteAttack.onCompete -= Compete;
        EnemyCompeteAttack.onCompete += Compete;
    }

    private void OnDisable()
    {
        EnemyCompeteAttack.onCompete -= Compete;
    }

    private void Update()
    {
        GetUserInput();
        Move();
        Roll();
        CounterSkill();
        Attack();
        Defend();

        if (!(IsRun || IsAttack || IsDefend || IsSkill || IsRoll || IsHit || IsHeavyHit || IsStun || IsCompete || IsDie))
            AutoRecoverStamina();
    }

    private IEnumerator CompeteTime()
    {
        yield return new WaitForSeconds(GameConstants.TIME_COMPETE);
        shield.gameObject.SetActive(false);
        PlayerCamera.gameObject.SetActive(true);
        SubCamera.gameObject.SetActive(false);
        CompeteAttack();
    }

    public override void Move()
    {
        if (IsAttack || IsDefend || IsSkill || IsRoll || IsHit || IsHeavyHit || IsStun || IsCompete || IsDie)
            return;

        // Move Input
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        Vector3 verticalDirection = new Vector3(PlayerCamera.transform.forward.x, 0, PlayerCamera.transform.forward.z);
        Vector3 horizontalDirection = new Vector3(PlayerCamera.transform.right.x, 0, PlayerCamera.transform.right.z);

        moveDirection = (verticalDirection * moveInput.z + horizontalDirection * moveInput.x).normalized;

        IsMove = (moveDirection.magnitude != 0) ? true : false;

        // Move State
        if (IsMove)
        {
            // Run
            if (IsLeftShiftKeyDown && CurrentStamina >= GameConstants.CHARACTER_STAMINA_CONSUMPTION_RUN)
            {
                IsRun = true;
                CurrentStamina -= GameConstants.CHARACTER_STAMINA_CONSUMPTION_RUN * Time.deltaTime;
                PlayerCharacterController.Move(moveDirection * (MoveSpeed * 2) * Time.deltaTime);
                moveBlendTreeFloat = Mathf.Lerp(moveBlendTreeFloat, 2, 10f * Time.deltaTime);
            }

            // Walk
            else
            {
                IsRun = false;
                PlayerCharacterController.Move(moveDirection * MoveSpeed * Time.deltaTime);
                moveBlendTreeFloat = Mathf.Lerp(moveBlendTreeFloat, 1, 10f * Time.deltaTime);
            }

            // Character Look Direction
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), 10f * Time.deltaTime);
        }

        // Stop State
        else
        {
            IsLeftShiftKeyDown = false;
            moveBlendTreeFloat = Mathf.Lerp(moveBlendTreeFloat, 0, 10f * Time.deltaTime);
        }

        Animator.SetFloat("moveFloat", moveBlendTreeFloat);
    }
    public override void Attack()
    {
        if (IsInteract || IsDefend || IsSkill || IsRoll || IsHit || IsHeavyHit || IsStun || IsCompete || IsDie)
            return;

        // Combo Attack
        if (IsMouseLeftDown || IsMouseLeftUp)
        {
            // 방향 전환
            Vector3 lookDirection = PlayerCamera.transform.forward;
            lookDirection.y = 0f;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookDirection), 10f * Time.deltaTime);

            IsAttack = true;
            Animator.SetBool("isComboAttack", !IsMouseLeftUp);
        }

        // Smash Attack
        if (IsAttack)
        {
            if (IsMouseRightDown || IsMouseRightUp)
            {
                IsAttack = true;
                Animator.SetBool("isSmashAttack", !IsMouseRightUp);
            }
        }
    }
    public void Defend()
    {
        if (IsInteract || IsSkill || IsAttack || IsRoll || IsHit || IsHeavyHit || IsStun || IsCompete || IsDie)
            return;

        if (IsMouseRightDown)
        {
            if (!IsDefend)
            {
                shield.gameObject.SetActive(true);
            }

            gameObject.tag = GameConstants.TAG_INVINCIBILITY;
            IsDefend = true;
            Animator.SetBool("isDefense", true);
        }

        if (IsMouseRightUp && IsDefend)
        {
            Animator.SetBool("isDefense", false);
        }

        if (IsDefend)
        {
            if (IsMouseRightDown || IsMouseRightUp)
            {
                Animator.SetBool("isCounterAttack", !IsMouseRightUp);
            }
        }
    }

    public override void Roll()
    {
        if (IsInteract || IsSkill || IsRoll || IsHit || IsHeavyHit || IsStun || IsCompete || IsDie)
            return;

        if (IsSpaceKeyDown && CurrentStamina >= GameConstants.CHARACTER_STAMINA_CONSUMPTION_ROLL)
        {
            CurrentStamina -= GameConstants.CHARACTER_STAMINA_CONSUMPTION_ROLL;
            // 키보드 입력 방향으로 회피
            Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
            Vector3 verticalDirection = new Vector3(PlayerCamera.transform.forward.x, 0, PlayerCamera.transform.forward.z);
            Vector3 horizontalDirection = new Vector3(PlayerCamera.transform.right.x, 0, PlayerCamera.transform.right.z);

            moveDirection = (verticalDirection * moveInput.z + horizontalDirection * moveInput.x).normalized;
            transform.forward = (moveDirection == Vector3.zero ? transform.forward : moveDirection);

            // Set Roll State
            gameObject.tag = GameConstants.TAG_INVINCIBILITY;
            IsRoll = true;
            Animator.SetTrigger("doRoll");
        }
    }
    public void Compete(EnemyCompeteAttack competeController)
    {
        if (IsDie)
            return;

        // Effect
        EffectPoolManager.Instance.RequestObject(EFFECT_POOL.PLAYER_COMPETE_START, transform.position);

        // Initialize Previous State
        IsDefend = false;
        InitializeAllAnimationParameter();

        // Set Compete State
        IsCompete = true;
        Animator.SetTrigger("doCompete");
        
        StartCoroutine(CompeteTime());
    }
    public void CompeteAttack()
    {
        Animator.SetTrigger("doCompeteAttack");
    }
    public void CounterSkill()
    {
        if (IsInteract || IsSkill || IsRoll || IsHit || IsHeavyHit || IsStun || IsCompete || IsDie)
            return;

        if (IsRKeyDown && CurrentStamina >= GameConstants.CHARACTER_STAMINA_CONSUMPTION_COUNTER)
        {
            CurrentStamina -= GameConstants.CHARACTER_STAMINA_CONSUMPTION_COUNTER;
            // 키보드 입력 방향으로 공격
            Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
            Vector3 verticalDirection = new Vector3(PlayerCamera.transform.forward.x, 0, PlayerCamera.transform.forward.z);
            Vector3 horizontalDirection = new Vector3(PlayerCamera.transform.right.x, 0, PlayerCamera.transform.right.z);

            moveDirection = (verticalDirection * moveInput.z + horizontalDirection * moveInput.x).normalized;
            transform.forward = (moveDirection == Vector3.zero ? transform.forward : moveDirection);

            // Set Roll State
            IsSkill = true;
            Animator.SetTrigger("doCounterSkill");
        }
    }
    public override void InitializeAllState()
    {
        IsMove = false;
        IsAttack = false;
        IsDefend = false;
        IsRoll = false;

        IsHit = false;
        IsHeavyHit = false;
        IsStun = false;
        IsCompete = false;
        IsDie = false;

        IsInteract = false;

        InitializeAllCombatCollider();
        InitializeAllAnimationParameter();
    }
    public override void InitializeAllAnimationParameter()
    {
        Animator.SetBool("isComboAttack", false);
        Animator.SetBool("isSmashAttack", false);
        Animator.SetBool("isDefense", false);
        Animator.SetBool("isBreakShield", false);
        Animator.SetBool("isPerfectShield", false);
    }
    public void InitializeAllCombatCollider()
    {
        spear.gameObject.SetActive(false);
        shield.gameObject.SetActive(false);
        leg.gameObject.SetActive(false);
    }

    // 피격 처리
    public override void Hit()
    {
        if (IsRoll || IsHit || IsHeavyHit || IsStun || IsCompete || IsDie)
            return;

        IsHit = true;
        Animator.SetTrigger("doHit");
    }
    public override void HeavyHit()
    {
        if (IsRoll || IsHeavyHit || IsStun || IsCompete || IsDie)
            return;

        IsHeavyHit = true;
        Animator.SetTrigger("doHeavyHit");
    }
    public override void Stun()
    {
        if (IsRoll || IsStun || IsCompete || IsDie)
            return;

        IsStun = true;
        Animator.SetTrigger("doStun");
    }
    public override void Die()
    {
        InitializeAllState();

        IsDie = true;
        Animator.SetTrigger("doDie");
    }

    #region Animation Event Function
    // Weapon
    private void OnNormalAttack()
    {
        spear.DamageRatio = 1.0f;
        spear.CombatType = COMBAT_TYPE.NORMAL;
        spear.gameObject.SetActive(true);
    }
    private void OnSmashAttack2()
    {
        spear.DamageRatio = 2.5f;
        spear.CombatType = COMBAT_TYPE.SMASH;
        spear.gameObject.SetActive(true);
    }
    private void OnSmashAttack3()
    {
        spear.DamageRatio = 4f;
        spear.CombatType = COMBAT_TYPE.SMASH;
        spear.gameObject.SetActive(true);
    }
    private void OnSmashAttack4()
    {
        spear.DamageRatio = 3f;
        spear.CombatType = COMBAT_TYPE.SMASH;
        spear.gameObject.SetActive(true);
    }
    private void OnCounterableAttack()
    {
        counterSkill.gameObject.SetActive(true);
    }
    private void OffCounterableAttack()
    {
        counterSkill.gameObject.SetActive(false);
    }
    private void OnCounterAttack()
    {
        spear.DamageRatio = 2.0f;
        spear.CombatType = COMBAT_TYPE.COUNTER;
        spear.gameObject.SetActive(true);
    }
    private void OffWeaponCollider()
    {
        spear.DamageRatio = 1.0f;
        spear.CombatType = COMBAT_TYPE.NORMAL;
        spear.gameObject.SetActive(false);
    }
    private void OnSmashAttack1()
    {
        leg.gameObject.SetActive(true);
    }
    private void OffSmashAttack1()
    {
        leg.gameObject.SetActive(false);
    }
    private void OutAttack()
    {
        IsDefend = false;
        IsAttack = false;
        Animator.SetBool("isSmashAttack", false);
    }

    // Shield
    private void OnPerfectDefense()
    {
        shield.CombatType = COMBAT_TYPE.PERFECT_DEFENSE;
    }
    private void OffPerfectDefense()
    {
        shield.CombatType = COMBAT_TYPE.DEFENSE;
    }
    private void OnShieldCounterAttack()
    {
        shield.CombatType = COMBAT_TYPE.COUNTER;
        shield.gameObject.SetActive(true);
    }
    private void OffShieldCollider()
    {
        shield.CombatType = COMBAT_TYPE.DEFENSE;
        shield.gameObject.SetActive(false);
    }
    private void OutDefense()
    {
        shield.CombatType = COMBAT_TYPE.DEFENSE;

        shield.gameObject.SetActive(false);
        gameObject.tag = GameConstants.TAG_PLAYER;

        IsDefend = false;

        InitializeAllAnimationParameter();
    }

    // Compete
    private void OutCompete()
    {
        Animator.SetBool("isPerfectShield", false);
        gameObject.tag = GameConstants.TAG_PLAYER;
        IsCompete = false;
    }

    // Action
    private void InRoll()
    {
        IsMove = false;
        IsAttack = false;
        IsDefend = false;

        InitializeAllCombatCollider();
        InitializeAllAnimationParameter();
    }
    private void OutRoll()
    {
        gameObject.tag = GameConstants.TAG_PLAYER;
        IsRoll = false;
    }
    private void InHit()
    {
        IsMove = false;
        IsAttack = false;
        IsDefend = false;

        InitializeAllCombatCollider();
        InitializeAllAnimationParameter();
    }
    private void OutHit()
    {
        IsHit = false;
    }

    private void InHeavyHit()
    {
        IsMove = false;
        IsAttack = false;
        IsDefend = false;
        IsHit = false;

        InitializeAllCombatCollider();
        InitializeAllAnimationParameter();
    }
    private void OutHeavyHit()
    {
        IsHeavyHit = false;
    }

    private void InStun()
    {
        IsMove = false;
        IsAttack = false;
        IsDefend = false;
        IsHit = false;
        IsHeavyHit = false;

        InitializeAllCombatCollider();
        InitializeAllAnimationParameter();
    }
    private void OutStun()
    {
        IsStun = false;
    }
    private void InCounterSkill()
    {
        IsMove = false;
        IsAttack = false;
        IsDefend = false;

        InitializeAllCombatCollider();
        InitializeAllAnimationParameter();
    }
    private void OutCounterSkill()
    {
        IsSkill = false;
    }
    // Effect
    private void OnCompeteAttackEffect()
    {
        competeAttackEffect.SetActive(true);
    }
    #endregion

    #region Property
    public bool IsRun { get => isRun; set => isRun = value; }
    public bool IsDefend { get => isDefend; set => isDefend = value; }
    public bool IsCompete { get => isCompete; set => isCompete = value; }
    public bool IsSkill { get => isSkill; set => isSkill = value; }
    #endregion
}
*/