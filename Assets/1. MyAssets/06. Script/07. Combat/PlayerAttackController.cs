using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : BaseCombatController
{
    // Private Function
    [SerializeField] private Character owner;
    private Collider attachedCollider;

    private void Awake()
    {
        AttachedCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Vector3 triggerPoint = other.bounds.ClosestPoint(transform.position);

            Monster monster = other.GetComponentInParent<Monster>();
            GameFunction.PlayerAttackProcess(Owner, monster, DamageRatio);

            switch (CombatType)
            {
                case COMBAT_TYPE.NORMAL:
                case COMBAT_TYPE.COUNTER_SKILL:
                    {
                        IHitable hitableObject = other.GetComponentInParent<IHitable>();
                        if (hitableObject != null)
                        {
                            EffectPoolManager.Instance.RequestObject(EFFECT_POOL.PLAYER_ATTACK, triggerPoint);

                            hitableObject.Hit();
                        }

                        break;
                    }
                case COMBAT_TYPE.SMASH:
                    {
                        IHeavyHitable heavyHitableObject = other.GetComponentInParent<IHeavyHitable>();
                        if (heavyHitableObject != null)
                        {
                            EffectPoolManager.Instance.RequestObject(EFFECT_POOL.PLAYER_SMASH, triggerPoint);

                            heavyHitableObject.HeavyHit();
                            StartCoroutine(SlowTime(0.5f));
                        }

                        break;
                    }

                case COMBAT_TYPE.STUN:
                    {
                        IStunable stunableObject = other.GetComponentInParent<IStunable>();
                        if (stunableObject != null)
                        {
                            EffectPoolManager.Instance.RequestObject(EFFECT_POOL.PLAYER_SMASH, triggerPoint);

                            stunableObject.Stun();
                            StartCoroutine(SlowTime(0.5f));
                        }
                        break;
                    }

                case COMBAT_TYPE.COUNTER:
                    {
                        IStunable stunableObject = other.GetComponentInParent<IStunable>();
                        if (stunableObject != null)
                        {
                            EffectPoolManager.Instance.RequestObject(EFFECT_POOL.PLAYER_COUNTER, triggerPoint);

                            stunableObject.Stun();
                            StartCoroutine(SlowTime(0.2f));
                        }
                        break;
                    }
            }
        }
    }

    #region Property
    public Character Owner
    {
        get { return owner; }
        private set { owner = value; }
    }
    public Collider AttachedCollider
    {
        get { return attachedCollider; }
        set { attachedCollider = value; }
    }
    #endregion
}
