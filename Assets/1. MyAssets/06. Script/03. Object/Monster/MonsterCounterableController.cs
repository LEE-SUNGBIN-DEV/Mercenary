using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCounterableController : MonoBehaviour
{
    [SerializeField] private Monster owner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player Attack"))
        {
            PlayerAttackController playerAttack = other.GetComponent<PlayerAttackController>();
            if (playerAttack != null && playerAttack.CombatType == COMBAT_TYPE.COUNTER_SKILL)
            {
                EffectPoolManager.Instance.RequestObject(EFFECT_POOL.COMBAT_COMPETE_START, other.bounds.ClosestPoint(transform.position));
                Owner.Stun();
                Owner.MonsterMeshRenderer.material.color = Color.white;
                gameObject.SetActive(false);
            }
        }
    }

    #region Property
    public Monster Owner
    {
        get { return owner; }
        set { owner = value; }
    }
    #endregion
}
