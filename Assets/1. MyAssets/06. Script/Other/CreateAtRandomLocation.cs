using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAtRandomLocation : MonoBehaviour
{
    public EFFECT_POOL targetObject;
    public float amount;
    public float interval;
    public float range;
    
    // Public Function
    public void OnEnable()
    {
        StartCoroutine(CreateEveryInterval(interval));
    }

    public virtual IEnumerator CreateEveryInterval(float interval)
    {
        for(int i=0; i<amount; ++i)
        {
            float pointX = Random.Range(-range, range);
            float secondRange = Mathf.Sqrt(range * range - pointX * pointX);
            float pointZ = Random.Range(-secondRange, secondRange);

            GameObject createObject = EffectPoolManager.Instance.RequestObject(targetObject);
            createObject.transform.position = new Vector3(pointX, 0, pointZ);

            yield return new WaitForSeconds(interval);
        }
    }
}
