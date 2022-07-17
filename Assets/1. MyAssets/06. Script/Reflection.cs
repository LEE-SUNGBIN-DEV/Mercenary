using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class Reflection : MonoBehaviour
{
    public Quest quest;

    private void Awake()
    {
        Type questType = quest.GetType();

        FieldInfo[] fields = questType.GetFields();
        MethodInfo[] methods = questType.GetMethods();
        PropertyInfo[] properties = questType.GetProperties();

        foreach (var field in fields)
        {
            Debug.Log("Field Name: " + field.Name);
            Debug.Log("Field Type: " + field.FieldType);
            Debug.Log("Field Value: " + field.GetValue(quest));
            Debug.Log("==============================");

        }

        foreach (var method in methods)
        {
            Debug.Log("Method Name: " + method.Name);
            Debug.Log("==============================");
        }

        foreach (var property in properties)
        {
            Debug.Log("Field Name: " + property.Name);
            Debug.Log("Field Type: " + property.PropertyType);
            Debug.Log("Field Value: " + property.GetValue(quest));
            Debug.Log("==============================");
        }

        questType.GetMethod("Reward").Invoke(quest, null);
    }
}
