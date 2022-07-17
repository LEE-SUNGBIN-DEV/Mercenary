using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : CombatController
{
    [SerializeField] private Monster owner;
    [SerializeField] private float speed;
    [SerializeField] private EFFECT_POOL objectKey;
    [SerializeField] private float returnTime;
    [SerializeField] private EFFECT_POOL[] hitEffects;
    [SerializeField] private Vector3 hitEffectRotationOffset;

    private void OnEnable()
    {
        StartCoroutine(AutoReturn(ObjectKey, ReturnTime));
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
            foreach (EFFECT_POOL hitEffect in HitEffects)
            {
                GameObject effect = EffectPoolManager.Instance.RequestObject(hitEffect);
                effect.transform.position = other.bounds.ClosestPoint(transform.position);
                effect.transform.rotation = Quaternion.Euler(other.transform.rotation.eulerAngles + HitEffectRotationOffset);
            }

            EffectPoolManager.Instance.ReturnObject(ObjectKey, gameObject);
        }
    }

    private IEnumerator AutoReturn(EFFECT_POOL _objectKey, float _returnTime)
    {
        yield return new WaitForSeconds(_returnTime);
        EffectPoolManager.Instance.ReturnObject(_objectKey, gameObject);
    }

    #region Property
    public Monster Owner
    {
        get { return owner; }
        set { owner = value; }
    }
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    public EFFECT_POOL ObjectKey
    {
        get { return objectKey; }
        set { objectKey = value; }
    }
    public float ReturnTime
    {
        get { return returnTime; }
        set { returnTime = value; }
    }
    public EFFECT_POOL[] HitEffects
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
