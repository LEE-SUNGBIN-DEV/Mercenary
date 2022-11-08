using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlots : MonoBehaviour
{
    private static QuickSlots instance = null;

    [SerializeField] private Slot[] slots;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            slots = GetComponentsInChildren<Slot>();
            Managers.DataManager.CurrentCharacter.CharacterData.OnLoadPlayerData -= LoadPlayerQuickSlots;
            Managers.DataManager.CurrentCharacter.CharacterData.OnLoadPlayerData += LoadPlayerQuickSlots;
            Managers.DataManager.CurrentCharacter.CharacterData.OnSavePlayerData -= SavePlayerQuickSlots;
            Managers.DataManager.CurrentCharacter.CharacterData.OnSavePlayerData += SavePlayerQuickSlots;
        }

        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        if (Managers.GameSceneManager.CurrentScene.SceneType == SCENE_TYPE.DUNGEON)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (slots[0].Item != null)
                {
                    IConsumableItem consumableItem = slots[0].Item.GetComponent<IConsumableItem>();

                    consumableItem.Consume();
                    slots[0].RemoveItemFromSlot(slots[0].Item);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (slots[1].Item != null)
                {
                    IConsumableItem consumableItem = slots[1].Item.GetComponent<IConsumableItem>();
                    consumableItem.Consume();
                    slots[1].RemoveItemFromSlot(slots[1].Item);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (slots[2].Item != null)
                {
                    IConsumableItem consumableItem = slots[2].Item.GetComponent<IConsumableItem>();
                    consumableItem.Consume();
                    slots[2].RemoveItemFromSlot(slots[2].Item);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (slots[3].Item != null)
                {
                    IConsumableItem consumableItem = slots[3].Item.GetComponent<IConsumableItem>();
                    consumableItem.Consume();
                    slots[3].RemoveItemFromSlot(slots[3].Item);
                }
            }
        }
    }

    #region Save & Load
    public void LoadPlayerQuickSlots(CharacterData characterData)
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            if (characterData.QuickSlotItemNames != null)
            {
                slots[i].LoadItem(Managers.ItemManager.FindItemFromList(characterData.QuickSlotItemNames[i]), characterData.QuickSlotItemCounts[i]);
            }

            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public void SavePlayerQuickSlots(CharacterData characterData)
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            if (slots[i].Item != null)
            {
                characterData.QuickSlotItemNames[i] = Slots[i].Item.ItemName;
                characterData.QuickSlotItemCounts[i] = Slots[i].ItemCount;
            }

            else
            {
                characterData.QuickSlotItemNames[i] = null;
                characterData.QuickSlotItemCounts[i] = 0;
            }
        }
    }
    #endregion

    #region Property
    public static QuickSlots Instance
    {
        get { return instance; }
        private set { instance = value; }
    }
    public Slot[] Slots
    {
        get { return slots; }
        private set { slots = value; }
    }
    #endregion
}
