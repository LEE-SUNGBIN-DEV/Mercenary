using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    [Header("Characters")]
    [SerializeField] private Character[] characters;

    [Header("Option")]
    [SerializeField] private float bgmVolume;
    [SerializeField] private float sfxVolume;
}
