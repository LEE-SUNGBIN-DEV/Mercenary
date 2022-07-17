using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public static class ExtensionMethod
{
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

    public static Transform GetRootTransform(this Transform currentTransform)
    {
        while (currentTransform.parent != null)
        {
            currentTransform = currentTransform.parent;
        }

        return currentTransform;
    }

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

    /*
    public static T DeepCopy<T>(this T target) where T : new()
    {
        Type type = target.GetType();
        
        if (type.IsClass)
        {
            T clone = new T();
            FieldInfo[] fields = type.GetFields();
            PropertyInfo[] properties = type.GetProperties();

            foreach (FieldInfo field in fields)
            {
                field.SetValue(clone, field.GetValue(target).DeepCopy());
            }

            foreach(PropertyInfo property in properties)
            {
                property.SetValue(clone, property.GetValue(target).DeepCopy());
            }

            return clone;
        }
        
        return target;
    }
    */
}
