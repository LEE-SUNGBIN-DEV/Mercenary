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
        Managers.GameSceneManager.LoadScene(SCENE_LIST.Forestia);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Managers.UIManager.ConfirmPanel.SetConfirmText("마을로 돌아가시겠습니까?");
            Managers.UIManager.OpenPanel(PANEL_TYPE.CONFIRM);
        }
    }
}
