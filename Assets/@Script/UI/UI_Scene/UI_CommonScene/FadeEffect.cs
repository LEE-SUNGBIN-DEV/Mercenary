using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FadeEffect : MonoBehaviour
{
    private Image fadeImage;

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
    }

    public void SetAlpha(float alpha)
    {
        fadeImage.color = Functions.SetColor(fadeImage.color, alpha);
    }

    public void FadeIn(float fadeTime = 1f, UnityAction callback = null)
    {
        StartCoroutine(CoFadeIn(fadeTime, callback));
    }

    public void FadeOut(float fadeTime = 1f, UnityAction callback = null)
    {
        StartCoroutine(CoFadeOut(fadeTime, callback));
    }

    IEnumerator CoFadeIn(float fadeTime, UnityAction callback)
    {
        Color color = fadeImage.color;
        float currentTime = 0f;

        while(currentTime <= fadeTime)
        {
            currentTime += Time.deltaTime;
            color.a -= (currentTime / fadeTime);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;

        callback?.Invoke();
    }
    IEnumerator CoFadeOut(float fadeTime, UnityAction callback)
    {
        Color color = fadeImage.color;
        float currentTime = 0f;

        while (currentTime <= fadeTime)
        {
            currentTime += Time.deltaTime;
            color.a += (currentTime / fadeTime);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;

        callback?.Invoke();
    }
}
