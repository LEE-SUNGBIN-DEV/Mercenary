using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager
{
    private AudioSource bgmPlayer;        
    private AudioSource[] sfxPlayers;
    private Slider bgmSlider;             
    private Slider sfxSlider;

    public void Initialize(Transform rootTransform)
    {
        // BGM Player
        GameObject bgmPlayerObject = GameObject.Find("BGM Player");
        if(bgmPlayerObject == null)
        {
            bgmPlayerObject = new GameObject("BGM Player");
        }
        bgmPlayerObject.transform.SetParent(rootTransform);
        bgmPlayer = bgmPlayerObject.AddComponent<AudioSource>();

        // SFX Player
        GameObject sfxPlayerObject = GameObject.Find("SFX Player");
        if (sfxPlayerObject == null)
        {
            sfxPlayerObject = new GameObject("SFX Player");
        }
        sfxPlayerObject.transform.SetParent(rootTransform);
        for (int i = 0; i < Constants.NUMBER_SFX_PLAYER; ++i)
        {
            sfxPlayerObject.gameObject.AddComponent<AudioSource>();
        }
        sfxPlayers = sfxPlayerObject.GetComponents<AudioSource>();
    }

    public void SetBGMVolume()
    {
        bgmPlayer.volume = bgmSlider.value;
    }

    public void SetSFXVolume()
    {
        for (int i = 0; i < sfxPlayers.Length; ++i)
        {
            sfxPlayers[i].volume = bgmSlider.value;
        }
    }

    public void PlaySFX(AudioSource[] audioPlayers, string sfxName)
    {
        AudioClip targetClip = Managers.ResourceManager.LoadResourceSync<AudioClip>(sfxName);

        for (int i = 0; i < audioPlayers.Length; ++i)
        {
            if (!audioPlayers[i].isPlaying)
            {
                //audioPlayers[i].volume = sfxSlider.value; // 볼륨 설정
                audioPlayers[i].clip = targetClip;
                audioPlayers[i].Play();
                return;
            }
        }
        Debug.Log("알림: 모든 오디오 플레이어가 동작중입니다.");
    }
    public void PlaySFX(string sfxName)
    {
        PlaySFX(sfxPlayers, sfxName);
    }

    public void PlayBGM(string sceneName)
    {
        AudioClip targetClip = Managers.ResourceManager.LoadResourceSync<AudioClip>(sceneName);

        bgmPlayer.Stop();
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmSlider.value;
        bgmPlayer.clip = targetClip;
        bgmPlayer.Play();
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }
}
