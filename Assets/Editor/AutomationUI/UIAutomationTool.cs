using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIAutomationTool : EditorWindow
{
    private string prefix = "V_";

    public string InputComponentName { get; private set; }

    [MenuItem("GameObject/组件查找和重命名(Shift+A) #A", false, 0)]
    [MenuItem("Assets/组件查找和重命名(Shift+A) #A")]
    [MenuItem("Tool/组件查找和重命名(Shift+A) #A", false, 0)]
    public static void GeneratorFindComponentTool() => EditorWindow.GetWindow(typeof(UIAutomationTool), false, "组件查找和重命名(Shift+A)").Show();

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical(GUILayout.Width(position.width), GUILayout.Height(position.height));
        {
            //******************************Debug自动生成的代码的前缀******************************
            EditorGUILayout.LabelField("Debug自动生成的代码的前缀", EditorStyles.label);
            GUILayout.BeginVertical("box");
            {
                GUILayout.Label("请输入前缀:", GUILayout.Width(70f));
                InputComponentName = GUILayout.TextField(InputComponentName, "BoldTextField", GUILayout.Width(200f));
            }
            GUILayout.EndVertical(); GUILayout.Space(5f);
            //******************************生成Config脚本******************************
            EditorGUILayout.LabelField("生成组件Config", EditorStyles.label);
            GUILayout.BeginVertical("box");
            {
                //EditorGUILayout.LabelField("生成Config脚本", EditorStyles.label);
                //if (GUILayout.Button("生成Config脚本", GUILayout.Width(150))) { CreatConfig(); }
                EditorGUILayout.LabelField("Debug生成Config代码", EditorStyles.label);
                if (GUILayout.Button("打印生成Config代码", GUILayout.Width(150))) { PrintConfig(); }
            }
            GUILayout.EndVertical(); GUILayout.Space(5f);
            //******************************组件查找代码******************************
            EditorGUILayout.LabelField("组件查找代码", EditorStyles.label);
            GUILayout.BeginVertical("box");
            {
                EditorGUILayout.LabelField("组件查找代码", EditorStyles.label);
                if (GUILayout.Button("组件查找代码", GUILayout.Width(200))) { ComponentFind(); }
            }
            GUILayout.EndVertical(); GUILayout.Space(5f);
            //******************************按钮监听代码******************************
            EditorGUILayout.LabelField("按钮监听代码", EditorStyles.label);
            GUILayout.BeginVertical("box");
            {
                EditorGUILayout.LabelField("按钮监听代码", EditorStyles.label);
                if (GUILayout.Button("按钮监听代码", GUILayout.Width(200))) { AddListener(); }
            }
            GUILayout.EndVertical(); GUILayout.Space(5f);
            //******************************组件重命名******************************
            EditorGUILayout.LabelField("组件重命名", EditorStyles.label);
            GUILayout.BeginHorizontal("box");
            {
                EditorGUILayout.LabelField($"前缀添加{prefix}", EditorStyles.label);
                if (GUILayout.Button($"前缀添加{prefix}", GUILayout.Width(200))) { AddPrefix(); }
                if (GUILayout.Button($"去除前缀{prefix}", GUILayout.Width(200))) { RemovePrefix(); }
            }
            GUILayout.EndHorizontal(); GUILayout.Space(5f);
            //******************************一键去除组件RayCast Target******************************
            EditorGUILayout.LabelField("一键去除组件RayCast Target", EditorStyles.label);
            GUILayout.BeginHorizontal("box");
            {
                EditorGUILayout.LabelField("一键去除组件RayCast Target", EditorStyles.label);
                if (GUILayout.Button("一键去除组件RayCast Target", GUILayout.Width(200))) { ClearRayCastTarget(); }
            }
            GUILayout.EndHorizontal(); GUILayout.Space(5f);
        }
        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// 即使刷新页面函数 OnSelectionChange
    /// </summary>
    private void OnSelectionChange() => Repaint();

    //******************************方法******************************

    /// <summary>
    /// 生成Config脚本
    /// </summary>
    private void CreatConfig()
    {
        //获取到当前选择的物体
        GameObject obj = Selection.objects.First() as GameObject;
        Dictionary<string, List<Component>> ComponentsDic = GeneratorFindComponentConfig.FindComponents(obj);
        GeneratorFindComponentConfig.CreatCSharpScript(obj, ComponentsDic);
    }

    /// <summary>
    /// 打印生成Config代码
    /// </summary>
    private void PrintConfig()
    {
        //获取到当前选择的物体
        GameObject obj = Selection.objects.First() as GameObject;
        Dictionary<string, List<Component>> ComponentsDic = GeneratorFindComponentConfig.FindComponents(obj);
        GeneratorFindComponentConfig.DebugOutDemo(ComponentsDic, true);
    }

    /// <summary>
    /// 打印组件查找代码
    /// </summary>
    private void ComponentFind()
    {
        //获取到当前选择的物体
        GameObject obj = Selection.objects.First() as GameObject;
        Dictionary<string, List<Component>> ComponentsDic = GeneratorFindComponentConfig.FindComponents(obj);
        GeneratorFindComponentConfig.DebugOutGetComponentDemo(InputComponentName, ComponentsDic);//getComponent.
    }

    /// <summary>
    /// 监听代码
    /// </summary>
    private void AddListener()
    {
        //获取到当前选择的物体
        GameObject obj = Selection.objects.First() as GameObject;
        Dictionary<string, List<Component>> ComponentsDic = GeneratorFindComponentConfig.FindComponents(obj);
        GeneratorFindComponentConfig.DebugOutAddListenerDemo(InputComponentName, ComponentsDic);
    }

    /// <summary>
    /// 添加前缀
    /// </summary>
    private void AddPrefix()
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
    private void RemovePrefix()
    {
        Object[] obj = Selection.objects;//获取到当前选择的物体 
        foreach (var item in obj)
        {
            GameObject go = item as GameObject;
            if (go.name.Contains(prefix))
            {
                go.name = go.name.Replace(prefix, "");
            }
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
