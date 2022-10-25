using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioComponent : MonoBehaviour
{
    [SerializeField] private int audioAmount;
    [SerializeField] private AudioSource[] sfxPlayers;
    [SerializeField] private AudioContainer[] sfxContainer;

    private void Awake()
    {
        for (int i = 0; i < audioAmount; ++i)
        {
            gameObject.AddComponent<AudioSource>();
        }
        sfxPlayers = GetComponents<AudioSource>();
    }

    public void PlaySFX(string sfxName)
    {
        AudioManager.Instance.PlaySFX(this, sfxName);
    }

    #region Property
    public AudioSource[] SfxPlayers
    {
        get => sfxPlayers;
    }
    public AudioContainer[] SfxContainer
    {
        get => sfxContainer;
    }
    #endregion
}
