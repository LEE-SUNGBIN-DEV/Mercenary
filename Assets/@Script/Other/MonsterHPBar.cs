using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// =================== MONSTER HP BAR CLASS ============================================
// ������ ü�¹ٸ� ���� ��ũ���� �����ִ� Ŭ����
// =====================================================================================

public class MonsterHPBar : MonoBehaviour
{
    private Slider sliderMonsterHP;

    private Camera screenCamera;
    private Canvas canvas;

    private RectTransform parentRectTransform;
    private RectTransform rectTransform;

    private Vector3 offset;
    private Transform targetTransform;

    private void Awake()
    {
        sliderMonsterHP = GetComponent<Slider>();
        
        canvas = GetComponentInParent<Canvas>();
        screenCamera = canvas.worldCamera;
        parentRectTransform = canvas.GetComponent<RectTransform>();

        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        // ���� ��ǥ�� ��ũ�� ��ǥ�� ��ȯ
        Vector3 screenPosition
            = Camera.main.WorldToScreenPoint(targetTransform.position + offset);

        // �þ� �ݴ��� �ִ� HP Bar�� ���̴� ���� ����
        if(screenPosition.z < 0.0f)
        {
            screenPosition *= -1.0f;
        }

        Vector2 localPosition = Vector2.zero;

        // ��ũ�� ��ǥ�� ĵ���� ��ǥ�� ��ȯ
        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (parentRectTransform
            , screenPosition
            , screenCamera
            , out localPosition);

        rectTransform.localPosition = localPosition;
    }

    public void UpdateMonsterHP(float maxHP, float currentHP)
    {
        sliderMonsterHP.minValue = 0;
        sliderMonsterHP.maxValue = maxHP;
        sliderMonsterHP.value = currentHP;
    }

    #region Property
    public Vector3 Offset
    {
        get { return offset; }
        set { offset = value; }
    }

    public Transform TargetTransform
    {
        get { return targetTransform; }
        set { targetTransform = value; }
    }
    #endregion
}
