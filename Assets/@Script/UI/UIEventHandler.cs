using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIEventHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
	public event UnityAction OnClickHandler = null;
	public event UnityAction OnPressHandler = null;

	public void OnPointerClick(PointerEventData eventData)
	{
		OnClickHandler?.Invoke();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		OnPressHandler?.Invoke();
	}

}
