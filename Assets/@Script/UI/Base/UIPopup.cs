using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIPopup : UIBase, IPointerDownHandler
{
    public event UnityAction<UIPopup> OnFocus;
    
    protected bool isInitialized = false;

    public virtual void Initialize(UnityAction<UIPopup> action = null)
    {
        if(action != null)
        {
            OnFocus -= action;
            OnFocus += action;
        }

        isInitialized = true;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        OnFocus(this);
    }

    public bool IsInitialized { get { return isInitialized; } }
}
