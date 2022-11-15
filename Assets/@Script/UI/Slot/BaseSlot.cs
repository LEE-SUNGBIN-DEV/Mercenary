using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public enum SLOT_TYPE
{
    NONE = 0,

    WEAPON = 10,
    HELMET = 20,
    ARMOR = 30,
    BOOTS = 40,

    EQUIPMENT = 100,
    CONSUMPTION = 200,
    OTHER = 300,
    QUEST = 400,

    ALL = 1000,
    QUICK = 2000
}

public abstract class BaseSlot: UIBase, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public enum IMAGE
    {
        ItemImage
    }
    public enum TEXT
    {
        ItemCountText
    }

    protected SLOT_TYPE slotType;
    protected Item item;
    protected Image itemImage;
    protected int itemCount;
    protected TextMeshProUGUI itemCountText;

    public virtual void Initialize()
    {
        BindImage(typeof(IMAGE));
        BindText(typeof(TEXT));

        itemImage = GetImage((int)IMAGE.ItemImage);
        itemCountText = GetText((int)TEXT.ItemCountText);

        slotType = SLOT_TYPE.NONE;
        item = null;
        itemCount = 0;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Managers.SlotManager.OnBeginDrag(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Managers.SlotManager.OnDrag(eventData);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Managers.SlotManager.OnDrop(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Managers.SlotManager.OnEndDrag();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
    
    public void AddItem(Item targetItem)
    {
        item = targetItem;
        itemImage.sprite = targetItem.ItemSprite;
        if (targetItem.IsCountable)
        {
            ++itemCount;
            if (itemCount > 0)
            {
                itemCountText.text = $"{itemCount}";
                itemCountText.enabled = true;
            }
        }
        else
        {
            itemCountText.enabled = false;
        }
    }
    public void RemoveItem(int amount = 1)
    {
        itemCount -= amount;
        if(itemCount <= 0)
        {
            ClearSlot();
        }
    }

    #region Slot Function
    public void ExchanageSlot(BaseSlot slot)
    {
        BaseSlot temporarySlot = this;

        SetSlot(slot);
        slot.SetSlot(temporarySlot);
    }
    public void CombineSlot(BaseSlot slot)
    {
        if (slot.Item.IsCountable == false)
        {
            Debug.Log($"{this}: 합칠 수 없는 아이템입니다.");
            return;
        }

        itemCount += slot.ItemCount;
        if (itemCount > 0)
        {
            itemCountText.text = $"{itemCount}";
            itemCountText.enabled = true;
        }
        slot.ClearSlot();
    }
    public void SetSlot(BaseSlot slot)
    {
        item = slot.Item;
        itemImage.sprite = slot.Item.ItemSprite;
        itemCount = slot.ItemCount;
        if (slot.Item.IsCountable == true)
        {
            itemCountText.text = $"{slot.ItemCount}";
            itemCountText.enabled = true;
        }
        itemCountText.enabled = false;
    }
    public void ClearSlot()
    {
        item = null;
        itemImage.sprite = null;
        itemCount = 0;
        itemCountText.text = null;
        itemCountText.enabled = false;
    }
    #endregion

    #region Property
    public SLOT_TYPE SlotType { get { return slotType; } set { slotType = value; } }
    public Item Item { get { return item; } set { item = value; } }
    public Image ItemImage { get { return itemImage; } set { itemImage = value; } }
    public int ItemCount { get { return itemCount; } set { itemCount = value; } }
    public TextMeshProUGUI ItemCountText { get { return itemCountText; } set { itemCountText = value; } }
    #endregion
}
