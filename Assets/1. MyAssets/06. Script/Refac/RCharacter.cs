using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RCharacter : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private CharacterCamera characterCamera;
    [SerializeField] private SubCamera subCamera;

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private CharacterStats characterStats;
    [SerializeField] private CharacterState characterState;

    private Animator characterAnimator;
    private CharacterController characterController;
    
    protected virtual void Awake()
    {
        characterData = GetComponent<CharacterData>();
        characterAnimator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        playerInput = new PlayerInput();
        characterStats = new CharacterStats(this);
        characterState = new CharacterState(this);
    }

    public abstract CHARACTER_STATE DetermineCharacterState();

    #region Animation Event
    public void SwitchCharacterState(CHARACTER_STATE targetState)
    {
        CharacterState.SwitchCharacterState(targetState);
    }
    public void OnTargetCollider(Collider targetCollider)
    {
        targetCollider.enabled = true;
    }
    public void OffTargetCollider(Collider targetCollider)
    {
        targetCollider.enabled = false;
    }
    #endregion

    #region Property
    public PlayerInput PlayerInput
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
    public CharacterCamera CharacterCamera
    {
        get => characterCamera;
    }
    public SubCamera SubCamera
    {
        get => subCamera;
    }
    public Animator CharacterAnimator
    {
        get => characterAnimator;
    }
    public CharacterController CharacterController
    {
        get => characterController;
    }
    #endregion
}
