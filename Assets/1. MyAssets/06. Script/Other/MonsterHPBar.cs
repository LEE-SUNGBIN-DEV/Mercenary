using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// =================== MONSTER HP BAR CLASS ============================================
// 몬스터의 체력바를 유저 스크린에 보여주는 클래스
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
        // 월드 좌표를 스크린 좌표로 변환
        Vector3 screenPosition
            = Camera.main.WorldToScreenPoint(targetTransform.position + offset);

        // 시야 반대편에 있는 HP Bar가 보이는 것을 방지
        if(screenPosition.z < 0.0f)
        {
            screenPosition *= -1.0f;
        }

        Vector2 localPosition = Vector2.zero;

        // 스크린 좌표를 캔버스 좌표로 변환
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
