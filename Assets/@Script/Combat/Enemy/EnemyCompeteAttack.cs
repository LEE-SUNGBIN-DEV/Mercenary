using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCompeteAttack : EnemyCombatController
{
    [SerializeField] private Transform playerCompetePoint;
    [SerializeField] private Transform directingCameraPoint;
    [SerializeField] private float cooldown;
    private bool isReady;
    private ICompetable competableEnemy;

    private void Awake()
    {
        CombatType = COMBAT_TYPE.COMPETE;
        isReady = true;
        competableEnemy = Owner as ICompetable;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IHeavyHitable heavyHitableObject = other.GetComponent<IHeavyHitable>();

            switch (CombatType)
            {
                case COMBAT_TYPE.COMPETE:
                    {
                        heavyHitableObject.HeavyHit();
                        break;
                    }
            }
        }

        if (other.CompareTag("Player Defense"))
        {
            CharacterCombatController combatController = other.GetComponent<CharacterCombatController>();

            if (isReady == true && combatController.CombatType == COMBAT_TYPE.PARRYING)
            {
                Vector3 triggerPoint = other.bounds.ClosestPoint(transform.position);
                Managers.ObjectPoolManager.RequestObject(Constants.RESOURCE_NAME_EFFECT_COMPETE_START, triggerPoint);
                Managers.ObjectPoolManager.RequestObject(Constants.RESOURCE_NAME_EFFECT_COMPETE_PROGRESS, triggerPoint);

                Compete(combatController);
            }
        }
    }
    public void Compete(CharacterCombatController combatController)
    {
        StartCoroutine(CoCompeteCooldown());
        StartCoroutine(CoCompete(combatController));
    }

    public IEnumerator CoCompeteCooldown()
    {
        isReady = false;
        yield return new WaitForSecondsRealtime(cooldown);
        isReady = true;
    }

    public IEnumerator CoCompete(CharacterCombatController combatController)
    {
        ICompetable competableCharacter = combatController.Owner as ICompetable;
        competableCharacter?.Compete();
        competableEnemy?.Compete();

        Functions.SetCharacterTransform(combatController.Owner, playerCompetePoint);
        Managers.GameManager.PlayerCamera.SetCameraTransform(playerCompetePoint);
        Managers.GameManager.DirectingCamera.SetCameraTransform(directingCameraPoint);

        Managers.GameManager.DirectingCamera.OriginalPosition = directingCameraPoint.position;
        Managers.GameManager.PlayerCamera.gameObject.SetActive(false);
        Managers.GameManager.DirectingCamera.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(Constants.TIME_COMPETE);
        Managers.GameManager.DirectingCamera.gameObject.SetActive(false);
        Managers.GameManager.PlayerCamera.gameObject.SetActive(true);
    }
}
