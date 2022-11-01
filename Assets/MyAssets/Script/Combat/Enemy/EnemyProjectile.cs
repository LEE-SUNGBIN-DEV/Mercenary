using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyCombatController
{
    [SerializeField] private float speed;
    [SerializeField] private string key;
    [SerializeField] private float returnTime;
    [SerializeField] private ObjectPool[] hitEffects;
    [SerializeField] private Vector3 hitEffectRotationOffset;

    private void OnEnable()
    {
        StartCoroutine(AutoReturn(key, ReturnTime));
    }

    private void Update()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IPlayer player = other.GetComponent<IPlayer>();

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

        if (other.gameObject.layer == 6)
        {
            foreach (ObjectPool hitEffect in HitEffects)
            {
                GameObject effect = Managers.ObjectPoolManager.RequestObject(hitEffect.key);
                effect.transform.position = other.bounds.ClosestPoint(transform.position);
                effect.transform.rotation = Quaternion.Euler(other.transform.rotation.eulerAngles + HitEffectRotationOffset);
            }

            Managers.ObjectPoolManager.ReturnObject(key, gameObject);
        }
    }

    private IEnumerator AutoReturn(string key, float _returnTime)
    {
        yield return new WaitForSeconds(_returnTime);
        Managers.ObjectPoolManager.ReturnObject(key, gameObject);
    }

    #region Property
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    public string Key
    {
        get { return key; }
        set { key = value; }
    }
    public float ReturnTime
    {
        get { return returnTime; }
        set { returnTime = value; }
    }
    public ObjectPool[] HitEffects
    {
        get { return hitEffects; }
        set { hitEffects = value; }
    }
    public Vector3 HitEffectRotationOffset
    {
        get { return hitEffectRotationOffset; }
        set { hitEffectRotationOffset = value; }
    }
    #endregion
}
