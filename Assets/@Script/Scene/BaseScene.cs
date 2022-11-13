using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
    [SerializeField] protected string mapName;
    [SerializeField] protected SCENE_TYPE sceneType;
    [SerializeField] protected SCENE_LIST scene;
    [SerializeField] protected Vector3 spawnPosition;

    protected virtual void Awake()
    {
        Managers.Instance.Initialize();

        // 이벤트 시스템
        GameObject eventSystem = GameObject.Find("EventSystem");
        if (eventSystem == null)
        {
            Managers.ResourceManager.InstantiatePrefabSync("Prefab_EventSystem");
        }

        sceneType = SCENE_TYPE.UNKNOWN;

        Initialize();
    }

    public virtual void Initialize()
    {
        Managers.GameSceneManager.CurrentScene = this;
    }

    public virtual void ExitScene()
    {
        Managers.GameSceneManager.CurrentScene = null;
    }

    #region Property
    public string MapName { get { return mapName; } }
    public SCENE_TYPE SceneType { get { return sceneType; } }
    public SCENE_LIST Scene { get { return scene; } }
    public Vector3 SpawnPosition { get {  return spawnPosition; } }
    #endregion
}
