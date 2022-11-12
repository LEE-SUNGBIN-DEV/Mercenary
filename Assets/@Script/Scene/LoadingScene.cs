using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : BaseScene
{
    static private string nextSceneName;    // 전환 요청이 들어온 씬
    [SerializeField] private Slider loadingBar;

    private void Start()
    {
        StartCoroutine(LoadSceneProgress());
    }

    public override void Initialize()
    {
        base.Initialize();
        sceneType = SCENE_TYPE.LOADING;
        scene = SCENE_LIST.Loading;
    }

    private IEnumerator LoadSceneProgress()
    {
        AsyncOperation loadingProgress = SceneManager.LoadSceneAsync(nextSceneName);
        loadingProgress.allowSceneActivation = false;

        Managers.GameSceneManager.FadeEffect.SetAlpha(0f);

        float timer = 0.0f;
        while (loadingProgress.isDone == false)
        {
            yield return null;

            if (loadingProgress.progress < 0.9f)
            {
                loadingBar.value = loadingProgress.progress;
            }

            else
            {
                timer += Time.unscaledDeltaTime;
                loadingBar.value = Mathf.Lerp(0.9f, 1f, timer);

                if (loadingBar.value >= 1.0f)
                {
                    loadingProgress.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    // Public Function
    public static void LoadScene(string sceneName)
    {
        nextSceneName = sceneName;
        SceneManager.LoadScene("Loading");
    }
}
