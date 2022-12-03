using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 这里写逻辑代码
/// </summary>
public class Panel1 : MonoBehaviour
{
    Panel1GetComponent panel1GetComponent;
    private void Awake()
    {
        panel1GetComponent = new Panel1GetComponent(this,this.gameObject);
        panel1GetComponent.Init();
    }

    internal void V_ButtonAddListener() => UnityEngine.Debug.Log("Button监听");

    internal void V_ToggleAddListener(bool arg0) => UnityEngine.Debug.Log("Toggle监听");
}
