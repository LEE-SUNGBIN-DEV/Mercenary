using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;

    private UserInput playerInput;
    private CharacterStats characterStats;
    private CharacterState characterState;
    private Inventory characterInventory;

    private Animator characterAnimator;
    private CharacterController characterController;
    
    protected virtual void Awake()
    {
        characterData = GetComponent<CharacterData>();
        characterAnimator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        playerInput = new UserInput();
        characterStats = new CharacterStats(this);
        characterState = new CharacterState(this);

        characterInventory = GetComponent<Inventory>();

        CharacterStats.OnDie += Die;
    }

    protected virtual void OnEnable()
    {
        UIManager.InteractPlayer -= SetInteract;
        UIManager.InteractPlayer += SetInteract;

        Potion.onConsumePotion -= UsePotion;
        Potion.onConsumePotion += UsePotion;

        Rebirth();
    }

    public abstract CHARACTER_STATE DetermineCharacterState();

    public void SetCharacterTransform(Transform targetTransform)
    {
        gameObject.SetTransform(targetTransform);
    }
    
    public void Die(CharacterStats characterStats)
    {
    }
    public void Rebirth()
    {

    }
    public void AutoRecoverStamina()
    {
        characterStats.CurrentStamina += (characterStats.MaxStamina * GameConstants.CHARACTER_STAMINA_RECOVERY_PERCENTAGE * 0.01f * Time.deltaTime);
    }
    public void SetInteract(bool isInteract)
    {
    }

    public void UsePotion(POTION_ITEM potionType, float amount)
    {
        switch (potionType)
        {
            case POTION_ITEM.NORMAL_HP_POTION:
                {
                    characterStats.CurrentHitPoint += (characterStats.MaxHitPoint * amount * 0.01f);
                    break;
                }
            case POTION_ITEM.NORMAL_SP_POTION:
                {
                    characterStats.CurrentStamina += (characterStats.MaxStamina * amount * 0.01f);
                    break;
                }
        }
    }


    #region Animation Event
    public void SwitchCharacterState(CHARACTER_STATE targetState)
    {
        CharacterState.SwitchCharacterState(targetState);
    }
    #endregion

    #region Property
    public UserInput PlayerInput
    {
        get => playerInput;
    }
    public CharacterStats CharacterStats
    {
        get => characterStats;
    }
    public CharacterState CharacterState
    {
        get => characterState;
    }
    
    public CharacterData CharacterData
    {
        get => characterData;
    }
    public Animator CharacterAnimator
    {
        get => characterAnimator;
    }
    public CharacterController CharacterController
    {
        get => characterController;
    }
    public Inventory CharacterInventory
    {
        get => characterInventory;
    }
    #endregion
}
