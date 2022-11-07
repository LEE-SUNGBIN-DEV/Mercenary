using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
    [SerializeField] protected string mapName;
    [SerializeField] protected SCENE_TYPE sceneType;
    [SerializeField] protected SCENE_LIST scene;

    protected virtual void Awake()
    {
        Managers.Instance.Initialize();

        // �̺�Ʈ �ý���
        GameObject eventSystem = GameObject.Find("EventSystem");
        if (eventSystem == null)
        {
            Managers.ResourceManager.InstantiatePrefab("EventSystem");
        }

        sceneType = SCENE_TYPE.UNKNOWN;

        StartCoroutine(GameFunction.WaitAsyncOperation(Managers.Instance.IsLoaded, Initialize));
    }

    public virtual void Initialize()
    {
        Debug.Log("Scene Initialize");
    }

    #region Property
    public string MapName
    {
        get { return mapName; }
    }
    public SCENE_TYPE SceneType
    {
        get { return sceneType; }
    }
    public SCENE_LIST Scene
    {
        get { return scene; }
    }
    #endregion
}
