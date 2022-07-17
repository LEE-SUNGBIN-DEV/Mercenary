using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLightningField : CreateAtRandomLocation
{
    [SerializeField] private Monster owner;
    private Vector3 offset;

    private void Awake()
    {
        offset = Vector3.zero;
    }

    public override IEnumerator CreateEveryInterval(float interval)
    {
        offset = transform.position;

        for (int i = 0; i < amount; ++i)
        {
            float pointX = Random.Range(-range, range);
            float secondRange = Mathf.Sqrt(range * range - pointX * pointX);
            float pointZ = Random.Range(-secondRange, secondRange);

            GameObject createObject = EffectPoolManager.Instance.RequestObject(targetObject);
            createObject.transform.position = new Vector3(offset.x + pointX, 0, offset.z + pointZ);
            createObject.GetComponent<MonsterLightningStrike>().Owner = Owner;

            yield return new WaitForSeconds(interval);
        }
    }

    #region Property
    public Monster Owner
    {
        get { return owner; }
        set
        {
            if (value is Monster)
            {
                owner = value;
            }
        }
    }
    #endregion
}
