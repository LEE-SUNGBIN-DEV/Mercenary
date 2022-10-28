using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySkill : MonoBehaviour
{
    [SerializeField] private Enemy owner;
    [SerializeField] private float cooldown;
    [SerializeField] private float maxRange;
    private bool isRotate;
    private bool isReady;

    public virtual void Update()
    {
        if(IsRotate == true && Owner.Target != null)
        {
            Owner.LookTarget(owner.RotationOffset);
        }
    }
    public abstract void ActiveSkill();

    public virtual bool CheckCondition(float _targetDistance)
    {
        return (IsReady && (_targetDistance <= MaxRange));
    }
    public IEnumerator WaitForRotate()
    {
        owner.IsAttack = true;
        IsRotate = true;
        yield return new WaitForSeconds(0.5f);
        IsRotate = false;
    }
    public IEnumerator SkillCooldown()
    {
        Owner.IsAttack = true;
        IsReady = false;
        yield return new WaitForSeconds(Cooldown);
        IsReady = true;
    }

    #region Property
    public Enemy Owner
    {
        get { return owner; }
        set { owner = value; }
    }
    public float MaxRange
    {
        get { return maxRange; }
        set { maxRange = value; }
    }
    public float Cooldown
    {
        get { return cooldown; }
        set { cooldown = value; }
    }
    public bool IsRotate
    {
        get { return isRotate; }
        set { isRotate = value; }
    }
    public bool IsReady
    {
        get { return isReady; }
        set { isReady = value; }
    }
    #endregion
}
