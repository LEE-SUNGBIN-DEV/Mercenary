using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombatController : BaseCombatController
{
    private Collider weaponCollider;
    private Character owner;
    private IEnumerator slowMotionCoroutine;

    private void Awake()
    {
        WeaponCollider = GetComponent<Collider>();
        WeaponCollider.enabled = false;
        slowMotionCoroutine = null;
    }

    private void OnDisable()
    {
        if (slowMotionCoroutine != null)
        {
            StopCoroutine(slowMotionCoroutine);
            Time.timeScale = 1f;
        } 
    }

    public void CallSlowMotion(float timeScale, float duration)
    {
        if (slowMotionCoroutine != null)
        {
            return;
        }

        else
        {
            slowMotionCoroutine = SlowMotion(timeScale, duration);
            StartCoroutine(slowMotionCoroutine);
        }
    }

    public IEnumerator SlowMotion(float timeScale, float duration)
    {
        Time.timeScale = timeScale;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
        slowMotionCoroutine = null;
    }

    #region Property
    public Character Owner
    {
        get { return owner; }
        private set { owner = value; }
    }
    public Collider WeaponCollider
    {
        get { return weaponCollider; }
        set { weaponCollider = value; }
    }
    #endregion
}
