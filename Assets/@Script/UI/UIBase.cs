using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class UIBase : MonoBehaviour
{
    // Enum - Object �� (ex: BUTTON - button1, button2 ... )
    protected Dictionary<System.Type, Object[]> uiObjectDictionary = new Dictionary<System.Type, Object[]>();

    // (1) Enum ���ο� ����� ���� ��, (2) ���׸����� �Է¹��� Ÿ�԰�
    // (3) ��� ��ġ�ϴ� ������Ʈ�� ã�Ƽ� uiObjectDictionary�� ���ε�
    public void BindObject<T>(System.Type type) where T : Object
    {
        string[] uiObjectNameArray = System.Enum.GetNames(type);
        Object[] uiObjectArray = new Object[uiObjectNameArray.Length];
        uiObjectDictionary.Add(typeof(T), uiObjectArray);

        for(int i=0; i < uiObjectArray.Length; i++)
        {
            uiObjectArray[i] = GameFunction.FindChild<T>(gameObject, uiObjectNameArray[i], true);
            if(uiObjectArray[i] == null)
            {
                Debug.Log($"Failed to bind({uiObjectNameArray[i]})");
            }
        }
    }
    public void BindText(System.Type type) { BindObject<TextMeshProUGUI>(type); }
    public void BindButton(System.Type type) { BindObject<Button>(type); }
    public void BindImge(System.Type type) { BindObject<Image>(type); }
    public void BindSlider(System.Type type) { BindObject<Slider>(type); }

    // Getting Function
    public T GetObject<T>(int index) where T : Object
    {
        if(uiObjectDictionary.TryGetValue(typeof(T), out Object[] uiObjectArray) == false)
        {
            return null;
        }
        else
        {
            return uiObjectArray[index] as T;
        }
    }
    public TextMeshProUGUI GetText(int index) { return GetObject<TextMeshProUGUI>(index); }
    public Button GetButton(int index) { return GetObject<Button>(index); }
    public Image GetImage(int index) { return GetObject<Image>(index); }
    public Slider GetSlider(int index) { return GetObject<Slider>(index); }

    // targetObject�� UIEventHandler ������Ʈ�� �����ϰ�, eventType�� �°� action�� ����Ѵ�.
    public void BindEvent(GameObject targetObject, UnityAction action, UI_EVENT eventType)
    {
        UIEventHandler newEventHandler = GameFunction.GetOrAddComponent<UIEventHandler>(targetObject);

        switch (eventType)
        {
            case UI_EVENT.CLICK:
                newEventHandler.OnClickHandler -= action;
                newEventHandler.OnClickHandler += action;
                break;

            case UI_EVENT.PRESS:
                newEventHandler.OnPressHandler -= action;
                newEventHandler.OnPressHandler += action;
                break;
        }
    }
}
