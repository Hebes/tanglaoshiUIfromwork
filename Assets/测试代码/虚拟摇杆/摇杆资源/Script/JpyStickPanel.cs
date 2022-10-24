using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 摇杆类型
/// </summary>
public enum E_JoystickType
{
    /// <summary>
    /// 固定摇杆
    /// </summary>
    Normal,
    /// <summary>
    /// 可变位置摇杆
    /// </summary>
    CanChangePos,
    /// <summary>
    /// 可移动摇杆
    /// </summary>
    CanMove,
}


public class JpyStickPanel : BasePanel
{
    public E_JoystickType e_JoystickType = E_JoystickType.Normal;
    public float maxL = 150;

    /// <summary>
    /// 鼠标按下抬起拖曳3个事件的监听这它主要用于控制范围
    /// </summary>
    private Image imageTouchRect => GetControl<Image>("ImgTouchRect");
    /// <summary>
    /// 摇杆背景图片
    /// </summary>
    private Image imgBk => GetControl<Image>("ImageBK");
    /// <summary>
    /// 摇杆圆圈
    /// </summary>
    private Image imgControl => GetControl<Image>("ImgControl");


    // Start is called before the first frame update
    void Start()
    {
        // 过管理器操供的添加自定义事件监听的方法把对应的函数和事件关联起来一一进行处得

        UIManager.AddCustomEventListener(imageTouchRect, EventTriggerType.PointerDown, PointerDown);
        UIManager.AddCustomEventListener(imageTouchRect, EventTriggerType.PointerUp, PointerUp);
        UIManager.AddCustomEventListener(imageTouchRect, EventTriggerType.Drag, Drag);

        switch (e_JoystickType)
        {
            default:
            case E_JoystickType.Normal: imgBk.gameObject.SetActive(true); break;
            case E_JoystickType.CanChangePos:
            case E_JoystickType.CanMove: imgBk.gameObject.SetActive(false); break;//可变位置摇杆 - 开始隐藏
        }
    }

    private void PointerDown(BaseEventData data)
    {
        Debug.Log("Down");

        //可变位置摇杆 - 按下显示
        imgBk.gameObject.SetActive(true);

        switch (e_JoystickType)
        {
            default:
            case E_JoystickType.Normal: break;
            case E_JoystickType.CanChangePos:
            case E_JoystickType.CanMove:
                //可变位置摇杆 - 点击屏幕位置显示 
                Vector2 localPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                   imageTouchRect.rectTransform,//你想要改变位置的对象的父对象
                   (data as PointerEventData).position,//得到当前屏幕鼠标位置
                   (data as PointerEventData).pressEventCamera,// UI用的摄像机
                    out localPos);//可以得到一个转换来的相对坐标

                imgBk.transform.localPosition = localPos;
                break;//可变位置摇杆 - 开始隐藏
        }
    }
    private void PointerUp(BaseEventData data)
    {
        Debug.Log("Up");
        imgControl.transform.localPosition = Vector2.zero;
        //分发我的摇杆方向
        EventCenter.GetInstance().EventTrigger<Vector2>("Joystick", Vector2.zero);


        switch (e_JoystickType)
        {
            default:
            case E_JoystickType.Normal: imgBk.gameObject.SetActive(true); break;
            case E_JoystickType.CanChangePos:
            case E_JoystickType.CanMove: imgBk.gameObject.SetActive(false); break;//可变位置摇杆 - 开始隐藏
        }
    }
    private void Drag(BaseEventData data)
    {
        Debug.Log("Drag");
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imgBk.rectTransform,//你想要改变位置的对象的父对象
            (data as PointerEventData).position,//得到当前屏幕鼠标位置
            (data as PointerEventData).pressEventCamera,// UI用的摄像机
             out localPos);//可以得到一个转换来的相对坐标

        //更新位置
        imgControl.transform.localPosition = localPos;


        //范围判断
        if (localPos.magnitude > maxL)//159代表ImageBK的Wight一半
        {
            switch (e_JoystickType)
            {
                default:
                case E_JoystickType.Normal:  
                case E_JoystickType.CanChangePos: break;
                case E_JoystickType.CanMove:
                    imgBk.transform.localPosition += (Vector3)(localPos.normalized * (localPos.magnitude - maxL)); break;//超出多少就让背景图动多少
            }
            //超出范围 等于这个范围
            imgControl.transform.localPosition = localPos.normalized * maxL;
        }

        //分发我的摇杆方向
        EventCenter.GetInstance().EventTrigger<Vector2>("Joystick", localPos.normalized);
    }
}


