using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum POTION_ITEM
{
    NORMAL_HP_POTION,
    NORMAL_SP_POTION,

    SIZE
}

public class Potion : Item, IConsumableItem
{
    [SerializeField] private POTION_ITEM potionItem;
    [SerializeField] private float recoveryAmount;

    public static UnityAction<POTION_ITEM, float> onConsumePotion;

    public void Consume()
    {
        onConsumePotion(potionItem, recoveryAmount);
        Managers.AudioManager.PlaySFX("Potion Consume");
    }
}
