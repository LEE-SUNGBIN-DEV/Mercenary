using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager
{
    private Dictionary<string, Object> resourceDictionary;
    private Dictionary<string, AsyncOperationHandle> asyncHandleDictionary;

    public ResourceManager()
    {
        resourceDictionary = new Dictionary<string, Object>();
        asyncHandleDictionary = new Dictionary<string, AsyncOperationHandle>();
    }

    public void Initialize()
    {
        
    }

    public void LoadResourceAsync<T>(string key, UnityAction<T> callback = null) where T : Object
    {
        if(resourceDictionary.TryGetValue(key, out Object resource) == true)
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
