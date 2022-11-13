using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static partial class Functions
{
    public static Color SetColor(Color color, float alpha = 1f)
    {
        Color targetColor = color;
        targetColor.a = alpha;

        return targetColor;
    }
    #region Async Operation
    public static IEnumerator WaitAsyncOperation(System.Func<bool> isLoaded, UnityAction callback = null)
    {
        while (!isLoaded.Invoke())
        {
            yield return null;
        }

        callback?.Invoke();
    }
    #endregion

    public static T GetOrAddComponent<T>(GameObject gameObject) where T : Component
    {
        T targetComponent = gameObject.GetComponent<T>();

        if (targetComponent == null)
        {
            targetComponent = gameObject.AddComponent<T>();
        }

        return targetComponent;
    }

    public static T FindChild<T>(GameObject gameObject, string name = null, bool recursive = false) where T : Object
    {
        if (gameObject == null)
        {
            return null;
        }

        if (recursive == false)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Transform transform = gameObject.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();

                    if (component != null)
                    {
                        return component;
                    }
                }
            }
        }

        else
        {
            foreach (T component in gameObject.GetComponentsInChildren<T>(true))
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }
            }
        }

        return null;
    }
}
