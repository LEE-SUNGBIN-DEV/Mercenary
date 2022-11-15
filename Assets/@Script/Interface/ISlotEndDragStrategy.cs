using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ISlotEndDragStrategy
{
    public void Action(Slot slot);
}

public class SlotEndDragCancel : ISlotEndDragStrategy
{
    public void Action(Slot slot)
    {
        DragSlot.Instance.ClearDragSlot();
        return;
    }
}

public class SlotEndDragExChange : ISlotEndDragStrategy
{
    public void Action(Slot slot)
    {
        slot.AddItemToSlot(DragSlot.Instance.Item, DragSlot.Instance.ItemCount);
        DragSlot.Instance.ClearDragSlot();
        return;
    }
}

public class SlotEndDragCombine : ISlotEndDragStrategy
{
    public void Action(Slot slot)
    {
        slot.ClearSlot();
        DragSlot.Instance.ClearDragSlot();
        return;
    }
}
