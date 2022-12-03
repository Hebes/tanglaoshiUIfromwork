using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// C# 中普通类、抽象类、接口之间的区别 https://blog.csdn.net/T_Twory/article/details/51543247
/// </summary>
public abstract class BaseUIGetComponents_new
{
    //通过里式转换原则 来存储所有的控件
    private Dictionary<string, List<Component>> controlDic = null;
    protected GameObject Obj;

    protected BaseUIGetComponents_new(GameObject Obj)
    {
        this.Obj = Obj;
        controlDic = new Dictionary<string, List<Component>>();
        FindChildrenControl<Button>(Obj);
        FindChildrenControl<Image>(Obj);
        FindChildrenControl<Text>(Obj);
        FindChildrenControl<Toggle>(Obj);
        FindChildrenControl<Slider>(Obj);
        FindChildrenControl<ScrollRect>(Obj);
        FindChildrenControl<InputField>(Obj);
        FindChildrenControl<Dropdown>(Obj);
        FindChildrenControl<ToggleGroup>(Obj);
        FindChildrenControl<Transform>(Obj);
    }

    public virtual void Init() { }

    /// <summary>
    /// 找到子对象的对应控件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private void FindChildrenControl<T>(GameObject root) where T : Component
    {
        T[] controls = root.GetComponentsInChildren<T>();

        for (int i = 0; i < controls?.Length; i++)
        {
            string objName = controls[i].gameObject.name;//获取组件的名称

            if (!objName.StartsWith("V_"))
                continue;

            if (controlDic.ContainsKey(objName))//字典里面有这个组件
                controlDic[objName].Add(controls[i]);
            else
                controlDic.Add(objName, new List<Component>() { controls[i] });
        }
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
