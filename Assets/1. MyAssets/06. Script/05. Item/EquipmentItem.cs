using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentItem : Item, IEquipmentItem
{
    [Header("Equipment")]
    [SerializeField] private bool isEquip;
    [SerializeField] private int increasedAmount;

    public void Toggle()
    {
        if(isEquip)
        {
            Disarm();
        }
        else
        {
            Equip();
        }
    }
    public void Equip()
    {
        isEquip = true;
        switch(ItemType)
        {
            case ITEM_TYPE.WEAPON:
                {
                    GameManager.Instance.Player.AttackPower += increasedAmount;
                    break;
                }
            case ITEM_TYPE.HELMET:
                {
                    GameManager.Instance.Player.DefensivePower += increasedAmount;
                    break;
                }
            case ITEM_TYPE.ARMOR:
                {
                    GameManager.Instance.Player.DefensivePower += increasedAmount;
                    break;
                }
            case ITEM_TYPE.BOOTS:
                {
                    GameManager.Instance.Player.DefensivePower += increasedAmount;
                    break;
                }
        }
        AudioManager.Instance.PlaySFX("Equipment Mount");
    }

    public void Disarm()
    {
        isEquip = false;
        switch (ItemType)
        {
            case ITEM_TYPE.WEAPON:
                {
                    GameManager.Instance.Player.AttackPower -= increasedAmount;
                    break;
                }
            case ITEM_TYPE.HELMET:
                {
                    GameManager.Instance.Player.DefensivePower -= increasedAmount;
                    break;
                }
            case ITEM_TYPE.ARMOR:
                {
                    GameManager.Instance.Player.DefensivePower -= increasedAmount;
                    break;
                }
            case ITEM_TYPE.BOOTS:
                {
                    GameManager.Instance.Player.DefensivePower -= increasedAmount;
                    break;
                }
        }
        AudioManager.Instance.PlaySFX("Equipment Dismount");
    }

    #region Property
    public int IncreasedAmount
    {
        get { return increasedAmount; }
        set { increasedAmount = value; }
    }
    public bool IsEquip
    {
        get { return isEquip; }
        set { isEquip = value; }
    }
    #endregion
}
