using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLightningStrike : EnemyAttack
{
    private Collider attachedCollider;

    private void Awake()
    {
        AttachedCollider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StartCoroutine(OnLightning());
    }

    private IEnumerator OnLightning()
    {
        Managers.AudioManager.PlaySFX("Lightning");
        yield return new WaitForSeconds(1.0f);
        AttachedCollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        AttachedCollider.enabled = false;
    }

    #region Property
    public Collider AttachedCollider
    {
        get { return attachedCollider; }
        private set { attachedCollider = value; }
    }
    #endregion
}
