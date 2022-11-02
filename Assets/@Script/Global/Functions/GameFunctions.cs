using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameFunction
{

    // 플레이어 대미지 프로세스
    public static void PlayerAttackProcess(Character character, Enemy monster, float ratio)
    {
        // Damage Func
        float damage = (character.CharacterStats.AttackPower - monster.DefensivePower * 0.5f) * 0.5f;

        if (damage < 0)
        {
            damage = 0;
        }

        damage += ((character.CharacterStats.AttackPower / 8f - character.CharacterStats.AttackPower / 16f) + 1f);

        // Critical
        bool isCritical;
        float randomNumber = Random.Range(0.0f, 100.0f);
        if (randomNumber <= character.CharacterStats.CriticalChance)
        {
            isCritical = true;
            damage *= (1 + character.CharacterStats.CriticalDamage * 0.01f);

            Managers.AudioManager.PlaySFX("Player Critical Attack");
        }

        else
        {
            isCritical = false;
            Managers.AudioManager.PlaySFX("Player Attack");
        }

        // Damage Ratio
        damage *= ratio;

        // Min ~ Max
        float damageRange = Random.Range(0.9f, 1.1f);
        damage *= damageRange;

        monster.CurrentHitPoint -= damage;

        FloatingDamageText floatingDamageText = Managers.ObjectPoolManager.RequestObject(GameConstants.RESOURCE_NAME_PREFAB_FLOATING_DAMAGE_TEXT).GetComponent<FloatingDamageText>();
        floatingDamageText.SetDamageText(isCritical, damage, monster.transform.position);
    }

    // 적 대미지 프로세스
    public static void EnemyAttackProcess(Enemy enemy, Character character, float ratio)
    {
        // Damage Func
        float damage
            = (enemy.AttackPower - character.CharacterStats.DefensivePower * 0.5f) * 0.5f;

        if (damage < 0)
        {
            damage = 0;
        }

        damage += ((enemy.AttackPower / 8f - enemy.AttackPower / 16f) + 1f);

        // Damage Ratio
        damage *= ratio;

        character.CharacterStats.CurrentHitPoint -= damage;
    }
}
