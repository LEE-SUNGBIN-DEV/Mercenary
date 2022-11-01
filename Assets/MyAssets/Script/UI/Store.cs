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
        if (Managers.GameManager.CurrentCharacter.CharacterData.Money < targetSlot.Item.ItemPrice)
        {
            Managers.UIManager.RequestNotice("�������� �����մϴ�.");
        }

        else if (requester.CharacterInventory.FindEmptySlot() == -1)
        {
            Managers.UIManager.RequestNotice("�κ��丮�� �����մϴ�.");
        }

        else
        {
            Managers.GameManager.CurrentCharacter.CharacterData.Money -= targetSlot.Item.ItemPrice;
            requester.CharacterInventory.AddItemToInventory(targetSlot.Item);
        }


    }
}
