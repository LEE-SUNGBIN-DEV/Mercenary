using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MovablePanel : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private RectTransform targetRectTransform;
    private Vector2 beginPosition;
    private Vector2 beginMovePosition;
    private Vector2 moveOffset;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        beginPosition = TargetRectTransform.position;
        beginMovePosition = eventData.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        moveOffset = eventData.position - beginMovePosition;
        TargetRectTransform.position = beginPosition + moveOffset;
    }

    #region Property
    public RectTransform TargetRectTransform
    {
        get { return targetRectTransform; }
        set { targetRectTransform = value; }
    }
    #endregion
}
