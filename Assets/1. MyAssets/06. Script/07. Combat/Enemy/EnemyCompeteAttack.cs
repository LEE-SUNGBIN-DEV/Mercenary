using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCompeteAttack : EnemyCombatController
{
    #region Event
    public static event UnityAction<EnemyCompeteAttack> onCompete;
    #endregion

    [SerializeField] private Transform playerCompetePoint;
    [SerializeField] private Transform playerSubCameraPoint;
    [SerializeField] private float cooldown;
    private bool isReady;

    private void Awake()
    {
        isReady = true;
        CombatType = COMBAT_TYPE.COMPETE;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IHeavyHitable heavyHitableObject = other.GetComponent<IHeavyHitable>();

            switch (CombatType)
            {
                case COMBAT_TYPE.COMPETE:
                    {
                        heavyHitableObject.HeavyHit();
                        break;
                    }
            }
        }

        if (other.CompareTag("Player Defend"))
        {
            LancerShield defendObject = other.GetComponent<LancerShield>();

            if (isReady == true && defendObject.CombatType == COMBAT_TYPE.PERFECT_DEFENSE)
            {
                Vector3 triggerPoint = other.bounds.ClosestPoint(transform.position);
                EffectPoolManager.Instance.RequestObject(EFFECT_POOL.COMBAT_COMPETE_START, triggerPoint);
                EffectPoolManager.Instance.RequestObject(EFFECT_POOL.COMBAT_COMPETE_PROGRESS, triggerPoint);

                CompeteCooldown();
                onCompete(this);
            }
        }
    }
    private IEnumerator CompeteCooldown()
    {
        isReady = false;
        yield return new WaitForSeconds(cooldown);
        isReady = true;
    }

    #region Property
    public Transform PlayerCompetePoint
    {
        get { return playerCompetePoint; }
        set { playerCompetePoint = value; }
    }
    public Transform PlayerSubCameraPoint
    {
        get { return playerSubCameraPoint; }
        set { playerSubCameraPoint = value; }
    }
    #endregion
}
