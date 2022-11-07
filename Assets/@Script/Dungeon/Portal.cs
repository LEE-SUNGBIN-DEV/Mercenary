using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public void ReturnViliage()
    {
        ConfirmPopup.OnConfirm -= ReturnViliage;
        Managers.GameSceneManager.LoadSceneAsync(SCENE_LIST.Forestia);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Managers.UIManager.RequestConfirm("������ ���ư��ðڽ��ϱ�?", ReturnViliage);
        }
    }
}
