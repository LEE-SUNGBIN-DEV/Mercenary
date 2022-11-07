using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    [Header("Characters")]
    [SerializeField] private CharacterData[] characterDatas;

    [Header("Option")]
    [SerializeField] private float bgmVolume;
    [SerializeField] private float sfxVolume;

    #region Property
    public CharacterData[] CharacterDatas
    {
        get { return characterDatas; }
    }
    #endregion
}
