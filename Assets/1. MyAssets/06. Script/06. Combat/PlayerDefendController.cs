using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefendController : CombatController
{
    [SerializeField] private Player owner;

    private void Awake()
    {
        CombatType = COMBAT_TYPE.DEFENCE;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy Attack"))
        {
            Vector3 triggerPoint = other.bounds.ClosestPoint(transform.position);

            switch (CombatType)
            {
                case COMBAT_TYPE.DEFENCE:
                    {
                        EffectPoolManager.Instance.RequestObject(EFFECT_POOL.PLAYER_DEFENCE, triggerPoint);
                        AudioManager.Instance.PlaySFX("Player Defence");

                        Owner.Animator.SetBool("isBreakShield", true);
                        break;
                    }

                case COMBAT_TYPE.PERFECT_DEFENCE:
                    {
                        EffectPoolManager.Instance.RequestObject(EFFECT_POOL.PLAYER_DEFENCE, triggerPoint);
                        EffectPoolManager.Instance.RequestObject(EFFECT_POOL.PLAYER_PERFECT_DEFENCE, triggerPoint);
                        AudioManager.Instance.PlaySFX("Player Perfect Defence");

                        Owner.Animator.SetBool("isPerfectShield", true);
                        Owner.Animator.SetBool("isBreakShield", false);
                        StartCoroutine(SlowTime(0.5f));
                        break;
                    }
            }
            gameObject.SetActive(false);
        }

        if (other.CompareTag("Enemy"))
        {
            Vector3 triggerPoint = other.bounds.ClosestPoint(transform.position);
            if (CombatType == COMBAT_TYPE.COUNTER)
            {
                Monster monster = other.GetComponentInParent<Monster>();
                GameFunction.PlayerAttackProcess(Owner, monster, DamageRatio);

                IStunable stunableObject = monster.GetComponent<IStunable>();
                if (stunableObject != null)
                {
                    EffectPoolManager.Instance.RequestObject(EFFECT_POOL.PLAYER_COUNTER, triggerPoint);

                    stunableObject.Stun();
                    StartCoroutine(SlowTime(0.2f));
                }
            }
        }
    }

    #region Property
    public Player Owner
    {
        get { return owner; }
        private set { owner = value; }
    }
    #endregion
}
