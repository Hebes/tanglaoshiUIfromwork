using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UITool.Generator
{
    public enum GeneratorType
    {
        Find,//组件查找
        Bind,//组件绑定
    }
    /// <summary>
    /// 解析的类型
    /// </summary>
    public enum ParseType
    {
        Name,
        Tag
    }

    /// <summary>
    /// 配置文件
    /// </summary>
    public class UISetting
    {
        public static string BindComponentGeneratorPath = Application.dataPath + "/Scripts/BindCompoent";
        public static string FindComponentGeneratorPath = Application.dataPath + "/Scripts/FindCompoent";
        public static string WindowGeneratorPath = Application.dataPath + "/Scripts/Window";
        public static string OBJDATALIST_KEY = "objDataList";
        public static GeneratorType GeneratorType = GeneratorType.Bind;
        public static ParseType ParseType = ParseType.Name;
        public static string[] TAGArr = { "Image", "RawImage", "Text", "Button", "Slider", "Dropdown", "InputField", "Canvas", "Panel", "ScrollRect", "Toggle" };
    }
}
