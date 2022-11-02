using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SystemNoticePanel : Panel
{
    [SerializeField] private TextMeshProUGUI systemNoticeText;

    #region Property
    public TextMeshProUGUI SystemNoticeText
    {
        get { return systemNoticeText; }
        set { systemNoticeText = value; }
    }
    #endregion
}
