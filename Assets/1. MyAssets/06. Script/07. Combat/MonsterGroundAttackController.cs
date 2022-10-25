using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGroundAttackController : CombatController
{
    [SerializeField] private Monster owner;
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
            Player player = other.GetComponent<Player>();
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

    public void PlaySFX(string _string)
    {
        AudioManager.Instance.PlaySFX(_string);
    }

    #region Property
    public Monster Owner
    {
        get { return owner; }
        set
        {
            if(value is Monster)
            {
                owner = value;
            }
        }
    }
    #endregion
}
