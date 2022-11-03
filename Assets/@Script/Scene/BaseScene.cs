using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
    [SerializeField] private string mapName;
    [SerializeField] private SCENE_TYPE sceneType;
    [SerializeField] private SCENE_LIST scene;

    protected virtual void Awake()
    {
        sceneType = SCENE_TYPE.UNKNOWN;
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
