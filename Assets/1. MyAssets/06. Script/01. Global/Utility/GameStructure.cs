using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioContainer
{
    public string name;
    public AudioClip audioClip;
}

[System.Serializable]
public class ObjectPool
{
    public GameObject value;
    public int amount;
    public Queue<GameObject> queue = new Queue<GameObject>();

    public void Initialize(GameObject parent)
    {
        for (int i = 0; i < amount; ++i)
        {
            GameObject poolObject = GameObject.Instantiate(value, parent.transform);
            poolObject.SetActive(false);
            queue.Enqueue(poolObject);
        }
    }
}