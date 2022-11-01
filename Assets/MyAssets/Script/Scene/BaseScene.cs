using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
    private SCENE_TYPE sceneType;

    protected virtual void Awake()
    {
        sceneType = SCENE_TYPE.UNKNOWN;
    }
}
