using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : BaseCombatController
{
    private Collider weaponCollider;
    private Enemy owner;

    public void PlayerHitProcess(Collider target)
    {
        Character character = target.GetComponent<Character>();
        if (character != null)
        {
            Functions.EnemyDamageProcess(Owner, character, DamageRatio);

            switch (CombatType)
            {
                case COMBAT_TYPE.NORMAL:
                    {
                        character.SwitchCharacterState(CHARACTER_STATE.HIT);
                        break;
                    }

                case COMBAT_TYPE.SMASH:
                    {
                        character.SwitchCharacterState(CHARACTER_STATE.HEAVY_HIT);
                        break;
                    }

                case COMBAT_TYPE.STUN:
                    {
                        character.SwitchCharacterState(CHARACTER_STATE.STUN);
                        break;
                    }
            }
        }
    }

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
