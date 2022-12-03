using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoginPanl : BasePanel
{
    protected override void Awake()
    {
        //一定不能少因为需要执行父类的awake来初始化一 些信息比如找控件加事件监听
        base.Awake();
        //在下面处理自己的一些初始化逻辑
    }



    void Start()
    {
        UIManager.AddCustomEventListener(GetControl<Button>("StartButton"), 
        EventTriggerType.PointerEnter, (data) =>
        {
            Debug.Log("进入");
        });

        UIManager.AddCustomEventListener(GetControl<Button>("StartButton"), 
        EventTriggerType.PointerExit, (data) =>
        {
            Debug.Log("离开");
        });
    }

    //外部不能调用  子类可以调用
    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName)
        {
            case "StartButton":
                ClickStart();
                break;
            case "QuitButton":
                ClickQuit();
                break;
            case "SettingButton":
                ClickSetting();
                break;
        }
    }

    /// <summary>
    /// 点击开始按钮的处理
    /// </summary>
    private void ClickStart()
    {
        Debug.Log("点击开始按钮的处理");
        //进度条
        UIManager.GetInstance().ShowPanel<LoginPanl>("LoginPanl", E_UI_Layer.Bot);
    }

    /// <summary>
    /// 点击退出按钮的处理
    /// </summary>
    private void ClickQuit()
    {
        Debug.Log("点击退出按钮的处理");
    }
    /// <summary>
    /// 点击设置按钮的处理
    /// </summary>
    private void ClickSetting()
    {
        Debug.Log("点击设置按钮的处理");
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void InitInfo()
    {
        Debug.Log("初始化数据");
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //显示面板时想要执行的逻辑这个函数在UI管理器中会自动帮我们调用
        //只要重写了它就会执行 里面的逻辑
        print("ShowPanel使用了这个方法");
    }

}
