using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// 基础组件获取脚本基类,需要的组件请按照[Button]Button标记好
/// </summary>
public class BaseUIGetComponents_MonoBehaviour : MonoBehaviour
{
    //通过里式转换原则 来存储所有的控件
    private Dictionary<string, List<Component>> controlDic = new Dictionary<string, List<Component>>();

    protected virtual void Awake()
    {
        FindChildrenControl<Button>();
        FindChildrenControl<Image>();
        FindChildrenControl<Text>();
        FindChildrenControl<Toggle>();
        FindChildrenControl<Slider>();
        FindChildrenControl<ScrollRect>();
        FindChildrenControl<InputField>();
        FindChildrenControl<Dropdown>();
        FindChildrenControl<ToggleGroup>();
        FindChildrenControl<Transform>();
    }


    /// <summary>
    /// 找到子对象的对应控件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private void FindChildrenControl<T>() where T : Component
    {
        T[] controls = this.GetComponentsInChildren<T>();

        for (int i = 0; i < controls?.Length; i++)
        {
            string objName = controls[i].gameObject.name;//获取组件的名称

            if (controlDic.ContainsKey(objName))//字典里面有这个组件
                controlDic[objName].Add(controls[i]);
            else
                controlDic.Add(objName, new List<Component>() { controls[i] });
            //如果是按钮控件
            if (controls[i] is Button)
            {
                (controls[i] as Button).onClick.AddListener(() =>
                {
                    OnButtonClick(objName);
                });
            }
            //如果是单选框或者多选框
            else if (controls[i] is Toggle)
            {
                (controls[i] as Toggle).onValueChanged.AddListener((value) =>
                {
                    OnValueChanged(objName, value);
                });
            }
        }
    }

    protected virtual void OnValueChanged(string objName, bool value)
    {
        
    }

    protected virtual void OnButtonClick(string objName)
    {
        
    }

    /// <summary>
    /// 得到对应名字的对应控件脚本
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="controlName"></param>
    /// <returns></returns>
    protected T GetControl<T>(string controlName) where T : Component
    {
        if (controlDic.ContainsKey(controlName))
        {
            for (int i = 0; i < controlDic[controlName].Count; ++i)
            {
                if (controlDic[controlName][i] is T)
                    return controlDic[controlName][i] as T;
            }
        }
        return null;
    }

    #region 获取组件
    /// <summary>
    /// 获取按钮
    /// </summary>
    /// <param name="InputFieldName"></param>
    /// <returns></returns>
    public InputField GetInputField(string InputFieldName) => GetControl<InputField>(InputFieldName);

    /// <summary>
    /// 获取Text
    /// </summary>
    /// <param name="TextName"></param>
    /// <returns></returns>
    public Text GetText(string TextName) => GetControl<Text>(TextName);

    /// <summary>
    /// 获取Text
    /// </summary>
    /// <param name="ImageName"></param>
    /// <returns></returns>
    public Image GetImage(string ImageName) => GetControl<Image>(ImageName);

    /// <summary>
    /// 获取Toggle
    /// </summary>
    /// <param name="ToggleName"></param>
    /// <returns></returns>
    public Toggle GetToggle(string ToggleName) => GetControl<Toggle>(ToggleName);

    /// <summary>
    /// 获取Toggle
    /// </summary>
    /// <param name="ButtonName"></param>
    /// <returns></returns>
    public Button GetButton(string ButtonName) => GetControl<Button>(ButtonName);

    /// <summary>
    /// 获取Transform
    /// </summary>
    /// <param name="TransformName"></param>
    /// <returns></returns>
    public Transform GetTransform(string TransformName) => GetControl<Transform>(TransformName);

    /// <summary>
    /// 获取GameObject
    /// </summary>
    /// <param name="TransformName"></param>
    /// <returns></returns>
    public GameObject GetGameObject(string TransformName) => GetControl<Transform>(TransformName).gameObject;

    /// <summary>
    /// 获取Dropdown
    /// </summary>
    /// <param name="DropdownName"></param>
    /// <returns></returns>
    public Dropdown GetDropdown(string DropdownName) => GetControl<Dropdown>(DropdownName);

    /// <summary>
    /// 获取ScrollRect
    /// </summary>
    /// <param name="ScrollRectName"></param>
    /// <returns></returns>
    public ScrollRect GetScrollRect(string ScrollRectName) => GetControl<ScrollRect>(ScrollRectName);

    /// <summary>
    /// 获取ToggleGroup
    /// </summary>
    /// <param name="ToggleGroupName"></param>
    /// <returns></returns>
    public ToggleGroup GetToggleGroup(string ToggleGroupName) => GetControl<ToggleGroup>(ToggleGroupName);
    #endregion
}
