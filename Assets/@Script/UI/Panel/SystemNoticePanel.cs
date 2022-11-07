using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SystemNoticePanel : UIPanel
{
    [SerializeField] private TextMeshProUGUI systemNoticeText;
    private bool isNotice;
    private float noticeTime;
    private Queue<string> systemNoticeQueue = new Queue<string>();

    private void Awake()
    {
        Managers.UIManager.OnRequestNotice -= AcceptRequest;
        Managers.UIManager.OnRequestNotice += AcceptRequest;

        isNotice = false;
        noticeTime = 0f;
    }

    private void Update()
    {
        if (isNotice == false && systemNoticeQueue.Count != 0)
        {
            isNotice = true;
            noticeTime += Time.deltaTime;

            SystemNoticeText.text = systemNoticeQueue.Dequeue();
            Managers.UIManager.OpenPanel(PANEL.SystemNoticePanel);

            if (noticeTime >= GameConstants.TIME_CLIENT_NOTICE)
            {
                isNotice = false;
                noticeTime = 0f;
                Managers.UIManager.ClosePanel(PANEL.SystemNoticePanel);
            }
        }
    }

    public void AcceptRequest(string content)
    {
        systemNoticeQueue.Enqueue(content);
    }

    #region Property
    public TextMeshProUGUI SystemNoticeText
    {
        get { return systemNoticeText; }
        set { systemNoticeText = value; }
    }
    #endregion
}
