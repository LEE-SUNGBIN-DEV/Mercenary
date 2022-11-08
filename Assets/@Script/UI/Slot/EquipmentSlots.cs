using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EQUIPMENT_SLOT_INDEX
{
    WEAPON = 0,
    HELMET = 1,
    ARMOR = 2,
    BOOTS = 3,
}

public class EquipmentSlots : MonoBehaviour
{
    private static EquipmentSlots instance = null;

    [SerializeField] private Slot[] slots;
    private Dictionary<ITEM_TYPE, EQUIPMENT_SLOT_INDEX> equipmentSlotDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            slots = GetComponentsInChildren<Slot>();
            Managers.DataManager.CurrentCharacter.CharacterData.OnLoadPlayerData -= LoadPlayerEquipmentSlots;
            Managers.DataManager.CurrentCharacter.CharacterData.OnLoadPlayerData += LoadPlayerEquipmentSlots;
            Managers.DataManager.CurrentCharacter.CharacterData.OnSavePlayerData -= SavePlayerEquipmentSlots;
            Managers.DataManager.CurrentCharacter.CharacterData.OnSavePlayerData += SavePlayerEquipmentSlots;

            EquipmentSlotDictionary = new Dictionary<ITEM_TYPE, EQUIPMENT_SLOT_INDEX>()
            {
                {ITEM_TYPE.WEAPON, EQUIPMENT_SLOT_INDEX.WEAPON},
                {ITEM_TYPE.HELMET, EQUIPMENT_SLOT_INDEX.HELMET},
                {ITEM_TYPE.ARMOR, EQUIPMENT_SLOT_INDEX.ARMOR},
                {ITEM_TYPE.BOOTS, EQUIPMENT_SLOT_INDEX.BOOTS}
            };
        }

        else
        {
            Destroy(gameObject);
            return;
        }
    }

    #region Save & Load
    public void LoadPlayerEquipmentSlots(CharacterData characterData)
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            if (characterData.EquipmentSlotItemNames != null)
            {
                slots[i].LoadItem(Managers.ItemManager.FindItemFromList(characterData.EquipmentSlotItemNames[i]), characterData.EquipmentSlotItemCounts[i]);
                if(slots[i].Item != null)
                {
                    EquipmentItem equipmentItem = slots[i].Item as EquipmentItem;
                    equipmentItem.Equip();
                }

            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public void SavePlayerEquipmentSlots(CharacterData characterData)
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            if (slots[i].Item != null)
            {
                characterData.EquipmentSlotItemNames[i] = Slots[i].Item.ItemName;
                characterData.EquipmentSlotItemCounts[i] = Slots[i].ItemCount;
            }

            else
            {
                characterData.EquipmentSlotItemNames[i] = null;
                characterData.EquipmentSlotItemCounts[i] = 0;
            }
        }
    }

    #endregion

    #region Property
    public static EquipmentSlots Instance
    {
        get { return instance; }
        private set { instance = value; }
    }
    public Slot[] Slots
    {
        get { return slots; }
        private set { slots = value; }
    }
    public Dictionary<ITEM_TYPE, EQUIPMENT_SLOT_INDEX> EquipmentSlotDictionary
    {
        get { return equipmentSlotDictionary; }
        private set { equipmentSlotDictionary = value; }
    }
    #endregion
}
