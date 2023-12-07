using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//ũ�� ����� ȣ���Ŵ
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
        Debug.Log("edit ũ�Ⱑ ����Ǿ����ϴ�.");
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
