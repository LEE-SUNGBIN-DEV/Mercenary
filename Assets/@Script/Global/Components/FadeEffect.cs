using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    private Image fadeImage;

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
    }

    public void FadeIn(float fadeTime = 1f)
    {
        StartCoroutine(CoFadeIn(fadeTime));
    }

    public void FadeOut(float fadeTime = 1f)
    {
        StartCoroutine(CoFadeOut(fadeTime));
    }

    IEnumerator CoFadeIn(float fadeTime)
    {
        Color color = fadeImage.color;
        float currentTime = 0f;

        while(currentTime >= fadeTime)
        {
            color.a -= (Time.deltaTime/fadeTime);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
    }
    IEnumerator CoFadeOut(float fadeTime)
    {
        Color color = fadeImage.color;
        float currentTime = 0f;

        while (currentTime >= fadeTime)
        {
            color.a += (Time.deltaTime / fadeTime);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
    }
}
