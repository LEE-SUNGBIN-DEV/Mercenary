using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public enum SLOT_TYPE
{
    NONE = 0,
    
    WEAPON = 1,
    HELMET = 2,
    ARMOR = 3,
    BOOTS = 4,

    EQUIPMENT = 100,
    CONSUMPTION = 200,
    OTHER = 300,
    QUEST = 400,

    ALL = 1000,
    QUICK = 2000
}
public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    #region Event
    public static event UnityAction onItemDestroy;
    #endregion

    [SerializeField] private SLOT_TYPE slotType;
    [SerializeField] private Item item;
    [SerializeField] private int itemCount;
    [SerializeField] private Image slotImage;
    [SerializeField] private TextMeshProUGUI slotCountText;
    [SerializeField] private RectTransform windowRectTransform;

    public ISlotEndDragStrategy endDragStrategy;

    private void Awake()
    {
        if (Item != null)
        {
            slotImage.sprite = Item.ItemSprite;
            EnableSlotCount();
            SetImageAlpha(1f);
        }

        windowRectTransform = transform.parent.parent.GetComponent<RectTransform>();

        SetEndDragStrategy(null);
    }

    public void SetEndDragStrategy(ISlotEndDragStrategy endDragStrategy)
    {
        this.endDragStrategy = endDragStrategy;
    }

    public bool IsEmpty()
    {
        if (Item == null)
            return true;
        else 
            return false;
    }

    public void LoadItem(Item item, int itemCount)
    {
        if (item != null)
        {
            Item = item;
            ItemCount = itemCount;
            slotImage.sprite = item.ItemSprite;
            EnableSlotCount();
            SetImageAlpha(1f);
        }
    }

    // Processing about Empty Slot, Same Item and Equipment Item is done in call class.
    public void AddItemToSlot(Item item, int itemCount = 1)
    {
        if (item == null)
        {
            ClearSlot();
        }
        else
        {
            Item = item;
            slotImage.sprite = Item.ItemSprite;
            ItemCount = itemCount;
            EnableSlotCount();
            SetImageAlpha(1f);
        }
        
    }

    public void RemoveItemFromSlot(Item item, int itemCount = 1)
    {
        if(item.ItemType == ITEM_TYPE.WEAPON || ItemCount == 1)
        {
            ClearSlot();
        }

        else
        {
            ItemCount -= itemCount;
            EnableSlotCount();
        }
    }

    public void ClearSlot()
    {
        DisableSlotCount();
        SetImageAlpha(0f);
        slotImage.sprite = null;
        ItemCount = 0;
        Item = null;
    }

    public void SetImageAlpha(float alphaValue)
    {
        if (slotImage.sprite != null)
        {
            slotImage.sprite = Item.ItemSprite;
            Color alpha = slotImage.color;
            alpha.a = alphaValue;
            slotImage.color = alpha;
        }
    }

    public void EnableSlotCount()
    {
        slotCountText.text = ItemCount.ToString();
        slotCountText.enabled = true;
    }

    public void DisableSlotCount()
    {
        slotCountText.enabled = false;
    }
    public void OnConfirm()
    {
        ClearSlot();
        DragSlot.Instance.ClearDragSlot();
        ConfirmPanel.onConfirm = null;
        UIManager.Instance.ClosePanel(PANEL_TYPE.CONFIRM);
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (!IsEmpty() && eventData.button == PointerEventData.InputButton.Right)
        {
            switch (Item.ItemType)
            {
                case ITEM_TYPE.WEAPON:
                case ITEM_TYPE.HELMET:
                case ITEM_TYPE.ARMOR:
                case ITEM_TYPE.BOOTS:
                    {
                        switch (SlotType)
                        {
                            // Equipment Slot -> Inventory 
                            case SLOT_TYPE.WEAPON:
                            case SLOT_TYPE.HELMET:
                            case SLOT_TYPE.ARMOR:
                            case SLOT_TYPE.BOOTS:
                                {
                                    UIManager.Instance.OpenPopUp(POPUP_TYPE.STATUS);

                                    EquipmentItem clickedItem = Item as EquipmentItem;
                                    clickedItem.Toggle();

                                    Inventory.Instance.AddItemToInventory(clickedItem);
                                    ClearSlot();
                                    break;
                                }
                            // Inventory -> Equipment Slot
                            case SLOT_TYPE.ALL:
                                {
                                    if (EquipmentSlots.Instance.Slots[(int)EquipmentSlots.Instance.EquipmentSlotDictionary[Item.ItemType]].Item != null)
                                    {
                                        EquipmentItem equippedItem = EquipmentSlots.Instance.Slots[(int)Item.ItemType].Item as EquipmentItem;
                                        equippedItem.Toggle();

                                        EquipmentItem clickedItem = Item as EquipmentItem;
                                        clickedItem.Toggle();

                                        EquipmentSlots.Instance.Slots[(int)EquipmentSlots.Instance.EquipmentSlotDictionary[Item.ItemType]].AddItemToSlot(Item);
                                        AddItemToSlot(equippedItem);

                                        break;
                                    }

                                    else
                                    {
                                        EquipmentItem clickedItem = Item as EquipmentItem;
                                        clickedItem.Toggle();

                                        EquipmentSlots.Instance.Slots[(int)EquipmentSlots.Instance.EquipmentSlotDictionary[Item.ItemType]].AddItemToSlot(Item);
                                        RemoveItemFromSlot(Item);
                                        break;
                                    }
                                }
                        }
                        break;
                    }

                case ITEM_TYPE.CONSUMPTION:
                    {
                        IConsumableItem consumableItem = Item as IConsumableItem;
                        consumableItem.Consume();
                        RemoveItemFromSlot(Item);
                        break;
                    }
            }
        }
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (!IsEmpty())
        {
            DragSlot.Instance.SetDragSlot(this);
        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (!DragSlot.Instance.IsEmpty())
        {
            DragSlot.Instance.SlotImage.rectTransform.position = eventData.position;
        }
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if (!DragSlot.Instance.IsEmpty())
        {
            SetSlotStrategy(this);
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Vector3 dragPosition = DragSlot.Instance.transform.localPosition;

        if(endDragStrategy == null)
        {
            ConfirmPanel.onConfirm -= OnConfirm;
            ConfirmPanel.onConfirm += OnConfirm;
            onItemDestroy();

            DragSlot.Instance.EnableSlotImage(false);

            /*
            // Check Drag Outside
            if (dragPosition.x < WindowRectTransform.localPosition.x
                || dragPosition.x > WindowRectTransform.localPosition.x + WindowRectTransform.rect.width
                || dragPosition.y < WindowRectTransform.localPosition.y
                || dragPosition.y > WindowRectTransform.localPosition.y + WindowRectTransform.rect.height)
            {
                ConfirmPanel.onConfirm -= OnConfirm;
                ConfirmPanel.onConfirm += OnConfirm;
                onItemDestroy();

                DragSlot.Instance.EnableSlotImage(false);
            }
            */
        }
        
        else
        {
            endDragStrategy.Action(this);
            SetEndDragStrategy(null);
        }
        
    }

    public void SetSlotStrategy(Slot targetSlot)
    {
        Item dragItem = DragSlot.Instance.Item;
        int dragItemCount = DragSlot.Instance.ItemCount;
        SLOT_TYPE dragSlotType = DragSlot.Instance.SelectSlot.SlotType;
        ITEM_TYPE dragItemType = DragSlot.Instance.Item.ItemType;

        Item temporaryItem = targetSlot.Item;
        int temporaryItemCount = targetSlot.ItemCount;
        ITEM_TYPE targetItemType = (targetSlot.Item == null) ? ITEM_TYPE.EMPTY : targetSlot.Item.ItemType;

        if(DragSlot.Instance.SelectSlot == targetSlot)
        {
            DragSlot.Instance.SelectSlot.SetEndDragStrategy(new SlotEndDragCancel());
            return;
        }

        // ���� ȣȯ�� üũ
        if(ItemManager.Instance.IsAllowItemDictionary[targetSlot.SlotType][dragItemType]
            && ItemManager.Instance.IsAllowItemDictionary[dragSlotType][targetItemType])
        {
            switch(dragSlotType)
            {
                case SLOT_TYPE.WEAPON:
                case SLOT_TYPE.HELMET:
                case SLOT_TYPE.ARMOR:
                case SLOT_TYPE.BOOTS:
                    {
                        // ȣȯ �Ǵ� ���Կ� ���� ��� ����
                        switch (targetSlot.SlotType)
                        {
                            case SLOT_TYPE.WEAPON:
                            case SLOT_TYPE.HELMET:
                            case SLOT_TYPE.ARMOR:
                            case SLOT_TYPE.BOOTS:
                                {
                                    EquipmentItem dragEquipmentItem = dragItem as EquipmentItem;
                                    dragEquipmentItem.Toggle();

                                    EquipmentItem targetEquipmentItem = targetSlot.Item as EquipmentItem;
                                    targetEquipmentItem.Toggle();

                                    targetSlot.AddItemToSlot(dragItem, dragItemCount);
                                    DragSlot.Instance.ExchangeItem(temporaryItem, temporaryItemCount);
                                    return;
                                }
                            case SLOT_TYPE.ALL:
                                {
                                    EquipmentItem dragEquipmentItem = dragItem as EquipmentItem;
                                    dragEquipmentItem.Toggle();

                                    if(targetSlot.Item != null)
                                    {
                                        EquipmentItem targetEquipmentItem = targetSlot.Item as EquipmentItem;
                                        targetEquipmentItem.Toggle();
                                    }

                                    targetSlot.AddItemToSlot(dragItem, dragItemCount);
                                    DragSlot.Instance.ExchangeItem(temporaryItem, temporaryItemCount);
                                    return;
                                }
                        }
                        return;
                    }
                case SLOT_TYPE.QUICK:
                    {
                        // ȣȯ �Ǵ� ���Կ� ���� ��� ����
                        switch (targetSlot.SlotType)
                        {
                            case SLOT_TYPE.QUICK:
                            case SLOT_TYPE.ALL:
                                {
                                    if(dragItem == targetSlot.Item)
                                    {
                                        targetSlot.AddItemToSlot(targetSlot.Item, targetSlot.ItemCount + dragItemCount);
                                        DragSlot.Instance.CombineItem();
                                    }
                                    else
                                    {
                                        targetSlot.AddItemToSlot(dragItem, dragItemCount);
                                        DragSlot.Instance.ExchangeItem(temporaryItem, temporaryItemCount);
                                    }
                                    return;
                                }
                        }
                        return;
                    }
                case SLOT_TYPE.ALL:
                    {
                        // ȣȯ �Ǵ� ���Կ� ���� ��� ����
                        switch (targetSlot.SlotType)
                        {
                            case SLOT_TYPE.WEAPON:
                            case SLOT_TYPE.HELMET:
                            case SLOT_TYPE.ARMOR:
                            case SLOT_TYPE.BOOTS:
                                {
                                    EquipmentItem dragEquipmentItem = dragItem as EquipmentItem;
                                    dragEquipmentItem.Toggle();

                                    if(targetSlot.Item != null)
                                    {
                                        EquipmentItem targetEquipmentItem = targetSlot.Item as EquipmentItem;
                                        targetEquipmentItem.Toggle();
                                    }

                                    targetSlot.AddItemToSlot(dragItem, dragItemCount);
                                    DragSlot.Instance.ExchangeItem(temporaryItem, temporaryItemCount);
                                    return;
                                }
                            case SLOT_TYPE.QUICK:
                                {
                                    if (dragItem == targetSlot.Item)
                                    {
                                        targetSlot.AddItemToSlot(targetSlot.Item, targetSlot.ItemCount + dragItemCount);
                                        DragSlot.Instance.CombineItem();
                                    }
                                    else
                                    {
                                        targetSlot.AddItemToSlot(dragItem, dragItemCount);
                                        DragSlot.Instance.ExchangeItem(temporaryItem, temporaryItemCount);
                                    }
                                    return;
                                }
                            case SLOT_TYPE.ALL:
                                {
                                    if (dragItem == targetSlot.Item)
                                    {
                                        if(targetSlot.Item.IsCountable)
                                        {
                                            targetSlot.AddItemToSlot(targetSlot.Item, targetSlot.ItemCount + dragItemCount);
                                            DragSlot.Instance.CombineItem();
                                        }

                                        else
                                        {
                                            targetSlot.AddItemToSlot(dragItem, dragItemCount);
                                            DragSlot.Instance.ExchangeItem(temporaryItem, temporaryItemCount);
                                        }
                                    }
                                    else
                                    {
                                        targetSlot.AddItemToSlot(dragItem, dragItemCount);
                                        DragSlot.Instance.ExchangeItem(temporaryItem, temporaryItemCount);
                                    }
                                    return;
                                }
                        }
                        return;
                    }
            }
        }
        else
        {
            DragSlot.Instance.CancelItem();
            return;
        }

        /*
        // Check Level: Slot Type > Item Type > Item
        // �� Same Slot Type
        if (dragSlotType == targetSlot.SlotType)
        {
            // �ڡ� Same Slot Type + Same Item Type
            if (dragItemType == targetItemType)
            {
                // �ڡڡ� Same Slot Type + Same Item Type + Same Item
                if (dragItem == targetSlot.Item)
                {
                    SameItemProcess(dragItem, dragItemCount, targetSlot);
                    return;
                }
                // �ڡڡ� Same Slot Type + Same Item Type + Different Item
                else
                {
                    targetSlot.AddItemToSlot(dragItem, dragItemCount);
                    DragSlot.Instance.ExchangeItem(temporaryItem, temporaryItemCount);
                    return;
                }
            }
            // �ڡ� Same Slot Type + Different Item Type
            else
            {
                if (targetSlot.SlotType == SLOT_TYPE.ALL)
                {
                    targetSlot.AddItemToSlot(dragItem, dragItemCount);
                    DragSlot.Instance.ExchangeItem(temporaryItem, temporaryItemCount);
                    return;
                }
                else
                {
                    DragSlot.Instance.CancelItem();
                    return;
                }
            }
        }

        // �� Different Slot Type
        else
        {
            // �ڡ� Different Slot Type + Same Item Type
            if (dragItemType == targetItemType)
            {
                // �ڡڡ� Different Slot Type + Same Item Type + Same Item
                if (dragItem == targetSlot.Item)
                {
                    SameItemProcess(dragItem, dragItemCount, targetSlot);
                    return;
                }
                // �ڡڡ� Different Slot Type + Same Item Type + Different Item
                else
                {
                    targetSlot.AddItemToSlot(dragItem, dragItemCount);
                    DragSlot.Instance.ExchangeItem(temporaryItem, temporaryItemCount);
                    return;
                }
            }
            // �ڡ� Different Slot Type + Different Item Type
            else
            {
                // �ڡڡ� Different Slot Type + Different Item Type + Empty
                if (targetItemType == ITEM_TYPE.EMPTY)
                {
                    switch (targetSlot.SlotType)
                    {
                        case SLOT_TYPE.WEAPON:
                        case SLOT_TYPE.HELMET:
                        case SLOT_TYPE.ARMOR:
                        case SLOT_TYPE.BOOTS:
                        case SLOT_TYPE.CONSUMPTION:
                        case SLOT_TYPE.OTHER:
                        case SLOT_TYPE.QUEST:
                            {
                                if ((int)targetSlot.SlotType == (int)dragItemType)
                                {
                                    targetSlot.AddItemToSlot(dragItem, dragItemCount);
                                    DragSlot.Instance.ExchangeItem(temporaryItem, temporaryItemCount);
                                }
                                else
                                {
                                    DragSlot.Instance.CancelItem();
                                }
                                return;
                            }
                        case SLOT_TYPE.ALL:
                            {
                                targetSlot.AddItemToSlot(dragItem, dragItemCount);
                                DragSlot.Instance.ExchangeItem(temporaryItem, temporaryItemCount);
                                return;
                            }
                    }
                }

                // �ڡڡ� Different Slot Type + Different Item Type + Not Empty
                else
                {
                    DragSlot.Instance.CancelItem();
                    return;
                }
            }
        }
        */
    }

    #region Property
    public Item Item
    {
        get { return item; }
        set { item = value; }
    }
    public int ItemCount
    {
        get { return itemCount; }
        set { itemCount = value; }
    }
    public SLOT_TYPE SlotType
    {
        get { return slotType; }
        private set { slotType = value; }
    }
    public RectTransform WindowRectTransform
    {
        get { return windowRectTransform; }
        private set { windowRectTransform = value; }
    }

    #endregion
}
