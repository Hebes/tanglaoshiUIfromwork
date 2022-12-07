using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Tool.ExcelRead;
using UnityEditor;
using UnityEngine;

namespace Tool.ExcelChange
{
    public class ExcelToAsset
    {
        /// <summary>
        /// 保存C#脚本的路径 Assets/UIAutomatedTool/Editor/OutPut/C#/AssetC#
        /// </summary>
        public static readonly string assetCSharpSavePath = Application.dataPath + "/Editor/OutPut/C#/AssetC#";
        /// <summary>
        /// 保存asset的路径 Assets/UIAutomatedTool/Editor/OutPut/Assets
        /// </summary>
        public static readonly string assetSavePath = "Assets/Editor/OutPut/Assets";
        /// <summary>
        /// 加载图片的路径 后续可自己修改 默认可以加载png 和 jpg
        /// </summary>
        public static readonly string loadImagePath = "Assets/Editor/ExcleChange/Image";
        /// <summary>
        /// 命名空间
        /// </summary>
        public const string nameSpaceName = "Asset";

        /// <summary>
        /// 生成成C#脚本
        /// </summary>
        /// <param name="CSharpName">脚本名称</param>
        /// <param name="content">内容</param>
        public static void SaveCSarp(string path, string CSharpName, string content)
        {
            //生成的文件路径
            string savePath = path + "/" + CSharpName + "AssetData.cs";
            //写入文件代码
            ToolHelper.ChackPath(path, savePath);
            ToolHelper.FileWriteContent(savePath, content);
            #region 旧版代码可直接用
            ////检查是否有这个路径的存在
            //if (!Directory.Exists(path))
            //{
            //    Debug.Log("路径不存在,进行创建...");
            //    Directory.CreateDirectory(path);
            //    Debug.Log("创建成功!");
            //}
            ////检查是否有这个文件的存在
            //if (File.Exists(savePath))
            //{
            //    Debug.Log("文件已存在,进行删除...");
            //    File.Delete(savePath);
            //    Debug.Log("删除成功!");
            //}
            ////生成文件
            //StreamWriter writer = File.CreateText(savePath);
            //writer.Write(content);
            //writer.Close();
            #endregion
        }

        /// <summary>
        /// 生成c#解析文件
        /// </summary>
        /// <param name="excelDataInfoList"></param>
        public static StringBuilder CreatAssetCshorp(string tableName, List<ExcelData> excelDataDic)
        {
            StringBuilder sb = new StringBuilder();
            string str2 = tableName + nameSpaceName + "Data";
            string TableName = tableName;
            //生成脚本内容
            //添加引用
            sb.AppendLine("/*---------------------------------");
            sb.AppendLine(" *Title:Excel转Asset自动化成代码生成工具");
            sb.AppendLine(" *Author:暗沉");
            sb.AppendLine(" *Date:" + System.DateTime.Now);
            sb.AppendLine(" *Description:Excel转Asset自动化成代码生成工具");
            sb.AppendLine(" *注意:以下文件是自动生成的，任何手动修改都会被下次生成覆盖,若手动修改后,尽量避免自动生成");
            sb.AppendLine("---------------------------------*/");
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine("using UnityEditor;");
            sb.AppendLine("using UnityEngine.UI;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine();

            //生成命名空间
            if (!string.IsNullOrEmpty(nameSpaceName))
            {
                sb.AppendLine($"namespace {nameSpaceName}");
                sb.AppendLine("{");
            }
            //前半部分
            sb.AppendLine($"\t[CreateAssetMenu(fileName =\"{str2}\", menuName = \"Inventory/{TableName}\")]");//new
            sb.AppendLine($"\tpublic class {str2 + ":ScriptableObject"}");
            sb.AppendLine("\t{");
            sb.AppendLine($"\t\tpublic List<{TableName}> {TableName}List;");

            //前半部分生成Asste代码 方法一
            sb.AppendLine("\t\t/// <summary>");
            sb.AppendLine($"\t\t/// Asset生成代码 方法一");
            sb.AppendLine("\t\t/// </summary>");
            sb.AppendLine($"\t\tpublic void CreatAsset(List<{TableName}> {TableName}s)");
            sb.AppendLine("\t\t{");
            sb.AppendLine($"\t\t\t{str2} manager = ({str2})ScriptableObject.CreateInstance<{str2}>();");
            sb.AppendLine($"\t\t\tmanager.{TableName}List = {TableName}s;");
            sb.AppendLine($"\t\t\tAssetDatabase.CreateAsset(manager,\"{assetSavePath}/{str2}.asset\");");//生成Asset文件的路径
            sb.AppendLine("\t\t\tAssetDatabase.SaveAssets();");
            sb.AppendLine("\t\t\tAssetDatabase.Refresh();");
            sb.AppendLine("\t\t}");
           
            sb.AppendLine("\t}");
            sb.AppendLine();

            //后半部分
            sb.AppendLine("\t[System.Serializable]");
            sb.AppendLine($"\tpublic class {TableName}");
            sb.AppendLine("\t{");
            for (int e = 0; e < excelDataDic.Count; e++)
            {
                List<string> Excels = excelDataDic[e].ExcelDataInfo;
                sb.AppendLine("\t\t/// <summary>");
                sb.AppendLine($"\t\t/// {Excels[0]}");
                sb.AppendLine("\t\t/// </summary>");
                sb.AppendLine($"\t\t[Header(\"{Excels[0]}\")]");
                sb.AppendLine($"\t\tpublic {Excels[1] + " " + Excels[2]};");
            }
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            return sb;
        }

        /// <summary>
        /// 读取excel文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="columnnum">列数</param>
        /// <param name="rownum">行数</param>
        /// <returns></returns>
        public static DataSet ReadExcel(string filePath)
        {
            //打开文件
            FileStream stream = null;
            try { stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read); }
            catch (Exception) { if (EditorUtility.DisplayDialog("消息提示", "请检查文件路径!\n请关闭打开的文件!", "确定")) { return null; } }
            //判断加载的文件类型 解析
            string[] fileType = filePath.Split('.');
            IExcelDataReader excelReader = null;
            switch (fileType[1])//报错异常处理 https://blog.csdn.net/qq_39221436/article/details/120951176
            {
                case "xls": excelReader = ExcelReaderFactory.CreateBinaryReader(stream); break;
                case "xlsx": excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream); break;
            }
            return excelReader.AsDataSet();
        }

        /// <summary>
        /// 反射数据类型并赋值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetTypeForExcel(string str, Type type)
        {
            if (type == typeof(int)) { return int.Parse(str); }
            else if (type == typeof(float)) { return float.Parse(str); }
            else if (type == typeof(string)) { return str; }
            else if (type == typeof(bool)) { return str.ToLower() == "ture" ? true : false; }
            else if (type == typeof(List<string>)) { return str.Split(',').ToList(); }
            else if (type == typeof(List<int>))
            {
                List<string> vs = str.Split(',').ToList();
                return vs.Select<string, int>(a => Convert.ToInt32(a)).ToList(); ;
            }
            else if (type == typeof(List<float>))
            {
                List<string> vs = str.Split(',').ToList();
                return vs.Select<string, float>(a => Convert.ToSingle(a)).ToList(); ;
            }
            else if (type == typeof(Sprite))
            {
                Sprite sprite = null;
                //编辑器加载图片png
                string pngPath = string.Format($"{loadImagePath}/{str}.png");
                sprite = AssetDatabase.LoadAssetAtPath<Sprite>(pngPath);
                if (sprite!=null)
                    return sprite;
                //编辑器加载图片jpg
                string jpgPath = string.Format($"{loadImagePath}/{str}.jpg");
                sprite = AssetDatabase.LoadAssetAtPath<Sprite>(jpgPath);
                if (sprite != null)
                    return sprite;
                UnityEngine.Debug.Log(string.Format("<color=#FFFF00>未知类型图片,请在上面代码中添加!</color>"));
                return null;
            }
            else 
            {
                UnityEngine.Debug.Log(string.Format("<color=#FFFF00>未知类型,请在上面代码中添加!</color>")); return null; 
            }
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="url"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        //IEnumerator LoadByURL(string url, Action handler)
        //{

        //    using (UnityWebRequest uwr = UnityWebRequest.Get(url))
        //    {
        //        DownloadHandlerTexture downloadTexture = new DownloadHandlerTexture(true);
        //        uwr.downloadHandler = downloadTexture;
        //        yield return uwr.SendWebRequest();
        //        if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
        //        {
        //            Debug.Log(uwr.error);
        //        }
        //        else
        //        {
        //            Texture2D t = downloadTexture.texture;
        //            Sprite.Create(t, new Rect(0, 0, t.width, t.height), Vector2.one);
        //            handler?.Invoke();
        //        }
        //        yield return null;
        //    }
        //}
    }
}