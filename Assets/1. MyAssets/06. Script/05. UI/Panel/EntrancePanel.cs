using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntrancePanel : Panel
{
    [SerializeField] private TextMeshProUGUI entranceText;
    [SerializeField] private float activeTime;

    private void OnEnable()
    {
        StartCoroutine(AutoDisable());
    }

    IEnumerator AutoDisable()
    {
        float time = 0f;

        while(time <= activeTime)
        {
            time += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }

    #region Property
    public TextMeshProUGUI EntranceText
    {
        get { return entranceText; }
        private set { entranceText = value; }
    }
    #endregion
}
