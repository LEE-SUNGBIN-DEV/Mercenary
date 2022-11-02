using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public static class ExtensionMethod
{
    #region GameObject
    public static void ToggleActive(this GameObject gameObject)
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }

        else
        {
            gameObject.SetActive(true);
        }
    }
    public static void SetTransform(this GameObject gameObject, Transform targetTransform)
    {
        gameObject.transform.position = targetTransform.position;
        gameObject.transform.rotation = targetTransform.rotation;
    }
    public static void SetTransform(this GameObject gameObject, GameObject targetObject)
    {
        gameObject.SetTransform(targetObject.transform);
    }
    #endregion

    #region Transform
    public static Transform GetRootTransform(this Transform currentTransform)
    {
        while (currentTransform.parent != null)
        {
            currentTransform = currentTransform.parent;
        }

        return currentTransform;
    }
    #endregion

    public static string GetEnumName<T>(this T targetEnum) where T : System.Enum
    {
        string enumName = System.Enum.GetName(typeof(T), targetEnum);

        return enumName;
    }

    public static bool IsBetween(this float value, float min, float max)
    {
        if (value > min && value < max)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
