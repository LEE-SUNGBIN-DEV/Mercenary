using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Player : MonoBehaviour, IPlayer
{
    #region Event
    public static event UnityAction<Player> onAttackPowerChanged;
    public static event UnityAction<Player> onDefensivePowerChanged;
    public static event UnityAction<Player> onMaxHitPointChanged;
    public static event UnityAction<Player> onCurrentHitPointChanged;
    public static event UnityAction<Player> onMaxStaminaChanged;
    public static event UnityAction<Player> onCurrentStaminaChanged;
    public static event UnityAction<Player> onCriticalChanceChanged;
    public static event UnityAction<Player> onCriticalDamageChanged;
    public static event UnityAction<Player> onAttackSpeedChanged;
    public static event UnityAction<Player> onMoveSpeedChanged;

    public static event UnityAction<Player> onDie;
    #endregion

    [SerializeField] private PlayerData playerData;
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private SubCamera subCamera;
    [SerializeField] private GameObject levelUpEffect;

    [SerializeField] private float attackPower;
    [SerializeField] private float defensivePower;

    [SerializeField] private float maxHitPoint;
    [SerializeField] private float currentHitPoint;
    [SerializeField] private float maxStamina;
    [SerializeField] private float currentStamina;
    [SerializeField] private float criticalChance;
    [SerializeField] private float criticalDamage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float moveSpeed;

    [SerializeField] private bool isMove;
    [SerializeField] private bool isAttack;
    [SerializeField] private bool isRoll;
    [SerializeField] private bool isHit;
    [SerializeField] private bool isHeavyHit;
    [SerializeField] private bool isStun;
    [SerializeField] private bool isDie;
    [SerializeField] private bool isInteract;

    [SerializeField] private bool isSpaceKeyDown;
    [SerializeField] private bool isLeftShiftKeyDown;
    [SerializeField] private bool isMouseLeftDown;
    [SerializeField] private bool isMouseLeftUp;
    [SerializeField] private bool isMouseRightDown;
    [SerializeField] private bool isMouseRightUp;
    [SerializeField] private bool isRKeyDown;

    public virtual void Awake()
    {
        PlayerData.onStrengthChanged += (PlayerData playerData) =>
        {
            AttackPower = playerData.Strength * 2;
            DefensivePower = playerData.Strength;
        };
        PlayerData.onVitalityChanged += (PlayerData playerData) =>
        {
            MaxHitPoint = playerData.Vitality * 10;
            CurrentHitPoint = playerData.Vitality * 10;

            MaxStamina = playerData.Vitality * 10;
            CurrentStamina = playerData.Vitality * 10;
        };
        PlayerData.onDexterityChanged += (PlayerData playerData) =>
        {
            AttackSpeed = GameConstant.defaultAttackSpeed + playerData.Dexterity * 0.01f;
            MoveSpeed = GameConstant.defaultMoveSpeed + playerData.Dexterity * 0.02f;
        };
        PlayerData.onLuckChanged += (PlayerData playerData) =>
        {
            CriticalChance = playerData.Luck;
            CriticalDamage = GameConstant.defaultCriticalDamage + playerData.Luck;
        };
    }

    public virtual void OnEnable()
    {
        UIManager.InteractPlayer -= SetInteract;
        UIManager.InteractPlayer += SetInteract;

        Potion.onConsumePotion -= UsePotion;
        Potion.onConsumePotion += UsePotion;

        Rebirth();
    }
    public void UsePotion(POTION_ITEM potionType, float amount)
    {
        switch(potionType)
        {
            case POTION_ITEM.NORMAL_HP_POTION:
                {
                    CurrentHitPoint += (MaxHitPoint * amount * 0.01f);
                    break;
                }
            case POTION_ITEM.NORMAL_SP_POTION:
                {
                    CurrentStamina += (MaxStamina * amount * 0.01f);
                    break;
                }
        }
    }
    public void AutoRecoverStamina()
    {
        CurrentStamina += (MaxStamina * GameConstant.recoverStaminaPercentage * 0.01f * Time.deltaTime);
    }
    public void SetInteract(bool isInteract)
    {
        InitializeAllAnimationParameter();
        IsInteract = isInteract;
    }
    
    public void GetUserInput()
    {
        IsSpaceKeyDown = Input.GetKeyDown(KeyCode.Space);
        IsMouseLeftDown = Input.GetMouseButton(0);
        IsMouseLeftUp = Input.GetMouseButtonUp(0);
        IsMouseRightDown = Input.GetMouseButton(1);
        IsMouseRightUp = Input.GetMouseButtonUp(1);
        IsLeftShiftKeyDown = Input.GetKey(KeyCode.LeftShift);
        IsRKeyDown = Input.GetKey(KeyCode.R);
    }
    public abstract void Move();
    public abstract void Attack();
    public abstract void Roll();
    public abstract void Hit();
    public abstract void HeavyHit();
    public abstract void Stun();
    public abstract void Die();
    public abstract void InitializeAllState();
    public abstract void InitializeAllAnimationParameter();
    
    public void InitializeHPAndStamina()
    {
        CurrentHitPoint = MaxHitPoint;
        CurrentStamina = MaxStamina;
    }
    
    public void Rebirth()
    {
        IsDie = false;
        CurrentStamina = MaxStamina;

        AttackPower = attackPower;
        DefensivePower = defensivePower;

        MaxHitPoint = maxHitPoint;
        CurrentHitPoint = MaxHitPoint;
        MaxStamina = maxStamina;
        CurrentStamina = MaxStamina;
        CriticalChance = criticalChance;
        CriticalDamage = criticalDamage;
        AttackSpeed = attackSpeed;
        MoveSpeed = moveSpeed;
    }

    #region Property
    public float AttackPower
    {
        get { return attackPower; }
        set
        {
            attackPower = value;
            if (attackPower < 0)
            {
                attackPower = 0;
            }
            onAttackPowerChanged(this);
        }
    }
    public float DefensivePower
    {
        get { return defensivePower; }
        set
        {
            defensivePower = value;
            if (defensivePower < 0)
            {
                defensivePower = 0;
            }
            onDefensivePowerChanged(this);
        }
    }
    public float MaxHitPoint
    {
        get { return maxHitPoint; }
        set
        {
            maxHitPoint = value;
            if (maxHitPoint <= 0)
            {
                maxHitPoint = 1;
            }
            onMaxHitPointChanged(this);
        }
    }
    public float CurrentHitPoint
    {
        get { return currentHitPoint; }
        set
        {
            currentHitPoint = value;
            if (currentHitPoint > MaxHitPoint)
            {
                currentHitPoint = MaxHitPoint;
            }

            if (currentHitPoint < 0)
            {
                currentHitPoint = 0;
                if(IsDie == false)
                {
                    onDie(this);
                    Die();
                }
            }
            onCurrentHitPointChanged(this);
        }
    }
    public float MaxStamina
    {
        get { return maxStamina; }
        set
        {
            maxStamina = value;
            if (maxStamina <= 0)
            {
                maxStamina = 1;
            }
            onMaxStaminaChanged(this);
        }
    }
    public float CurrentStamina
    {
        get { return currentStamina; }
        set
        {
            currentStamina = value;
            if (currentStamina > MaxStamina)
            {
                currentStamina = MaxStamina;
            }

            if (currentStamina < 0)
            {
                currentStamina = 0;
            }

            onCurrentStaminaChanged(this);
        }
    }
    public float AttackSpeed
    {
        get { return attackSpeed; }
        set
        {
            attackSpeed = value;

            if (attackSpeed < GameConstant.minAttackSpeed)
            {
                attackSpeed = GameConstant.minAttackSpeed;
            }

            if (attackSpeed > GameConstant.maxAttackSpeed)
            {
                attackSpeed = GameConstant.maxAttackSpeed;
            }

            onAttackSpeedChanged(this);

            if (Animator != null)
            {
                Animator.SetFloat("attackSpeed", attackSpeed);
            }
        }
    }
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set
        {
            moveSpeed = value;

            if (moveSpeed < GameConstant.minMoveSpeed)
            {
                moveSpeed = GameConstant.minMoveSpeed;
            }

            if (moveSpeed > GameConstant.maxMoveSpeed)
            {
                moveSpeed = GameConstant.maxMoveSpeed;
            }

            onMoveSpeedChanged(this);
        }
    }
    public float CriticalChance
    {
        get { return criticalChance; }
        set
        {
            criticalChance = value;
            if (criticalChance < GameConstant.minCriticalChance)
            {
                criticalChance = GameConstant.minCriticalChance;
            }

            if (criticalChance > GameConstant.maxCriticalChance)
            {
                criticalChance = GameConstant.maxCriticalChance;
            }

            onCriticalChanceChanged(this);
        }
    }
    public float CriticalDamage
    {
        get { return criticalDamage; }
        set
        {
            criticalDamage = value;
            if (criticalDamage < GameConstant.mincriticalDamage)
            {
                criticalDamage = GameConstant.mincriticalDamage;
            }
            onCriticalDamageChanged(this);
        }
    }
    // Component
    public CharacterController PlayerCharacterController { get; set; }
    public Animator Animator { get; set; }
    public PlayerData PlayerData
    {
        get { return playerData; }
        set { playerData = value; }
    }
    public PlayerCamera PlayerCamera
    {
        get { return playerCamera; }
        private set { playerCamera = value; }
    }
    public SubCamera SubCamera
    {
        get { return subCamera; }
        private set { subCamera = value; }
    }
    public GameObject LevelUpEffect
    {
        get { return levelUpEffect; }
        private set { levelUpEffect = value; }
    }

    // State
    public bool IsMove { get => isMove; set => isMove = value; }
    public bool IsAttack { get => isAttack; set => isAttack = value; }
    public bool IsRoll { get => isRoll; set => isRoll = value; }
    public bool IsHit { get => isHit; set => isHit = value; }
    public bool IsHeavyHit { get => isHeavyHit; set => isHeavyHit = value; }
    public bool IsStun { get => isStun; set => isStun = value; }
    public bool IsDie { get => isDie; set => isDie = value; }
    public bool IsInteract { get => isInteract; set => isInteract = value; }

    // Input
    public bool IsSpaceKeyDown { get => isSpaceKeyDown; set => isSpaceKeyDown = value; }
    public bool IsLeftShiftKeyDown { get => isLeftShiftKeyDown; set => isLeftShiftKeyDown = value; }
    public bool IsMouseLeftDown { get => isMouseLeftDown; set => isMouseLeftDown = value; }
    public bool IsMouseLeftUp { get => isMouseLeftUp; set => isMouseLeftUp = value; }
    public bool IsMouseRightDown { get => isMouseRightDown; set => isMouseRightDown = value; }
    public bool IsMouseRightUp { get => isMouseRightUp; set => isMouseRightUp = value; }
    public bool IsRKeyDown { get => isRKeyDown; set => isRKeyDown = value; }
    #endregion
}
