using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject root = GameObject.Find(typeof(T).Name);
                if (root == null)
                {
                    root = new GameObject(typeof(T).Name);
                }

                instance = Functions.GetOrAddComponent<T>(root);
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            GameObject root = GameObject.Find(typeof(T).Name);
            if (root == null)
            {
                root = new GameObject(typeof(T).Name);
            }

            instance = Functions.GetOrAddComponent<T>(root);
            DontDestroyOnLoad(instance.gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public abstract void Initialize();
}
