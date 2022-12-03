using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 这里写各种组件获取
/// </summary>
public class Panel1GetComponent : BaseUIGetComponents_new
{
    private Panel1 panel1;

    public Button V_ButtonButton { set; get; }

    public Image V_ButtonImage { set; get; }
    public Image V_ImageImage { set; get; }

    public Transform V_ButtonTransform { set; get; }
    public Transform V_ImageTransform { set; get; }
    public Transform V_TextTransform { set; get; }
    public Transform V_ToggleTransform { set; get; }

    public Text V_TextText { set; get; }

    public Toggle V_ToggleToggle { set; get; }


    public Panel1GetComponent(MonoBehaviour monoBehaviour, GameObject root) : base(root) => panel1 = monoBehaviour as Panel1;
    public override void Init()
    {
        base.Init();
        UnityEngine.Debug.Log("初始化成功");
        OnGetComponent();
        OnAddListener();
    }

    /// <summary>
    /// 获取组件
    /// </summary>
    private void OnGetComponent()
    {
        V_ButtonButton = GetButton("V_Button");

        V_ButtonImage = GetImage("V_Button");
        V_ImageImage = GetImage("V_Image");

        V_ButtonTransform = GetTransform("V_Button");
        V_ImageTransform = GetTransform("V_Image");
        V_TextTransform = GetTransform("V_Text");
        V_ToggleTransform = GetTransform("V_Toggle");

        V_TextText = GetText("V_Text");

        V_ToggleToggle = GetToggle("V_Toggle");
    }

    /// <summary>
    /// 按钮监听
    /// </summary>
    private void OnAddListener()
    {
        V_ButtonButton.onClick.AddListener(panel1.V_ButtonAddListener);
        V_ToggleToggle.onValueChanged.AddListener(panel1.V_ToggleAddListener);
    }
}
