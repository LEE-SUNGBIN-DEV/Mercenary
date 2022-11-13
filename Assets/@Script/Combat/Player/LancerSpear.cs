using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerSpear : CharacterCombatController
{
    private void Awake()
    {
        WeaponCollider = GetComponent<Collider>();
        WeaponCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Vector3 triggerPoint = other.bounds.ClosestPoint(transform.position);

            Enemy monster = other.GetComponentInParent<Enemy>();
            Functions.PlayerDamageProcess(Owner, monster, DamageRatio);

            switch (CombatType)
            {
                case COMBAT_TYPE.NORMAL:
                case COMBAT_TYPE.COUNTER_SKILL:
                    {
                        IHitable hitableObject = other.GetComponentInParent<IHitable>();
                        if (hitableObject != null)
                        {
                            Managers.ObjectPoolManager.RequestObject(Constants.RESOURCE_NAME_EFFECT_PLAYER_ATTACK, triggerPoint);

                            hitableObject.Hit();
                        }

                        break;
                    }
                case COMBAT_TYPE.SMASH:
                    {
                        IHeavyHitable heavyHitableObject = other.GetComponentInParent<IHeavyHitable>();
                        if (heavyHitableObject != null)
                        {
                            Managers.ObjectPoolManager.RequestObject(Constants.RESOURCE_NAME_EFFECT_PLAYER_SMASH, triggerPoint);

                            heavyHitableObject.HeavyHit();
                            CallSlowMotion(0.5f, 0.5f);
                        }

                        break;
                    }

                case COMBAT_TYPE.STUN:
                    {
                        IStunable stunableObject = other.GetComponentInParent<IStunable>();
                        if (stunableObject != null)
                        {
                            Managers.ObjectPoolManager.RequestObject(Constants.RESOURCE_NAME_EFFECT_PLAYER_SMASH, triggerPoint);

                            stunableObject.Stun();
                            CallSlowMotion(0.5f, 0.5f);
                        }
                        break;
                    }

                case COMBAT_TYPE.COUNTER:
                    {
                        IStunable stunableObject = other.GetComponentInParent<IStunable>();
                        if (stunableObject != null)
                        {
                            Managers.ObjectPoolManager.RequestObject(Constants.RESOURCE_NAME_EFFECT_PLAYER_COUNTER, triggerPoint);

                            stunableObject.Stun();
                            CallSlowMotion(0.2f, 0.5f);
                        }
                        break;
                    }
            }
        }
    }
}
