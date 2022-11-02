using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private const float gravityConstant = 9.8f;
    private Vector3 offset = new Vector3(0, 0.5f, 0);

    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position + offset, -transform.up * 0.6f, Color.black, 0.1f);
        if (Physics.Raycast(transform.position + offset, -transform.up * 0.6f, out hit, LayerMask.GetMask("Floor")))
        {
            return;
        }
        else
        {
            Vector3 updatePosition = transform.position;
            updatePosition.y -= gravityConstant * Time.deltaTime;
            transform.position = updatePosition;
        }
    }
}
