using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Slot[] slots;
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Awake()
    {
        slots = GetComponentsInChildren<Slot>();

        Managers.DataManager.CurrentCharacter.CharacterData.OnPlayerDataChanged += (CharacterData playerData) =>
        {
            MoneyText.text = playerData.Money.ToString();
        };

        Managers.DataManager.CurrentCharacter.CharacterData.OnLoadPlayerData -= LoadPlayerInventory;
        Managers.DataManager.CurrentCharacter.CharacterData.OnLoadPlayerData += LoadPlayerInventory;
        Managers.DataManager.CurrentCharacter.CharacterData.OnSavePlayerData -= SavePlayerInventory;
        Managers.DataManager.CurrentCharacter.CharacterData.OnSavePlayerData += SavePlayerInventory;
    }

    public void AddItemToInventory(Item item, int itemCount = 1)
    {
        // 중복 아이템 처리
        for (int i = 0; i < slots.Length; ++i)
        {
            if (slots[i].Item != null
                && item.ItemName == slots[i].Item.ItemName
                && item.ItemType != ITEM_TYPE.WEAPON)
            {
                slots[i].AddItemToSlot(item, slots[i].ItemCount + itemCount);
                return;
            }
        }

        // 빈 슬롯 찾기
        int slotIndex = FindEmptySlot();

        if (slotIndex != -1)
        {
            slots[slotIndex].AddItemToSlot(item, itemCount);
            return;
        }

        else
        {
            Managers.UIManager.RequestNotice("인벤토리가 가득 찼습니다.");
            return;
        }

    }

    public void RemoveItem(Item item, int itemCount = 1)
    {
        // 중복 아이템 처리
        for (int i = 0; i < slots.Length; ++i)
        {
            if (item.ItemName == slots[i].Item.ItemName)
            {
                slots[i].RemoveItemFromSlot(item, itemCount);
                return;
            }
        }

        Managers.UIManager.RequestNotice("아이템이 존재하지 않습니다.");
    }

    public int FindEmptySlot()
    {
        for(int i=0; i<slots.Length; ++i)
        {
            if(slots[i].Item == null)
            {
                return i;
            }
        }

        return -1;
    }

    #region Save & Load
    public void LoadPlayerInventory(CharacterData characterData)
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            if (characterData.InventoryItemNames != null)
            {
                slots[i].LoadItem(Managers.ItemManager.FindItemFromList(characterData.InventoryItemNames[i]), characterData.InventoryItemCounts[i]);
            }

            else
            {
                slots[i].ClearSlot();
            }
        }

        MoneyText.text = characterData.Money.ToString();
    }

    public void SavePlayerInventory(CharacterData characterData)
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            if (slots[i].Item != null)
            {
                characterData.InventoryItemNames[i] = Slots[i].Item.ItemName;
                characterData.InventoryItemCounts[i] = Slots[i].ItemCount;
            }

            else
            {
                characterData.InventoryItemNames[i] = null;
                characterData.InventoryItemCounts[i] = 0;
            }
        }
    }
    #endregion

    #region Property
    public Slot[] Slots
    {
        get { return slots; }
        private set { slots = value; }
    }
    public TextMeshProUGUI MoneyText
    {
        get { return moneyText; }
        private set { moneyText = value; }
    }
    #endregion
}
