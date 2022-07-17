using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReturnPanel : Panel
{
    public static event UnityAction onReturnViliage;
    public void ReturnViliage()
    {
        onReturnViliage();
    }
}
