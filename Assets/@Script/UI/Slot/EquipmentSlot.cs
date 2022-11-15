using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : BaseSlot
{
    public override void Initialize()
    {
        base.Initialize();
    }

    public void EquipItem(BaseSlot targetSlot)
    {
        EquipmentItem equipmentItem = targetSlot.Item as EquipmentItem;
        if(equipmentItem != null)
        {
            if((int)equipmentItem.ItemType == (int)slotType)
            {
                equipmentItem.Equip();
                ExchanageSlot(targetSlot);
            }
            else
            {
                Debug.Log("¿Â¬¯ ΩΩ∑‘¿Ã ¥Ÿ∏®¥œ¥Ÿ.");
            }
        }
        else
        {
            Debug.Log("¿Â∫Ò æ∆¿Ã≈€¿Ã æ∆¥’¥œ¥Ÿ.");
        }
    }
}
