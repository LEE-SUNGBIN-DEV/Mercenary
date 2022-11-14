using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoticePanel : UIPanel
{
    public enum TEXT
    {
        NoticeText
    }

    private bool isNotice;
    private float noticeTime;
    private Queue<string> systemNoticeQueue = new Queue<string>();

    public override void Initialize()
    {
        if (isInitialized == true)
        {
            Debug.Log($"{this}: Already Initialized.");
            return;
        }
        isInitialized = true;

        BindText(typeof(TEXT));

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

            GetText((int)TEXT.NoticeText).text = systemNoticeQueue.Dequeue();
            GetText((int)TEXT.NoticeText).gameObject.SetActive(true);

            if (noticeTime >= Constants.TIME_CLIENT_NOTICE)
            {
                isNotice = false;
                noticeTime = 0f;
                GetText((int)TEXT.NoticeText).gameObject.SetActive(false);
            }
        }
    }

    public void AcceptRequest(string content)
    {
        systemNoticeQueue.Enqueue(content);
    }
}
