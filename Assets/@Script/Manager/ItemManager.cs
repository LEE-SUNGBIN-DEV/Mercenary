using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    [SerializeField] private List<Item> itemList;
    private Dictionary<string, Item> itemDictionary;

    private Dictionary<SLOT_TYPE, Dictionary<ITEM_TYPE, bool>> isAllowItemDictionary;
    private Dictionary<ITEM_TYPE, bool> weaponSlotAllowItem;
    private Dictionary<ITEM_TYPE, bool> helmetSlotAllowItem;
    private Dictionary<ITEM_TYPE, bool> armorSlotAllowItem;
    private Dictionary<ITEM_TYPE, bool> bootsSlotAllowItem;

    private Dictionary<ITEM_TYPE, bool> allSlotAllowItem;
    private Dictionary<ITEM_TYPE, bool> quickSlotAllowItem;

    public void Initialize()
    {
        ItemDictionary = new Dictionary<string, Item>();
        for (int i = 0; i < ItemList.Count; ++i)
        {
            ItemDictionary.Add(ItemList[i].ItemName, ItemList[i]);
        }

        WeaponSlotAllowItem = new Dictionary<ITEM_TYPE, bool>()
            {
                {ITEM_TYPE.EMPTY, true},
                {ITEM_TYPE.WEAPON, true},
                {ITEM_TYPE.HELMET, false},
                {ITEM_TYPE.ARMOR, false},
                {ITEM_TYPE.BOOTS, false},
                {ITEM_TYPE.CONSUMPTION, false},
                {ITEM_TYPE.OTHER, false},
                {ITEM_TYPE.QUEST, false}
            };
        HelmetSlotAllowItem = new Dictionary<ITEM_TYPE, bool>()
            {
                {ITEM_TYPE.EMPTY, true},
                {ITEM_TYPE.WEAPON, false},
                {ITEM_TYPE.HELMET, true},
                {ITEM_TYPE.ARMOR, false},
                {ITEM_TYPE.BOOTS, false},
                {ITEM_TYPE.CONSUMPTION, false},
                {ITEM_TYPE.OTHER, false},
                {ITEM_TYPE.QUEST, false}
            };
        ArmorSlotAllowItem = new Dictionary<ITEM_TYPE, bool>()
            {
                {ITEM_TYPE.EMPTY, true},
                {ITEM_TYPE.WEAPON, false},
                {ITEM_TYPE.HELMET, false},
                {ITEM_TYPE.ARMOR, true},
                {ITEM_TYPE.BOOTS, false},
                {ITEM_TYPE.CONSUMPTION, false},
                {ITEM_TYPE.OTHER, false},
                {ITEM_TYPE.QUEST, false}
            };
        BootsSlotAllowItem = new Dictionary<ITEM_TYPE, bool>()
            {
                {ITEM_TYPE.EMPTY, true},
                {ITEM_TYPE.WEAPON, false},
                {ITEM_TYPE.HELMET, false},
                {ITEM_TYPE.ARMOR, false},
                {ITEM_TYPE.BOOTS, true},
                {ITEM_TYPE.CONSUMPTION, false},
                {ITEM_TYPE.OTHER, false},
                {ITEM_TYPE.QUEST, false}
            };
        QuickSlotAllowItem = new Dictionary<ITEM_TYPE, bool>()
            {
                {ITEM_TYPE.EMPTY, true},
                {ITEM_TYPE.WEAPON, false},
                {ITEM_TYPE.HELMET, false},
                {ITEM_TYPE.ARMOR, false},
                {ITEM_TYPE.BOOTS, false},
                {ITEM_TYPE.CONSUMPTION, true},
                {ITEM_TYPE.OTHER, false},
                {ITEM_TYPE.QUEST, false}
            };
        AllSlotAllowItem = new Dictionary<ITEM_TYPE, bool>()
            {
                {ITEM_TYPE.EMPTY, true},
                {ITEM_TYPE.WEAPON, true},
                {ITEM_TYPE.HELMET, true},
                {ITEM_TYPE.ARMOR, true},
                {ITEM_TYPE.BOOTS, true},
                {ITEM_TYPE.CONSUMPTION, true},
                {ITEM_TYPE.OTHER, true},
                {ITEM_TYPE.QUEST, true}
            };

        IsAllowItemDictionary = new Dictionary<SLOT_TYPE, Dictionary<ITEM_TYPE, bool>>
            {
                {SLOT_TYPE.WEAPON, WeaponSlotAllowItem},
                {SLOT_TYPE.HELMET, HelmetSlotAllowItem},
                {SLOT_TYPE.ARMOR, ArmorSlotAllowItem},
                {SLOT_TYPE.BOOTS, BootsSlotAllowItem},
                {SLOT_TYPE.QUICK, QuickSlotAllowItem},
                {SLOT_TYPE.ALL, AllSlotAllowItem}
            };
    }

    public Item FindItemFromList(string itemName)
    {
        if(itemName == null)
        {
            return null;
        }

        else if(ItemDictionary.ContainsKey(itemName))
        {
            return ItemDictionary[itemName];
        }

        else
        {
            return null;
        }
    }

    #region Property
    public List<Item> ItemList
    {
        get { return itemList; }
        private set { itemList = value; }
    }
    public Dictionary<string, Item> ItemDictionary
    {
        get { return itemDictionary; }
        private set { itemDictionary = value; }
    }
    public Dictionary<ITEM_TYPE, bool> QuickSlotAllowItem
    {
        get { return quickSlotAllowItem; }
        private set { quickSlotAllowItem = value; }
    }
    public Dictionary<SLOT_TYPE, Dictionary<ITEM_TYPE, bool>> IsAllowItemDictionary
    {
        get { return isAllowItemDictionary; }
        private set { isAllowItemDictionary = value; }
    }
    public Dictionary<ITEM_TYPE, bool> WeaponSlotAllowItem
    {
        get { return weaponSlotAllowItem; }
        private set { weaponSlotAllowItem = value; }
    }
    public Dictionary<ITEM_TYPE, bool> HelmetSlotAllowItem
    {
        get { return helmetSlotAllowItem; }
        private set { helmetSlotAllowItem = value; }
    }
    public Dictionary<ITEM_TYPE, bool> ArmorSlotAllowItem
    {
        get { return armorSlotAllowItem; }
        private set { armorSlotAllowItem = value; }
    }
    public Dictionary<ITEM_TYPE, bool> BootsSlotAllowItem
    {
        get { return bootsSlotAllowItem; }
        private set { bootsSlotAllowItem = value; }
    }
    public Dictionary<ITEM_TYPE, bool> AllSlotAllowItem
    {
        get { return allSlotAllowItem; }
        private set { allSlotAllowItem = value; }
    }
    #endregion
}
