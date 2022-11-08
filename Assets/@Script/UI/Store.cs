using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] private List<Item> sellList;
    [SerializeField] private StoreSlot[] storeSlots;

    private void Awake()
    {
        storeSlots = GetComponentsInChildren<StoreSlot>();

        for(int i=0; i<sellList.Count; ++i)
        {
            storeSlots[i].SetStoreItem(sellList[i]);

            storeSlots[i].RequestBuy += (StoreSlot targetSlot, Character requester) =>
            {
                BuyItem(targetSlot, requester);
            };
        }
    }

    public void BuyItem(StoreSlot targetSlot, Character requester)
    {
        if (Managers.DataManager.CurrentCharacter.CharacterData.Money < targetSlot.Item.ItemPrice)
        {
            Managers.UIManager.RequestNotice("소지금이 부족합니다.");
        }

        else if (requester.CharacterInventory.FindEmptySlot() == -1)
        {
            Managers.UIManager.RequestNotice("인벤토리가 부족합니다.");
        }

        else
        {
            Managers.DataManager.CurrentCharacter.CharacterData.Money -= targetSlot.Item.ItemPrice;
            requester.CharacterInventory.AddItemToInventory(targetSlot.Item);
        }


    }
}
