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
                        Managers.ObjectPoolManager.RequestObject(Constants.RESOURCE_NAME_EFFECT_LANCER_DEFENSE, triggerPoint);
                        Owner.CharacterAnimator.SetBool("isBreakShield", true);
                        break;
                    }

                case COMBAT_TYPE.PARRYING:
                    {
                        Managers.ObjectPoolManager.RequestObject(Constants.RESOURCE_NAME_EFFECT_LANCER_DEFENSE, triggerPoint);
                        Managers.ObjectPoolManager.RequestObject(Constants.RESOURCE_NAME_EFFECT_LANCER_PERFECT_DEFENSE, triggerPoint);

                        Owner.CharacterAnimator.SetBool("isPerfectShield", true);
                        Owner.CharacterAnimator.SetBool("isBreakShield", false);
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
                Enemy enemy = other.GetComponentInParent<Enemy>();
                Functions.PlayerDamageProcess(Owner, enemy, DamageRatio);

                IStunable stunableObject = enemy.GetComponent<IStunable>();
                if (stunableObject != null)
                {
                    Managers.ObjectPoolManager.RequestObject(Constants.RESOURCE_NAME_EFFECT_PLAYER_COUNTER, triggerPoint);

                    stunableObject.Stun();
                    CallSlowMotion(0.2f, 0.5f);
                }
            }
        }
    }
}
