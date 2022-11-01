using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCamera : MonoBehaviour
{
    [SerializeField] private Camera thisCamera;
    [SerializeField] private Transform targetTransform;
    private Vector3 originalPosition;
    private IEnumerator shakeCoroutine;

    protected void Awake()
    {
        thisCamera = GetComponent<Camera>();
    }

    public void SetCameraTransform(Transform targetTransform)
    {
        gameObject.SetTransform(targetTransform);
    }

    public void ShakeCamera(float shakeTime, float shakeIntensity = 0.05f)
    {
        originalPosition = transform.position;
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
        }

        shakeCoroutine = CoShakeCamera(shakeTime, shakeIntensity);
        StartCoroutine(shakeCoroutine);
    }

    public IEnumerator CoShakeCamera(float shakeTime, float shakeIntensity)
    {
        float cumulativeTime = 0f;

        while(cumulativeTime < shakeTime)
        {
            cumulativeTime += Time.deltaTime;
            transform.position = (Random.insideUnitSphere * shakeIntensity) + originalPosition;
            yield return null;
        }
        transform.position = originalPosition;
    }

    #region Property
    public Camera ThisCamera
    {
        get { return thisCamera; }
    }
    public Transform TargetTransform
    {
        get { return targetTransform; }
    }
    public Vector3 OriginalPosition
    {
        get { return originalPosition; }
        set { originalPosition = value; }
    }
    #endregion
}
