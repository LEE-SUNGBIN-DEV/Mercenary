using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIPopup : UIBase, IPointerDownHandler
{
    public event UnityAction<UIPopup> OnFocus;

    [SerializeField] private POPUP popupType;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        OnFocus(this);
    }

    public POPUP PopupType
    {
        get { return popupType; }
        private set { popupType = value; }
    }
}
