using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelTable
{
    [SerializeField] private int maxLevel;
    [SerializeField] private int[] levels;
    [SerializeField] private float[] maxExperiences;

    #region Property
    public int MaxLevel
    {
        get { return maxLevel; }
    }
    public int[] Level
    {
        get { return levels; }
    }
    public float[] MaxExperience
    {
        get { return maxExperiences; }
    }
    #endregion
}
