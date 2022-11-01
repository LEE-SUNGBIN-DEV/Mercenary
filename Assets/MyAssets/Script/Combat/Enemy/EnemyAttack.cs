using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyCombatController
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHitProcess(other);
        }
    }
}
