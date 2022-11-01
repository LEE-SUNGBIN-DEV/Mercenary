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
        Managers.GameManager.LoadScene(SCENE_LIST.VILIAGE);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Managers.UIManager.ConfirmPanel.SetConfirmText("������ ���ư��ðڽ��ϱ�?");
            Managers.UIManager.OpenPanel(PANEL_TYPE.CONFIRM);
        }
    }
}
