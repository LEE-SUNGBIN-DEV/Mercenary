using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager
{
    private Dictionary<string, Object> resourceDictionary = new Dictionary<string, Object>();
    private Dictionary<string, AsyncOperationHandle> asyncHandleDictionary = new Dictionary<string, AsyncOperationHandle>();

    public void Initialize()
    {
        
    }
    
    #region Load Sync
    public GameObject InstantiatePrefabSync(string key, Transform parent = null)
    {
        GameObject targetPrefab = LoadResourceSync<GameObject>(key);
        GameObject newPrefab = GameObject.Instantiate(targetPrefab, parent);
        newPrefab.name = targetPrefab.name;
        newPrefab.transform.localPosition = targetPrefab.transform.position;

        return newPrefab;
    }
    public T LoadResourceSync<T>(string key) where T : Object
    {
        if (resourceDictionary.TryGetValue(key, out Object resource))
        {
            return resource as T;
        }
        else
        {
            var handler = Addressables.LoadAssetAsync<T>(key);
            resource = handler.WaitForCompletion();
            resourceDictionary.Add(key, resource);

            return resource as T;
        }
        
    }
    #endregion

    #region Load Async
    public void InstantiatePrefabAsync(string key, Transform parent = null, UnityAction<GameObject> callback = null)
    {
        LoadResourceAsync<GameObject>(key, (GameObject targetPrefab) =>
        {
            GameObject newObject = GameObject.Instantiate(targetPrefab, parent);
            newObject.name = targetPrefab.name;
            newObject.transform.localPosition = targetPrefab.transform.position;
            callback?.Invoke(newObject);
        });
    }
    public void LoadResourceAsync<T>(string key, UnityAction<T> callback = null) where T : Object
    {
        if (resourceDictionary.TryGetValue(key, out Object resource) == true)
        {
            callback?.Invoke(resource as T);
            return;
        }

        else if (asyncHandleDictionary.ContainsKey(key) == true)
        {
            asyncHandleDictionary[key].Completed += (AsyncOperationHandle asyncHandle) =>
            {
                callback?.Invoke(asyncHandle.Result as T);
            };
            return;
        }

        else
        {
            asyncHandleDictionary.Add(key, Addressables.LoadAssetAsync<T>(key));
            asyncHandleDictionary[key].Completed += (AsyncOperationHandle asyncHandle) =>
            {
                resourceDictionary.Add(key, asyncHandle.Result as Object);
                callback?.Invoke(asyncHandle.Result as T);
            };
        }
    }
    #endregion


    public void ReleaseResource(string key)
    {
        if (resourceDictionary.TryGetValue(key, out Object value) == true)
        {
            resourceDictionary.Remove(key);
        }

        if(asyncHandleDictionary.TryGetValue(key, out AsyncOperationHandle asyncHandle) == true)
        {
            Addressables.Release(asyncHandle);
            asyncHandleDictionary.Remove(key);
        }
    }

    public void ClearResources()
    {
        resourceDictionary.Clear();

        foreach(var asyncHandle in asyncHandleDictionary.Values)
        {
            Addressables.Release(asyncHandle);
        }
        asyncHandleDictionary.Clear();
    }
}
