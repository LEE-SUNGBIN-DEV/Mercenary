using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameFunction
{
    // 플레이어 대미지 프로세스
    public static void PlayerAttackProcess(Character player, Enemy monster, float ratio)
    {
        // Damage Func
        float damage = (player.AttackPower - monster.DefensivePower * 0.5f) * 0.5f;

        if (damage < 0)
        {
            damage = 0;
        }

        damage += ((player.AttackPower / 8f - player.AttackPower / 16f) + 1f);

        // Critical
        bool isCritical;
        float randomNumber = Random.Range(0.0f, 100.0f);
        if (randomNumber <= player.CriticalChance)
        {
            isCritical = true;
            damage *= (1 + player.CriticalDamage * 0.01f);

            AudioManager.Instance.PlaySFX("Player Critical Attack");
        }

        else
        {
            isCritical = false;
            AudioManager.Instance.PlaySFX("Player Attack");
        }

        // Damage Ratio
        damage *= ratio;

        // Min ~ Max
        float damageRange = Random.Range(0.9f, 1.1f);
        damage *= damageRange;

        monster.CurrentHitPoint -= damage;

        FloatingDamageText floatingDamageText = EffectPoolManager.Instance.RequestObject(EFFECT_POOL.COMBAT_DAMAGE_TEXT).GetComponent<FloatingDamageText>();
        floatingDamageText.SetDamageText(isCritical, damage, monster.transform.position);
    }

    // 몬스터 대미지 프로세스
    public static void MonsterAttackProcess(Enemy monster, Character player, float ratio)
    {
        // Damage Func
        float damage
            = (monster.AttackPower - player.DefensivePower * 0.5f) * 0.5f;

        if (damage < 0)
        {
            damage = 0;
        }

        damage += ((monster.AttackPower / 8f - monster.AttackPower / 16f) + 1f);

        // Damage Ratio
        damage *= ratio;

        player.CurrentHitPoint -= damage;
    }
}
