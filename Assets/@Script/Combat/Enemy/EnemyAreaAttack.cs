using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAreaAttack : EnemyCombatController
{
    [SerializeField] private float dotTime;
    [SerializeField] private string[] audioString;
    private bool isRange;

    private void OnEnable()
    {
        for (int i = 0; i < audioString.Length; ++i)
        {
            PlaySFX(audioString[i]);
        }
        isRange = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isRange = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && isRange == true)
        {
            PlayerHitProcess(other);
            StartCoroutine(DotDamageInterval());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isRange = false;
        }
    }

    IEnumerator DotDamageInterval()
    {
        isRange = false;
        yield return new WaitForSeconds(dotTime);
        isRange = true;
    }

    public void PlaySFX(string sfxName)
    {
        Managers.AudioManager.PlaySFX(sfxName);
    }
}
