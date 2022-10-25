using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RPlayer : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerState playerState;

    [SerializeField] private PlayerData playerData;
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private SubCamera subCamera;

    private Animator playerAnimator;
    private CharacterController characterController;
    

    protected virtual void Awake()
    {
        playerStats = new PlayerStats();
        playerInput = new PlayerInput();
        playerState = new PlayerState(this);

        playerData = GetComponent<PlayerData>();
        playerAnimator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    public abstract PLAYER_STATE DeterminePlayerState();

    #region Property
    public PlayerStats PlayerStats
    {
        get => playerStats;
    }
    public PlayerInput PlayerInput
    {
        get => playerInput;
    }
    public PlayerState PlayerState
    {
        get => playerState;
    }
    
    public PlayerData PlayerData
    {
        get => playerData;
    }
    public PlayerCamera PlayerCamera
    {
        get => playerCamera;
    }
    public SubCamera SubCamera
    {
        get => subCamera;
    }
    public Animator PlayerAnimator
    {
        get => playerAnimator;
    }
    public CharacterController CharacterController
    {
        get => characterController;
    }
    #endregion
}
