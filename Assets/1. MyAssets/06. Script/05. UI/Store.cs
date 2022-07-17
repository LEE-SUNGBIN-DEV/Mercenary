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

            storeSlots[i].onBuy += (StoreSlot storeSlot, int price) =>
            {
                BuyItem(storeSlot, price);
            };
        }
    }

    public void BuyItem(StoreSlot storeSlot, int price)
    {
        if (GameManager.Instance.Player.PlayerData.Money < price)
        {
            UIManager.Instance.RequestNotice("소지금이 부족합니다.");
        }

        else if (Inventory.Instance.FindEmptySlot() == -1)
        {
            UIManager.Instance.RequestNotice("인벤토리가 부족합니다.");
        }

        else
        {
            GameManager.Instance.Player.PlayerData.Money -= price;
            Inventory.Instance.AddItemToInventory(storeSlot.Item);
        }


    }
}
