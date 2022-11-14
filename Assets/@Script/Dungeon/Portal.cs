using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public void ReturnViliage()
    {
        Managers.UIManager.UICommonScene.ConfirmPanel.OnConfirm -= ReturnViliage;
        Managers.GameSceneManager.LoadSceneAsync(SCENE_LIST.Forestia);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Managers.UIManager.RequestConfirm("마을로 돌아가시겠습니까?", ReturnViliage);
        }
    }
}
