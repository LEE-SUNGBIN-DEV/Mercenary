using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnEnable()
    {
        ConfirmPanel.onConfirm -= ReturnViliage;
        ConfirmPanel.onConfirm += ReturnViliage;
    }

    public void ReturnViliage()
    {
        ConfirmPanel.onConfirm = null;
        GameManager.Instance.LoadScene(SCENE_LIST.VILIAGE);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            UIManager.Instance.ConfirmPanel.SetConfirmText("������ ���ư��ðڽ��ϱ�?");
            UIManager.Instance.OpenPanel(PANEL_TYPE.CONFIRM);
        }
    }
}
