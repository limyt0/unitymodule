using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//크기 변경시 호출시킴
public class OnRect : MonoBehaviour
{
    public UnityAction e;
    public UnityAction enable;
    public UnityAction disable;

    private void OnRectTransformDimensionsChange()
    {
        if (e != null)
        {
            e.Invoke();
        }
        else
        {
            Debug.Log("e null!");
        }
        Debug.Log("edit 크기가 변경되었습니다.");
    }

    private void OnEnable()
    {
        if (enable != null) {
            enable.Invoke();
        }   
    }

    private void OnDisable()
    {
        if (disable != null)
        {
            disable.Invoke();
        }
    }
}
