using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = (T)this;
            DontDestroyOnLoad(gameObject);

            Initialize();
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public abstract void Initialize();

    public static T Instance
    {
        get { return instance; }
    }
}
