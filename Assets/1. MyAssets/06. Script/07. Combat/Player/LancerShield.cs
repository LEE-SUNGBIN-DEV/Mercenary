using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerShield : CharacterCombatController
{
    private void Awake()
    {
        WeaponCollider = GetComponent<Collider>();
        WeaponCollider.enabled = false;
        CombatType = COMBAT_TYPE.DEFENSE;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy Attack"))
        {
            Vector3 triggerPoint = other.bounds.ClosestPoint(transform.position);

            switch (CombatType)
            {
                case COMBAT_TYPE.DEFENSE:
                    {
                        EffectPoolManager.Instance.RequestObject(EFFECT_POOL.PLAYER_DEFENCE, triggerPoint);
                        Owner.Animator.SetBool("isBreakShield", true);
                        break;
                    }

                case COMBAT_TYPE.PERFECT_DEFENSE:
                    {
                        EffectPoolManager.Instance.RequestObject(EFFECT_POOL.PLAYER_DEFENCE, triggerPoint);
                        EffectPoolManager.Instance.RequestObject(EFFECT_POOL.PLAYER_PERFECT_DEFENCE, triggerPoint);

                        Owner.Animator.SetBool("isPerfectShield", true);
                        Owner.Animator.SetBool("isBreakShield", false);
                        CallSlowMotion(0.5f, 0.5f);
                        break;
                    }
            }
            WeaponCollider.enabled = false;
        }

        if (other.CompareTag("Enemy"))
        {
            Vector3 triggerPoint = other.bounds.ClosestPoint(transform.position);
            if (CombatType == COMBAT_TYPE.COUNTER)
            {
                Enemy monster = other.GetComponentInParent<Enemy>();
                GameFunction.PlayerAttackProcess(Owner, monster, DamageRatio);

                IStunable stunableObject = monster.GetComponent<IStunable>();
                if (stunableObject != null)
                {
                    EffectPoolManager.Instance.RequestObject(EFFECT_POOL.PLAYER_COUNTER, triggerPoint);

                    stunableObject.Stun();
                    CallSlowMotion(0.2f, 0.5f);
                }
            }
        }
    }
}
