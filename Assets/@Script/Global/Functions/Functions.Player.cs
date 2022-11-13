using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Functions
{
    public static void SetCharacterPosition(Character character, Vector3 targetPosition)
    {
        character.CharacterController.enabled = false;
        character.transform.position = targetPosition;
        character.CharacterController.enabled = true;
    }
    public static void SetCharacterTransform(Character character, Transform targetTransform)
    {
        character.CharacterController.enabled = false;
        character.gameObject.SetTransform(targetTransform);
        character.CharacterController.enabled = true;
    }
    public static void CreateCharacterWithCamera(Vector3 position)
    {
        GameObject cameraObject = Managers.ResourceManager.InstantiatePrefabSync("Prefab_Player_Camera");
        Character character = Managers.ResourceManager.InstantiatePrefabSync("Prefab_" + Managers.DataManager.CurrentCharacterData.CharacterClass).GetComponent<Character>();

        SetCharacterPosition(character, position);
        cameraObject.transform.position = position;
    }
    public static void TeleportCharacterWithCamera(Character character, Vector3 position, PlayerCamera camera)
    {
        SetCharacterPosition(character, position);
        camera.transform.position = position;
    }

    // !!플레이어의 대미지를 계산하는 함수
    public static void PlayerDamageProcess(Character character, Enemy enemy, float ratio)
    {
        // Basic Damage Process
        float damage = (character.CharacterStats.AttackPower - enemy.DefensivePower * 0.5f) * 0.5f;
        if (damage < 0)
        {
            damage = 0;
        }
        damage += ((character.CharacterStats.AttackPower / 8f - character.CharacterStats.AttackPower / 16f) + 1f);

        // Critical Process
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

        // Damage Ratio Process
        damage *= ratio;

        // Final Damage Process
        float damageRange = Random.Range(0.9f, 1.1f);
        damage *= damageRange;

        enemy.CurrentHitPoint -= damage;

        FloatingDamageText floatingDamageText = Managers.ObjectPoolManager.RequestObject(Constants.RESOURCE_NAME_PREFAB_FLOATING_DAMAGE_TEXT).GetComponent<FloatingDamageText>();
        floatingDamageText.SetDamageText(isCritical, damage, enemy.transform.position);
    }

    // !! 적의 대미지를 계산하는 함수
    public static void EnemyDamageProcess(Enemy enemy, Character character, float ratio)
    {
        // Damage Process
        float damage = (enemy.AttackPower - character.CharacterStats.DefensivePower * 0.5f) * 0.5f;
        if (damage < 0)
        {
            damage = 0;
        }
        damage += ((enemy.AttackPower / 8f - enemy.AttackPower / 16f) + 1f);

        // Final Damage
        damage *= ratio;

        character.CharacterStats.CurrentHitPoint -= damage;
    }
}
