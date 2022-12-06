using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// UI组件查找
/// </summary>
public class UIFindComponent : Editor
{

    public static string CSharpSavePath = Application.dataPath + "/ATempUIViewConfig";        //view组件查找脚本的生成路径

    private static Dictionary<string, List<Component>> controlDic = null;

    //******************************通用代码******************************
    public static Dictionary<string, List<Component>> FindComponents(GameObject obj, string keyValue)
    {
        Dictionary<string, List<Component>> controlDic = new Dictionary<string, List<Component>>();
        //查找组件
        FindChildrenControl<Button>(obj, controlDic, keyValue);
        FindChildrenControl<Image>(obj, controlDic, keyValue);
        FindChildrenControl<Text>(obj, controlDic, keyValue);
        FindChildrenControl<Toggle>(obj, controlDic, keyValue);
        FindChildrenControl<Slider>(obj, controlDic, keyValue);
        FindChildrenControl<ScrollRect>(obj, controlDic, keyValue);
        FindChildrenControl<InputField>(obj, controlDic, keyValue);
        FindChildrenControl<Transform>(obj, controlDic, keyValue);
        FindChildrenControl<ToggleGroup>(obj, controlDic, keyValue);
        FindChildrenControl<Dropdown>(obj, controlDic, keyValue);
        return controlDic;
    }

    /// <summary>
    /// 找到子对象的对应控件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private static void FindChildrenControl<T>(GameObject gameObject, Dictionary<string, List<Component>> controlDic, string keyValue) where T : Component
    {
        T[] controls = gameObject.GetComponentsInChildren<T>();

        for (int i = 0; i < controls?.Length; i++)
        {
            string objName = controls[i].gameObject.name;//获取组件的名称

            if (!objName.StartsWith(keyValue))//keyValue = V_
                continue;

            if (controlDic.ContainsKey(objName))//字典里面有这个组件
                controlDic[objName].Add(controls[i]);
            else
                controlDic.Add(objName, new List<Component>() { controls[i] });
        }
    }

    //******************************杂项代码******************************
    /// <summary>
    /// 文件以追加写入的方式
    /// https://wenku.baidu.com/view/a8fdb767fd4733687e21af45b307e87100f6f85b.html
    /// 显示IO异常请在创建文件的时候Close下
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="content">内容</param>
    private static void FileWriteContent(string path, string content)
    {
        byte[] myByte = System.Text.Encoding.UTF8.GetBytes(content);
        using (FileStream fsWrite = new FileStream(path, FileMode.Append, FileAccess.Write))
        {
            fsWrite.Write(myByte, 0, myByte.Length);
        }
    }

    /// <summary>
    /// 通过路径检文件夹是否存在，如果不存在则创建
    /// </summary>
    /// <param name="folderPath"></param>
    private static void ChackFolder(string folderPath)
    {
        if (!Directory.Exists(folderPath))//是否存在这个文件
        {
            Debug.Log("文件夹不存在,正在创建...");
            Directory.CreateDirectory(folderPath);//创建
            AssetDatabase.Refresh();//刷新编辑器
            Debug.Log("创建成功!");
        }
    }

    /// <summary>
    /// 字典重新排列
    /// </summary>
    private static Dictionary<string, List<string>> ReArrangeDic(Dictionary<string, List<Component>> controlDic)
    {
        //重新排列
        Dictionary<string, List<string>> controlDicTemp = new Dictionary<string, List<string>>();
        foreach (var item in controlDic)
        {
            foreach (var child in item.Value)
            {
                //获取组件的类型
                string[] childType = child.GetType().Name.Split('.');
                string childTypeString = childType[childType.Length - 1];
                if (controlDicTemp.ContainsKey(childTypeString))
                    controlDicTemp[childTypeString].Add(child.name);
                else
                    controlDicTemp.Add(childTypeString, new List<string>() { child.name, });
            }
        }
        return controlDicTemp;
    }

    /// <summary>
    /// 添加前缀
    /// </summary>
    /// <returns></returns>
    private static string AddPrefix(string beginStr)
    {
        //添加前缀
        if (!string.IsNullOrEmpty(beginStr))
            return $"{beginStr}.";
        return beginStr;
    }

    //******************************生成Config脚本******************************
    /// <summary>
    /// 生成c#脚本
    /// </summary>
    public static void CreatCSharpScript(GameObject obj, Dictionary<string, List<Component>> controlDic)
    {
        //通过路径检文件夹是否存在，如果不存在则创建
        ChackFolder(CSharpSavePath);
        //配置文件的代码
        string sb = CreatConfigFile($"{obj.name}Config", controlDic);
        //创建并写入内容
        string filePath = $"{CSharpSavePath}/{obj.name}Config.cs";
        if (!File.Exists(filePath))
        {
            Debug.Log("文件不存在,进行创建...");
            using (StreamWriter writer = File.CreateText(filePath))//生成文件
            {
                writer.Write(sb);
                Debug.Log("内容写入成功!");
            }
        }
        else
        {
            FileWriteContent(filePath, sb);
        }
        //刷新unity编辑器
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 生成配置文件 最开始版本
    /// </summary>
    private static string CreatConfigFile(string FileName, Dictionary<string, List<Component>> controlDic)
    {
        StringBuilder sb = new StringBuilder();
        //添加引用
        sb.AppendLine("/*---------------------------------");
        sb.AppendLine(" *Title:UI自动化组件配置生成工具");
        sb.AppendLine(" *Author:暗沉");
        sb.AppendLine(" *Date:" + System.DateTime.Now);
        sb.AppendLine(" *注意:以下文件是自动生成的");
        sb.AppendLine("---------------------------------*/");
        sb.AppendLine("using UnityEngine;");
        sb.AppendLine();

        sb.AppendLine($"public class {FileName}");
        sb.AppendLine("{");

        foreach (var str in controlDic.Keys)
        {
            sb.AppendLine($"\tpublic string {str} = \"{str}\";");
        }
        sb.AppendLine("}");
        return sb.ToString();
    }

    //******************************打印里面输出Config******************************
    /// <summary>
    /// 打印里面输出Config
    /// </summary>
    /// <param name="obj"></param>
    public static string DebugOutDemo(FindConfig findtConfig)
    {
        //字典重新排列
        Dictionary<string, List<string>> controlDicTemp = ReArrangeDic(findtConfig.controlDic);
        //打印
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("#region 组件模块");
        foreach (var item in controlDicTemp)
        {
            string itemKey = item.Key;
            //过滤模块
            switch (item.Key)
            {
                case "RectTransform":
                    itemKey = "Transform";
                    break;
            }
            foreach (var child in item.Value)
            {
                if (findtConfig.isGetSet)
                    sb.AppendLine($"public {itemKey} {child}{itemKey} {{ set; get; }}");
                else
                    sb.AppendLine($"public {itemKey} {child}{itemKey};");
            }
            sb.AppendLine();
        }
        sb.AppendLine("#endregion");
        Debug.Log(sb.ToString());
        return sb.ToString();
    }

    //******************************打印里面输出组件查找代码******************************
    /// <summary>
    /// 
    /// </summary>
    /// <param name="beginStr"></param>
    /// <param name="controlDic"></param>
    public static string DebugOutGetComponentDemo(FindConfig findtConfig)
    {
        //添加前缀
        string beginStr = findtConfig.isAddPrefix ? AddPrefix(findtConfig.beginStr) : string.Empty;
        //字典重新排列 重新排列
        Dictionary<string, List<string>> controlDicTemp = ReArrangeDic(findtConfig.controlDic);
        //打印
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("#region 获取组件模块");
        sb.AppendLine("/// <summary>");
        sb.AppendLine("/// 获取组件");
        sb.AppendLine("/// </summary>");
        sb.AppendLine("private void OnGetComponent()");
        sb.AppendLine("{");
        foreach (var item in controlDicTemp)
        {
            string itemKey = item.Key;
            //过滤模块
            switch (item.Key)
            {
                case "RectTransform":
                    itemKey = "Transform";
                    break;
            }
            switch (findtConfig.findComponentType)
            {
                case FindConfig.FindComponentType.UIFind:
                    foreach (var child in item.Value)
                        sb.AppendLine($"\t{child}{itemKey} = {beginStr}Get{itemKey}(\"{child}\");");
                    break;
                case FindConfig.FindComponentType.TfFing:
                    foreach (var child in item.Value)
                        sb.AppendLine($"\t{child}{itemKey} = {beginStr}OnGet{itemKey}(\"{child}\");");
                    break;
                default:
                    break;
            }


            sb.AppendLine();
        }
        sb.AppendLine("}");
        sb.AppendLine("#endregion");
        Debug.Log(sb.ToString());
        return sb.ToString();
    }

    //******************************Button按钮监听代码******************************
    /// <summary>
    /// 打印里面输出监听代码
    /// </summary>
    /// <param name="beginStr"></param>
    /// <param name="controlDic"></param>
    public static string DebugOutAddListenerDemo(FindConfig findtConfig)
    {
        //添加前缀
        string beginStr = AddPrefix(findtConfig.beginStr);
        //字典重新排列 重新排列
        Dictionary<string, List<string>> controlDicTemp = ReArrangeDic(findtConfig.controlDic);
        //打印
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("#region 按钮监听模块");
        sb.AppendLine("/// <summary>");
        sb.AppendLine("/// 按钮监听");
        sb.AppendLine("/// </summary>");
        sb.AppendLine("private void OnAddListener()");
        sb.AppendLine("{");
        foreach (var item in controlDicTemp)
        {
            string itemKey = item.Key;
            foreach (var child in item.Value)
            {
                switch (itemKey)
                {
                    case "Button":
                        //V_HeiShiButton.onClick.AddListener(cityUI.HeiShi); 模板
                        sb.AppendLine($"{child}{itemKey}.onClick.AddListener({beginStr}{child}AddListener);");
                        break;
                    case "Toggle":
                        //toggle.onValueChanged.AddListener(toggleAddListener); 模板
                        sb.AppendLine($"{child}{itemKey}.onValueChanged.AddListener({beginStr}{child}AddListener);");
                        break;
                    default:
                        break;
                }
            }
            sb.AppendLine();
        }
        sb.AppendLine("}");
        sb.AppendLine("#endregion");
        Debug.Log(sb.ToString());
        return sb.ToString();
    }
}



/// <summary>
/// Find类型查找配置文件
/// </summary>
public class FindConfig
{
    /// <summary>
    /// 查找类型
    /// </summary>
    public enum FindComponentType
    {
        UIFind,
        TfFing,
    }

    /// <summary>
    /// 查找类型
    /// </summary>
    public FindComponentType findComponentType { get; set; } = FindComponentType.UIFind;

    /// <summary>
    /// 关键词 例如V_
    /// </summary>

    public string KeyValue { get; set; }

    /// <summary>
    /// 是否添加前缀
    /// </summary>
    public bool isAddPrefix { get; set; } = false;

    /// <summary>
    /// 是否是GetSet生成
    /// </summary>
    public bool isGetSet { get; set; } = true;

    /// <summary>
    /// 用于代码生成输入的前缀
    /// </summary>
    public string beginStr { get; set; }

    /// <summary>
    /// 数据字典
    /// </summary>
    public Dictionary<string, List<Component>> controlDic { get; set; }

    public FindConfig()
    {
        controlDic = new Dictionary<string, List<Component>>();
    }
}
