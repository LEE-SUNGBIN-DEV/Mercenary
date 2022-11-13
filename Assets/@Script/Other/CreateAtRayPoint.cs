using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAtRayPoint : MonoBehaviour
{
    public GameObject targetObject;
    public float rayDistance;
    public float genarateInterval;
    private Vector3 rotationOffset;
    private bool isGeneratable;

    private void OnEnable()
    {
        rotationOffset = new Vector3(-90, 0, 0);
        isGeneratable = true;
        StartCoroutine(IntervalGenerate(genarateInterval));
    }

    IEnumerator IntervalGenerate(float interval)
    {
        while(true)
        {
            if(isGeneratable == false)
            {
                yield return new WaitForSeconds(interval);
                isGeneratable = true;
            }
            yield return null;
        }
    }

    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward.normalized * rayDistance, Color.blue, 0.1f);
        if (Physics.Raycast(transform.position, transform.forward.normalized, out hit, rayDistance, LayerMask.GetMask("Terrain")))
        {
            if(isGeneratable)
            {
                EnemyAreaAttack effect = Managers.ObjectPoolManager.RequestObject(Constants.RESOURCE_NAME_EFFECT_BLACK_DRAGON_BREATH_AFTER).GetComponent<EnemyAreaAttack>();
                effect.Owner = GetComponent<EnemyAttack>().Owner;
                effect.transform.position = hit.point;
                effect.transform.rotation = Quaternion.Euler(rotationOffset);
                isGeneratable = false;
            }

            return;
        }
    }
}
