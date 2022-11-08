using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class StoreSlot : MonoBehaviour, IPointerClickHandler
{
    #region Event
    public event UnityAction<StoreSlot, Character> RequestBuy;
    #endregion

    [SerializeField] private Item item;
    [SerializeField] private Image storeSlotImage;
    [SerializeField] private TextMeshProUGUI storeSlotItemNameText;
    [SerializeField] private TextMeshProUGUI storeSlotItemPriceText;

    public void SetStoreItem(Item item)
    {
        if (item != null)
        {
            Item = item;
            StoreSlotImage.sprite = Item.ItemSprite;
            StoreSlotItemNameText.text = Item.ItemName;
            StoreSlotItemPriceText.text = Item.ItemPrice.ToString() + "G";

            SetImageAlpha(1f);
        }
    }

    public void SetImageAlpha(float value)
    {
        if (storeSlotImage.sprite != null)
        {
            storeSlotImage.sprite = Item.ItemSprite;
            Color alpha = storeSlotImage.color;
            alpha.a = value;
            storeSlotImage.color = alpha;
        }
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (Item != null
            && eventData.button == PointerEventData.InputButton.Right
            && Managers.DataManager.CurrentCharacter != null)
        {
            RequestBuy(this, Managers.DataManager.CurrentCharacter);
        }
    }


    #region Property
    public Item Item
    {
        get { return item; }
        set { item = value; }
    }
    public Image StoreSlotImage
    {
        get { return storeSlotImage; }
        set { storeSlotImage = value; }
    }
    public TextMeshProUGUI StoreSlotItemNameText
    {
        get { return storeSlotItemNameText; }
        set { storeSlotItemNameText = value; }
    }
    public TextMeshProUGUI StoreSlotItemPriceText
    {
        get { return storeSlotItemPriceText; }
        set { storeSlotItemPriceText = value; }
    }
    #endregion
}
