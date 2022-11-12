using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameFunction
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
    public static Color SetColor(Color color, float alpha = 1f)
    {
        Color targetColor = color;
        targetColor.a = alpha;

        return targetColor;
    }
    #region Async Operation
    public static IEnumerator WaitAsyncOperation(System.Func<bool> isLoaded, UnityAction callback = null)
    {
        while (!isLoaded.Invoke())
        {
            yield return null;
        }

        callback?.Invoke();
    }
    #endregion

    public static T GetOrAddComponent<T>(GameObject gameObject) where T : Component
    {
        T targetComponent = gameObject.GetComponent<T>();

        if (targetComponent == null)
        {
            targetComponent = gameObject.AddComponent<T>();
        }

        return targetComponent;
    }

    public static T FindChild<T>(GameObject gameObject, string name = null, bool recursive = false) where T : Object
    {
        if (gameObject == null)
        {
            return null;
        }

        if (recursive == false)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Transform transform = gameObject.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();

                    if (component != null)
                    {
                        return component;
                    }
                }
            }
        }

        else
        {
            foreach (T component in gameObject.GetComponentsInChildren<T>(true))
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }
            }
        }

        return null;
    }

    // 플레이어 대미지 프로세스
    public static void PlayerAttackProcess(Character character, Enemy enemy, float ratio)
    {
        // Damage Func
        float damage = (character.CharacterStats.AttackPower - enemy.DefensivePower * 0.5f) * 0.5f;

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

        enemy.CurrentHitPoint -= damage;

        FloatingDamageText floatingDamageText = Managers.ObjectPoolManager.RequestObject(GameConstants.RESOURCE_NAME_PREFAB_FLOATING_DAMAGE_TEXT).GetComponent<FloatingDamageText>();
        floatingDamageText.SetDamageText(isCritical, damage, enemy.transform.position);
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
