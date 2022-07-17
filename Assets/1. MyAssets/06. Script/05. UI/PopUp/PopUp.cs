using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PopUp : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private POPUP_TYPE popUpType;

    public static event UnityAction<PopUp> onFocus;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        onFocus(this);
    }

    public POPUP_TYPE PopUpType
    {
        get { return popUpType; }
        private set { popUpType = value; }
    }
}
