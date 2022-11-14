using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIPanel : UIBase
{
    protected bool isInitialized = false;

    private void Awake()
    {
        Initialize();
    }
    public abstract void Initialize();
}
