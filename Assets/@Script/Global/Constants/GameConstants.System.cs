using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class GameConstants
{
    public static readonly int RESOLUTION_DEFAULT_WIDTH = 1980;
    public static readonly int RESOLUTION_DEFAULT_HEIGHT = 1080;

    public const int MAX_CHARACTER_SLOT_NUMBER = 3;


    public static readonly Vector3[] SELECTION_CHARACTER_POINT
        = new Vector3[MAX_CHARACTER_SLOT_NUMBER]
        {
            new Vector3(-5, 5.09f, 17),
            new Vector3(-2.5f, 5.09f, 17),
            new Vector3(0, 5.09f, 17)
        };
}
