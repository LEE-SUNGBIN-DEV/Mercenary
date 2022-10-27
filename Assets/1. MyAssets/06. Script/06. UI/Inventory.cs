using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    private static Inventory instance = null;

    [SerializeField] private Slot[] slots;
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            slots = GetComponentsInChildren<Slot>();

            CharacterData.OnPlayerDataChanged += (CharacterData playerData) =>
            {
                MoneyText.text = playerData.Money.ToString();
            };

            CharacterData.onLoadPlayerData -= LoadPlayerInventory;
            CharacterData.onLoadPlayerData += LoadPlayerInventory;
            CharacterData.onSavePlayerData -= SavePlayerInventory;
            CharacterData.onSavePlayerData += SavePlayerInventory;
        }

        else
        {
            Destroy(gameObject);
            return;
        }
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
            UIManager.Instance.RequestNotice("인벤토리가 가득 찼습니다.");
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

        UIManager.Instance.RequestNotice("아이템이 존재하지 않습니다.");
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
    public void LoadPlayerInventory(PlayerSaveData playerSaveData)
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            if (playerSaveData.inventoryItemNames != null)
            {
                slots[i].LoadItem(ItemManager.Instance.FindItemFromList(playerSaveData.inventoryItemNames[i]), playerSaveData.inventoryItemCounts[i]);
            }

            else
            {
                slots[i].ClearSlot();
            }
        }

        MoneyText.text = playerSaveData.money.ToString();
    }

    public void SavePlayerInventory(PlayerSaveData playerSaveData)
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            if (slots[i].Item != null)
            {
                playerSaveData.inventoryItemNames[i] = Slots[i].Item.ItemName;
                playerSaveData.inventoryItemCounts[i] = Slots[i].ItemCount;
            }

            else
            {
                playerSaveData.inventoryItemNames[i] = null;
                playerSaveData.inventoryItemCounts[i] = 0;
            }
        }
    }
    #endregion

    #region Property
    public static Inventory Instance
    {
        get { return instance; }
        private set { instance = value; }
    }
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
