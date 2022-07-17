using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// =================== AUDIO MANAGER CLASS (Singleton) =================================
// �����, ȿ���� ���ҽ��� �����ϰ� ��� ���ִ� Ŭ����
// =====================================================================================

[System.Serializable]
public class AudioContainer
{
    public string name;
    public AudioClip audioClip;
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] GameObject sfxPlayerObject;
    [SerializeField] AudioSource bgmPlayer;        
    [SerializeField] AudioSource[] sfxPlayers;      
    [SerializeField] AudioContainer[] bgmContainer;
    [SerializeField] AudioContainer[] sfxContainer;
    [SerializeField] Slider bgmSlider;             
    [SerializeField] Slider sfxSlider;

    public override void Initialize()
    {
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

    public void PlaySFX(string sfxName)
    {
        foreach (AudioContainer audioContainer in sfxContainer)
        {
            if(audioContainer.name == sfxName)
            {
                for (int i = 0; i < sfxPlayers.Length; ++i)
                {
                    // ��� ������ ���� sfx �÷��̾ �ִٸ�
                    if(!sfxPlayers[i].isPlaying)
                    {
                        sfxPlayers[i].volume = sfxSlider.value; // ���� ����
                        sfxPlayers[i].clip = audioContainer.audioClip;
                        sfxPlayers[i].Play();
                        return;
                    }
                }
                Debug.Log("Notice: All of SFX Player is Playing");
            }
        }
    }

    public void PlayBGM(string sceneName)
    {
        bgmPlayer.Stop();
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmSlider.value;

        for(int i=0; i<bgmContainer.Length; ++i)
        {
            if(bgmContainer[i].name == sceneName)
            {
                bgmPlayer.clip = bgmContainer[i].audioClip;
                bgmPlayer.Play();

                return;
            }
        }
    }
    public void StopBGM()
    {
        bgmPlayer.Stop();
    }
}
