using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotManager
{
    private BaseSlot selectSlot;
    private BaseSlot targetSlot;
    private BaseSlot temporarySlot;

    public void Initialize()
    {
        selectSlot = null;
        targetSlot = null;

        GameObject slotObject = Managers.ResourceManager.InstantiatePrefabSync("Prefab_Slot");
        temporarySlot = slotObject.GetComponent<BaseSlot>();
        temporarySlot.gameObject.SetActive(false);
    }

    public void OnBeginDrag(BaseSlot slot)
    {
        temporarySlot.gameObject.SetActive(true);
        temporarySlot.ItemImage.sprite = slot.ItemImage.sprite;
        selectSlot = slot;
        targetSlot = null;
    }
    public void OnDrag(PointerEventData eventData)
    {
        temporarySlot.ItemImage.rectTransform.position = eventData.position;
    }

    public void OnDrop(BaseSlot slot)
    {
        targetSlot = slot;
    }
    public void OnEndDrag()
    {
        DetermineSlotInteration();

        temporarySlot.ClearSlot();
        temporarySlot.gameObject.SetActive(false);

        selectSlot = null;
        targetSlot = null;
    }

    public void DetermineSlotInteration()
    {
        if (TryCancel())
            return;
        if (TryDestroy())
            return;
        if (TryCombine())
            return;
        if (TryMount())
            return;
        if (TryDismount())
            return;
        if (TryExchanage())
            return;
    }

    public bool TryCancel()
    {
        if(selectSlot == targetSlot)
        {
            return true;
        }
        return false;
    }

    public bool TryDestroy()
    {
        if (targetSlot == null)
        {
            selectSlot.ClearSlot();
            return true;
        }
        return false;
    }

    public bool TryCombine()
    {
        if (selectSlot.Item.ItemID == targetSlot.Item.ItemID
            && selectSlot.Item.IsCountable)
        {
            targetSlot.CombineSlot(selectSlot);
            return true;
        }
        return false;
    }
    public bool TryMount()
    {
        if(targetSlot is EquipmentSlot && selectSlot is not EquipmentSlot)
        {
            EquipmentSlot targetEquipmentSlot = targetSlot as EquipmentSlot;

        }
        if (true)
        {
        }
        return false;
    }
    public bool TryDismount()
    {
        if (true)
        {
        }
        return false;
    }
    public bool TryExchanage()
    {
        if (selectSlot.Item.IsCountable == false || targetSlot.Item.IsCountable == false)
        {
            targetSlot.ExchanageSlot(selectSlot);
            return true;
        }
        return false;
    }

    #region Property
    public BaseSlot SelectSlot { get { return selectSlot; } set { selectSlot = value; } }
    public BaseSlot TargetSlot { get { return targetSlot; } set { targetSlot = value; } }
    public BaseSlot TemporarySlot { get { return temporarySlot; } set { temporarySlot = value; } }
    #endregion
}
