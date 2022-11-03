using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class BaseUI : MonoBehaviour
{
    // Enum - Object 쌍 (ex: BUTTON - button1, button2 ... )
    protected Dictionary<System.Type, Object[]> uiObjectDictionary = new Dictionary<System.Type, Object[]>();

    // (1) Enum 내부에 선언된 변수 명과 (2) 제네릭으로 입력받은 타입과 (3) 모두 일치하는 컴포넌트를 찾아서 uiObjectDictionary에 바인딩
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
        Object[] uiObjectArray = null;

        if(uiObjectDictionary.TryGetValue(typeof(T), out uiObjectArray) == false)
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

    //
    public void BindEvent(GameObject go, UnityAction action)
	{
	}
}
