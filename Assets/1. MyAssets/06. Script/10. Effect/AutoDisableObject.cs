using System.Collections;
using UnityEngine;

public class AutoDisableObject : MonoBehaviour
{
    // Private Variable
    [SerializeField] private float disableTime;

    // Private Function
    private void OnEnable()
    {
        StartCoroutine(AutoDisable(disableTime));
    }

    private IEnumerator AutoDisable(float returnTime)
    {
        yield return new WaitForSeconds(returnTime);
        gameObject.SetActive(false);
    }
}
