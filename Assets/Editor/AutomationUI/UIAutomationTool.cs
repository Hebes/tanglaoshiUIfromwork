using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIAutomationTool : EditorWindow
{
    private const string UIprefix = "V_";
    private const string TransformPrefix = "T_";

    private static bool isAddPrefix { get; set; }
    private Vector2 scrollPosition { get; set; } = Vector2.zero;
    public string InputComponentName { get; private set; }
    public string InputTransformComponentName { get; private set; }

    [MenuItem("GameObject/组件查找和重命名(Shift+A) #A", false, 0)]
    [MenuItem("Assets/组件查找和重命名(Shift+A) #A")]
    [MenuItem("Tool/组件查找和重命名(Shift+A) #A", false, 0)]
    public static void GeneratorFindComponentTool() => EditorWindow.GetWindow(typeof(UIAutomationTool), false, "组件查找和重命名(Shift+A)").Show();

    private void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);//GUILayout.Width(400), GUILayout.Height(500)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Width(position.width), GUILayout.Height(position.height));
            {
                GUILayout.Space(5f);

                //******************************UI组件查找打印******************************
                GUILayout.BeginVertical("box", GUILayout.Width(200f));//垂直
                {
                    EditorGUILayout.LabelField("Debug自动生成的代码的前缀", EditorStyles.label);
                    //******************************请输入前缀******************************
                    GUILayout.Space(5f);
                    GUILayout.Label("请输入前缀:", GUILayout.Width(70f));
                    InputComponentName = GUILayout.TextField(InputComponentName, "BoldTextField", GUILayout.Width(200f));
                    //******************************生成Config脚本******************************
                    GUILayout.Space(5f);
                    //EditorGUILayout.LabelField("生成Config脚本", EditorStyles.label);
                    //if (GUILayout.Button("生成Config脚本", GUILayout.Width(150))) { CreatConfig(); }
                    EditorGUILayout.LabelField("Debug生成Config代码", GUILayout.Width(200f));//EditorStyles.label
                    if (GUILayout.Button("打印生成Config代码", GUILayout.Width(200f))) { PrintConfig(); }
                    //******************************组件查找代码******************************
                    GUILayout.Space(5f);
                    EditorGUILayout.LabelField("组件查找代码", GUILayout.Width(100f));
                    isAddPrefix = GUILayout.Toggle(isAddPrefix, "是否添加前缀");
                    if (GUILayout.Button("组件查找代码", GUILayout.Width(200))) { ComponentFind(isAddPrefix); }
                    //******************************按钮监听代码******************************
                    GUILayout.Space(5f);
                    EditorGUILayout.LabelField("按钮监听代码", GUILayout.Width(100f));
                    if (GUILayout.Button("按钮监听代码", GUILayout.Width(200))) { AddListener(UIprefix); }
                    //******************************组件重命名******************************
                    GUILayout.Space(5f);
                    EditorGUILayout.LabelField($"前缀添加{UIprefix}:", GUILayout.Width(70f));//EditorStyles.label
                    if (GUILayout.Button($"前缀添加{UIprefix}", GUILayout.Width(200))) { AddPrefix(UIprefix); }
                    if (GUILayout.Button($"去除前缀{UIprefix}", GUILayout.Width(200))) { RemovePrefix(UIprefix); }
                    //******************************保存修改******************************
                    GUILayout.Space(5f);
                    EditorGUILayout.LabelField($"保存修改", GUILayout.Width(70f));//EditorStyles.label
                    if (GUILayout.Button($"保存修改", GUILayout.Width(200))) { SaveModification(); }
                }
                GUILayout.EndVertical(); GUILayout.Space(5f);

                //******************************Transform组件查找打印******************************配合Transform拓展
                GUILayout.BeginVertical("box", GUILayout.Width(200f));
                {
                    EditorGUILayout.LabelField("Transform组件查找打印", EditorStyles.label);
                    //******************************请输入Transform组件查找前缀******************************
                    GUILayout.Space(5f);
                    GUILayout.Label("请输入Transform组件查找前缀:", GUILayout.Width(200f));
                    InputTransformComponentName = GUILayout.TextField(InputTransformComponentName, "BoldTextField", GUILayout.Width(200f));
                    //******************************Transform组件查找打印******************************
                    GUILayout.Space(5f);
                    EditorGUILayout.LabelField("Transform组件查找打印:", GUILayout.Width(170f));//EditorStyles.label
                    if (GUILayout.Button("Transform组件查找打印", GUILayout.Width(200))) { TransformComponentGet(); }
                    //******************************按钮监听代码******************************
                    GUILayout.Space(5f);
                    EditorGUILayout.LabelField("按钮监听代码", GUILayout.Width(100f));
                    if (GUILayout.Button("按钮监听代码", GUILayout.Width(200))) { AddListener(TransformPrefix); }
                    //******************************Transform组件获取打印******************************
                    GUILayout.Space(5f);
                    EditorGUILayout.LabelField("Transform组件获取打印:", GUILayout.Width(170f));//EditorStyles.label
                    if (GUILayout.Button("Transform组件获取打印", GUILayout.Width(200))) { TransformComponentFind(); }
                    //******************************组件重命名******************************
                    GUILayout.Space(5f);
                    EditorGUILayout.LabelField($"前缀添加{TransformPrefix}:", GUILayout.Width(70f));//EditorStyles.label
                    if (GUILayout.Button($"前缀添加{TransformPrefix}", GUILayout.Width(200))) { AddPrefix(TransformPrefix); }
                    if (GUILayout.Button($"去除前缀{TransformPrefix}", GUILayout.Width(200))) { RemovePrefix(TransformPrefix); }
                    //******************************保存修改******************************
                    GUILayout.Space(5f);
                    EditorGUILayout.LabelField($"保存修改", GUILayout.Width(70f));//EditorStyles.label
                    if (GUILayout.Button($"保存修改", GUILayout.Width(200))) { SaveModification(); }
                }
                GUILayout.EndVertical(); GUILayout.Space(5f);
            }
            EditorGUILayout.EndHorizontal(); GUILayout.Space(5f);
        }
        GUILayout.EndScrollView();

        //******************************一键去除组件RayCast Target******************************
        GUILayout.BeginVertical("box");
        {
            EditorGUILayout.LabelField("一键去除组件RayCast Target", GUILayout.Width(200f));
            EditorGUILayout.LabelField("一键去除组件RayCast Target:", GUILayout.Width(170f));//EditorStyles.label
            if (GUILayout.Button("一键去除组件RayCast Target", GUILayout.Width(200))) { ClearRayCastTarget(); }
        }
        GUILayout.EndVertical(); GUILayout.Space(5f);
    }

    /// <summary>
    /// 即使刷新页面函数 OnSelectionChange
    /// </summary>
    private void OnSelectionChange() => Repaint();

    //******************************方法******************************
    //*****************************************************UI*****************************************************
    /// <summary>
    /// 生成Config脚本
    /// </summary>
    private void CreatConfig()
    {
        //获取到当前选择的物体
        GameObject obj = Selection.objects.First() as GameObject;
        Dictionary<string, List<Component>> ComponentsDic = UIFindComponent.FindComponents(obj, UIprefix);
        UIFindComponent.CreatCSharpScript(obj, ComponentsDic);
    }

    /// <summary>
    /// 打印生成Config代码
    /// </summary>
    private void PrintConfig()
    {
        //获取到当前选择的物体
        GameObject obj = Selection.objects.First() as GameObject;
        Dictionary<string, List<Component>> ComponentsDic = UIFindComponent.FindComponents(obj, UIprefix);
        UIFindComponent.DebugOutDemo(ComponentsDic, true);
    }

    /// <summary>
    /// 打印组件查找代码
    /// </summary>
    private void ComponentFind(bool isAddPrefix)
    {
        //获取到当前选择的物体
        GameObject obj = Selection.objects.First() as GameObject;
        Dictionary<string, List<Component>> ComponentsDic = UIFindComponent.FindComponents(obj, UIprefix);
        UIFindComponent.DebugOutGetComponentDemo(InputComponentName, ComponentsDic, isAddPrefix);//getComponent.
    }

    /// <summary>
    /// 监听代码
    /// </summary>
    private void AddListener(string keyValue)
    {
        //获取到当前选择的物体
        GameObject obj = Selection.objects.First() as GameObject;
        Dictionary<string, List<Component>> ComponentsDic = UIFindComponent.FindComponents(obj, keyValue);
        UIFindComponent.DebugOutAddListenerDemo(InputComponentName, ComponentsDic);
    }

    /// <summary>
    /// 添加前缀
    /// </summary>
    private void AddPrefix(string prefix)
    {
        Object[] obj = Selection.objects;//获取到当前选择的物体
        foreach (var item in obj)
        {
            GameObject go = item as GameObject;

            if (go.name.StartsWith(prefix))
                continue;

            go.name = $"{prefix}{go.name}";
        }
    }

    /// <summary>
    /// 删除前缀
    /// </summary>
    private void RemovePrefix(string prefix)
    {
        Object[] obj = Selection.objects;//获取到当前选择的物体 
        foreach (var item in obj)
        {
            GameObject go = item as GameObject;
            if (go.name.Contains(prefix))
                go.name = go.name.Replace(prefix, "");
        }
    }

    //*****************************************************Transform*****************************************************
    /// <summary>
    /// 通过Transform组件获得
    /// </summary>
    private void TransformComponentGet()
    {
        //获取到当前选择的物体
        GameObject obj = Selection.objects.First() as GameObject;
        Dictionary<string, List<Component>> ComponentsDic = UIFindComponent.FindComponents(obj, TransformPrefix);
        UIFindComponent.DebugOutDemo(ComponentsDic, true);//getComponent.
    }

    /// <summary>
    /// 通过Transform拓展组件查找
    /// </summary>
    private void TransformComponentFind()
    {
        //获取到当前选择的物体
        GameObject obj = Selection.objects.First() as GameObject;
        Dictionary<string, List<Component>> ComponentsDic = UIFindComponent.FindComponents(obj, TransformPrefix);
        UIFindComponent.DebugOutGetTransformComponentDemo(ComponentsDic, InputTransformComponentName);//getComponent.
    }

    //*****************************************************其他*****************************************************
    /// <summary>
    /// 保存修改
    /// </summary>
    private void SaveModification()
    {
        Object[] obj = Selection.objects;
        for (int i = 0; i < obj.Length; i++)
        {
            Undo.RecordObject(obj[i], "modify test value");
            EditorUtility.SetDirty(obj[i]);
        }
    }
    /// <summary>
    /// 去除组件RayCast Target
    /// </summary>
    private void ClearRayCastTarget()
    {
        Object[] obj = Selection.objects;//获取到当前选择的物体
        foreach (var item in obj)
        {
            GameObject go = item as GameObject;
            if (go.GetComponent<Text>() != null)
            {
                go.GetComponent<Text>().raycastTarget = false;
                //if (EditorUtility.DisplayDialog("消息提示", "已去除:" + go.name + "的RayCast Target选项", "确定")) { }
                continue;
            }
            else if (go.GetComponent<Image>())
            {
                go.GetComponent<Image>().raycastTarget = false;
                //if (EditorUtility.DisplayDialog("消息提示", "已去除:" + go.name + "的RayCast Target选项", "确定")) { }
                continue;
            }
            else if (go.GetComponent<RawImage>())
            {
                go.GetComponent<RawImage>().raycastTarget = false;
                //if (EditorUtility.DisplayDialog("消息提示", "已去除:" + go.name + "的RayCast Target选项", "确定")) { }
                continue;
            }
            if (EditorUtility.DisplayDialog("消息提示", go.name + "没有找到需要去除的RayCast Target选项", "确定")) { }
        }
    }

}
