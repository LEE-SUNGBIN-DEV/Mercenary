using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Constants
{
    public static readonly int RESOLUTION_DEFAULT_WIDTH = 1980;
    public static readonly int RESOLUTION_DEFAULT_HEIGHT = 1080;

    public static readonly int NUMBER_SFX_PLAYER = 20;

    public const int MAX_CHARACTER_SLOT_NUMBER = 3;

    public static readonly Vector3[] SELECTION_CHARACTER_POINT
        = new Vector3[MAX_CHARACTER_SLOT_NUMBER]
        {
            new Vector3(34.552f, 0.9f, 8),
            new Vector3(29.552f, 0.9f, 8),
            new Vector3(24.552f, 0.9f, 8)
        };
}
