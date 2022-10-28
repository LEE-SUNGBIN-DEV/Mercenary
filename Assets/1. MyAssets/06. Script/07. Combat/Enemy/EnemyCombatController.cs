using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : BaseCombatController
{
    private Collider weaponCollider;
    private Enemy owner;

    #region Property
    public Enemy Owner
    {
        get { return owner; }
        set { owner = value; }
    }
    public Collider WeaponCollider
    {
        get { return weaponCollider; }
        set { weaponCollider = value; }
    }
    #endregion
}
