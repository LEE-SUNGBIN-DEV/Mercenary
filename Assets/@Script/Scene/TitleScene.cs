using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    protected override void Awake()
    {
        Managers.GameManager.Initialize();
    }
}
