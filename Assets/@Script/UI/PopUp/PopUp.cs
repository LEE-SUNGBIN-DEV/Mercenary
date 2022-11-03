using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Popup : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private POPUP_TYPE popupType;

    public static event UnityAction<Popup> onFocus;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        onFocus(this);
    }

    public POPUP_TYPE PopupType
    {
        get { return popupType; }
        private set { popupType = value; }
    }
}
