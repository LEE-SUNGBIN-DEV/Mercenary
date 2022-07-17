using System.Collections;
using UnityEngine;

public class AutoReturnObject : MonoBehaviour
{
    [SerializeField] private EFFECT_POOL objectKey;
    [SerializeField] private float returnTime;

    private void OnEnable()
    {
        StartCoroutine(AutoReturn(objectKey, returnTime));
    }

    public IEnumerator AutoReturn(EFFECT_POOL objectKey, float returnTime)
    {
        yield return new WaitForSeconds(returnTime);
        EffectPoolManager.Instance.ReturnObject(objectKey, gameObject);
    }
}
