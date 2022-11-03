using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReturnPopup : Popup
{
    private SCENE_LIST returnScene;

    public void ReturnViliage()
    {
        Managers.GameSceneManager.LoadScene(returnScene);
    }

    #region Property
    public SCENE_LIST ReturnScene
    {
        get { return returnScene; }
    }
    #endregion
}
