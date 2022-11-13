using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioComponent : MonoBehaviour
{
    [SerializeField] private int audioAmount;
    [SerializeField] private AudioSource[] sfxPlayers;

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
        Managers.AudioManager.PlaySFX(sfxPlayers, sfxName);
    }

    public AudioSource[] SfxPlayers { get { return sfxPlayers; } }
}
