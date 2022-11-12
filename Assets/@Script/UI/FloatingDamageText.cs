using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingDamageText : MonoBehaviour
{
    [SerializeField] private float textSpeed;
    [SerializeField] private float alphaSpeed;
    private TextMeshProUGUI damageText;
    private Color textColor;
    private Vector3 offset;

    private void Awake()
    {
        damageText = GetComponent<TextMeshProUGUI>();
        textColor = damageText.color;
        offset = Vector3.up;
    }

    private void OnEnable()
    {
        transform.SetParent(Managers.UIManager.Canvas.transform);
        textColor.a = 1f;
    }

    private void Update()
    {
        transform.Translate(new Vector3(0, textSpeed * Time.deltaTime, 0));

        textColor.a = Mathf.Lerp(textColor.a, 0, alphaSpeed * Time.deltaTime);
        damageText.color = textColor;
    }

    public void SetDamageText(bool isCritical, float damage, Vector3 worldPosition)
    {
        transform.position = Managers.GameManager.PlayerCamera.ThisCamera.WorldToScreenPoint(worldPosition) + offset;

        if(isCritical == true)
        {
            textColor = Color.red;
        }

        else
        {
            textColor = Color.white;
        }

        damageText.text = damage.ToString("F0");
    }
}
