using System.Collections;
using UnityEngine;

public class AutoReturnObject : MonoBehaviour
{
    [SerializeField] private string key;
    [SerializeField] private float returnTime;

    private void OnEnable()
    {
        StartCoroutine(AutoReturn(key, returnTime));
    }

    public IEnumerator AutoReturn(string key, float returnTime)
    {
        yield return new WaitForSeconds(returnTime);
        Managers.ObjectPoolManager.ReturnObject(key, gameObject);
    }
}
