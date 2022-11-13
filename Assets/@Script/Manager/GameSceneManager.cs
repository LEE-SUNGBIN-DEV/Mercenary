using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameSceneManager
{
    public event UnityAction OnSceneExit;
    public event UnityAction OnSceneEnter;

    private BaseScene currentScene;
    private FadeEffect fadeEffect;

    public void Initialize()
    {
        SceneManager.sceneLoaded += SceneEnter;
        SceneManager.sceneUnloaded += SceneExit;
        fadeEffect = Managers.UIManager.Canvas.GetComponentInChildren<FadeEffect>();
    }

    public void SceneEnter(Scene scene, LoadSceneMode loadMode)
    {
        fadeEffect.FadeIn(1.5f);
        OnSceneEnter?.Invoke();
    }
    public void SceneExit(Scene scene)
    {
        OnSceneExit?.Invoke();
        currentScene.ExitScene();
    }

    public string GetSceneName(SCENE_LIST sceneList)
    {
        return sceneList.GetEnumName();
    }

    // Load Scene Fade
    public void LoadScene(string sceneName)
    {
        fadeEffect.FadeOut(1.5f, () => { SceneManager.LoadScene(sceneName); });
    }
    public void LoadScene(SCENE_LIST requestScene)
    {
        LoadScene(requestScene.GetEnumName());
    }

    // Load Scene Async 
    public void LoadSceneAsync(string sceneName)
    {
        fadeEffect.FadeOut(1.5f, () =>
        {
            LoadingScene.LoadScene(sceneName);
        });
    }
    public void LoadSceneAsync(SCENE_LIST requestScene)
    {
        LoadSceneAsync(requestScene.GetEnumName());
    }

    #region Property
    public BaseScene CurrentScene { get { return currentScene; } set { currentScene = value; } }
    public FadeEffect FadeEffect { get { return fadeEffect; } }
    #endregion
}
