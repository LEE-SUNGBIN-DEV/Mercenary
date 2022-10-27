using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackController : BaseCombatController
{
    [SerializeField] private Monster owner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character player = other.GetComponent<Character>();
            if (player != null)
            {
                GameFunction.MonsterAttackProcess(Owner, player, DamageRatio);

                switch (CombatType)
                {
                    case COMBAT_TYPE.NORMAL:
                        {
                            player.Hit();
                            break;
                        }

                    case COMBAT_TYPE.SMASH:
                        {
                            player.HeavyHit();
                            break;
                        }

                    case COMBAT_TYPE.STUN:
                        {
                            player.Stun();
                            break;
                        }
                }
            }
        }
    }

    #region Property
    public Monster Owner
    {
        get { return owner; }
        set
        {
            if (value is Monster)
            {
                owner = value;
            }
        }
    }
    #endregion
}
