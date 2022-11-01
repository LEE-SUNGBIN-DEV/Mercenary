using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager
{
    private GameObject rootObject;
    private Dictionary<string, ObjectPool> objectPoolDictionary;
    [SerializeField] private ObjectPool[] objectPools;

    public void Initialize(GameObject rootObject)
    {
        this.rootObject = rootObject;
        ObjectPoolDictionary = new Dictionary<string, ObjectPool>();

        foreach (ObjectPool objectPool in ObjectPools)
        {
            objectPool.Initialize(this.rootObject);
            ObjectPoolDictionary.Add(objectPool.key, objectPool);
        }
    }

    public GameObject RequestObject(string key)
    {
        ObjectPool objectPool = ObjectPoolDictionary[key];

        if (objectPool != null)
        {
            GameObject requestObject;

            // 사용할 수 있는 오브젝트가 있을 경우
            if (objectPool.queue.Count > 0)
            {
                requestObject = objectPool.queue.Dequeue();
            }

            // 모두 사용중일 경우
            else
            {
                requestObject = GameObject.Instantiate(objectPool.value, rootObject.transform);
            }
            requestObject.SetActive(true);

            return requestObject;
        }

        return null;
    }

    public GameObject RequestObject(string key, Vector3 position)
    {
        ObjectPool objectPool = ObjectPoolDictionary[key];

        if (objectPool != null)
        {
            GameObject requestObject;

            // 사용할 수 있는 오브젝트가 있을 경우
            if (objectPool.queue.Count > 0)
            {
                requestObject = objectPool.queue.Dequeue();
            }

            // 모두 사용중일 경우
            else
            {
                requestObject = GameObject.Instantiate(objectPool.value, rootObject.transform);
            }

            requestObject.transform.position = position;
            requestObject.SetActive(true);

            return requestObject;
        }

        return null;
    }

    public void ReturnObject(string key, GameObject returnObject)
    {
        ObjectPool objectPool = ObjectPoolDictionary[key];

        if (objectPool != null)
        {
            returnObject.SetActive(false);
            objectPool.queue.Enqueue(returnObject);
            return;
        }

        return;
    }

    #region Property
    public Dictionary<string, ObjectPool> ObjectPoolDictionary
    {
        get { return objectPoolDictionary; }
        private set { objectPoolDictionary = value; }
    }
    public ObjectPool[] ObjectPools
    {
        get { return objectPools; }
        private set { objectPools = value; }
    }
    #endregion
}
