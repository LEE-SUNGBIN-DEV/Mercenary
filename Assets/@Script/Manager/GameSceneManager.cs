using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSceneManager
{
    public event UnityAction OnLoadScene;
    public event UnityAction OnSceneLoaded;

    private BaseScene currentScene;

    public void Initialize()
    {
    }

    public string GetSceneName(SCENE_LIST sceneList)
    {
        return sceneList.GetEnumName();
    }

    public void LoadSceneFade(string sceneName)
    {

        OnLoadScene?.Invoke();
    }
    public void LoadSceneAsync(string sceneName)
    {
        OnLoadScene?.Invoke();
        LoadingManager.LoadScene(sceneName);
    }

    public void LoadSceneAsync(SCENE_LIST requestScene)
    {
        LoadSceneAsync(requestScene.GetEnumName());
    }

    #region Property
    public BaseScene CurrentScene
    {
        get { return currentScene; }
        set { currentScene = value; }
    }
    #endregion
}
