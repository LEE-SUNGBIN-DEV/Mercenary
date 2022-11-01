using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ======================================
//              Legacy Script
// ======================================

/*
public class SubCamera : MonoBehaviour
{
    // Private Variable
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float shakeAmount;
    private Vector3 originalPosition;
    private bool isShake;
    private float shakeTime;

    // Private Function
    private void Awake()
    {
        shakeTime = GameConstants.TIME_COMPETE;
    }
    private void OnEnable()
    {
        isShake = true;
    }

    private void OnDisable()
    {
        isShake = false;
    }

    private void Update()
    {
        if (isShake == true)
        {
            transform.position = (Random.insideUnitSphere * shakeAmount) + originalPosition;
            shakeTime -= Time.deltaTime;

            if (shakeTime <= 0)
            {
                isShake = false;
            }
        }
    }

    public Vector3 OriginalPosition
    {
        get { return originalPosition; }
        set { originalPosition = value; }
    }
}
*/