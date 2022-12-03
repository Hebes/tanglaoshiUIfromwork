using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Tool.ExcelChange;
using Tool.ExcelRead;
using UnityEngine;
using Tool;
using System;
using Newtonsoft.Json;

public class ExcelToJson
{
    /// <summary>
    /// 保存Jsonc#脚本的路径
    /// </summary>
    public static readonly string JsonCSharpSavePath = $"{Application.dataPath}/UIAutomateTool/Editor/OutPut/C#/JsonC#";
    public static readonly string JsonTxtSavePath = $"{Application.dataPath}/UIAutomateTool/Editor/OutPut/Json";


    private static readonly string data = "JsonData";
    private static readonly string info = "JsonInfo";
    private static readonly string config = "Config";
    /// <summary>
    /// 生成Json c#脚本
    /// </summary>
    public static void CreatJsonCSharp(string LoadExcelPath)
    {
        //解析Excel
        DataSet dataSet = ExcelReadData.ReadExcel(LoadExcelPath);
        UnityEngine.Debug.Log(dataSet);

        for (int d = 0; d < dataSet.Tables.Count; d++)
        {
            //解析成列表数据
            List<ExcelData> excelDatas = ExcelReadData.ParseExcelColumn(dataSet.Tables[d], out string tableName, 2, 5);
            string sb = JsonCSharp(tableName, excelDatas);
            ToolHelper.ChackFileAndWriter($"{JsonCSharpSavePath}/{tableName}{data}.cs", sb);
        }
    }

    /// <summary>
    /// 生成Json文本
    /// </summary>
    public static void CreatJsonText(string LoadExcelPath)
    {
        //解析Excel
        DataSet dataSet = ExcelReadData.ReadExcel(LoadExcelPath);
        UnityEngine.Debug.Log(dataSet);
        UnityEngine.Debug.Log("未完成");
        //解析成列表数据
        for (int d = 0; d < dataSet.Tables.Count; d++)
        {
            //解析成列表数据
            List<ExcelData> excelDatas1 = ExcelReadData.ParseExcelRow(dataSet.Tables[d], out string tableName1, 3, 5);
            List<ExcelData> excelDatas2 = ExcelReadData.ParseExcelRow(dataSet.Tables[d], out string tableName2, 5);
            string sb = JsonText($"{tableName1}", excelDatas1, excelDatas2);
            ToolHelper.ChackFileAndWriter($"{JsonTxtSavePath}/{tableName1}{config}.txt", sb);
        }
    }

    /// <summary>
    /// 解析json文件
    /// </summary>
    /// <param name="textAsset"></param>
    public static void ParseJson(TextAsset textAsset)
    {
        CharectJsonData charectJsonData = JsonConvert.DeserializeObject<CharectJsonData>(textAsset.text);
        List<CharectJsonInfo> charectJsonInfoList = charectJsonData.CharectJsonInfo;
        for (int i = 0; i < charectJsonInfoList.Count; i++)
        {
            UnityEngine.Debug.Log(charectJsonInfoList[i].Name);
        }
    }

    /// <summary>
    /// 生成C#代码
    /// </summary>
    /// <param name="tableName">表的名称</param>
    /// <param name="excelDataList">数据</param>
    private static string JsonCSharp(string tableName, List<ExcelData> excelDataList)
    {
        string str1 = tableName + data;
        string str2 = tableName + info;


        StringBuilder sb = new StringBuilder();

        //生成脚本内容
        //添加引用
        sb.AppendLine("/*---------------------------------");
        sb.AppendLine(" *Title:Excel解析转Json C#自动化成代码生成工具");
        sb.AppendLine(" *Author:暗沉");
        sb.AppendLine(" *Date:" + System.DateTime.Now);
        sb.AppendLine(" *Description:Excel解析转Json C#自动化成代码生成工具");
        sb.AppendLine(" *注意:以下文件是自动生成的，任何手动修改都会被下次生成覆盖,若手动修改后,尽量避免自动生成");
        sb.AppendLine("---------------------------------*/");
        sb.AppendLine("using UnityEngine.UI;");
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine();

        //前半部分
        sb.AppendLine($"\tpublic class {str1}");
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tpublic string table { get ; set; }");//public string table { get; set; }
        sb.AppendLine($"\t\tpublic List<{str2}> {str2} {"{ get ; set; }"}");//public List<CharectJsonInfo> CharectJsonInfo { get; set; }
        sb.AppendLine("\t}");

        //下半部分
        sb.AppendLine($"\tpublic class {str2}");
        sb.AppendLine("\t{");
        for (int e = 0; e < excelDataList?.Count; e++)
        {
            ExcelData excelDataListTemp = excelDataList[e];
            List<string> Excels = excelDataListTemp.ExcelDataInfo;

            if (Excels[1].Equals("Sprite"))//如果是图片
                Excels[1] = "string";

            sb.AppendLine("\t\t/// <summary>");
            sb.AppendLine($"\t\t/// {Excels[0]}");
            sb.AppendLine("\t\t/// </summary>");
            sb.AppendLine($"\t\tpublic {"string"} {Excels[2]}{"{ get ; set; }"}");
            sb.AppendLine();
        }
        sb.AppendLine("\t}");

        return sb.ToString();
    }

    private static string JsonText(string tableName, List<ExcelData> excelDatas1, List<ExcelData> excelDatas2)
    {
        /**
         * json模板请到heidisql工具里面复制
         **/

        List<string> excelDatasTemp1 = excelDatas1[0].ExcelDataInfo;//类型
        List<string> excelDatasTemp2 = excelDatas1[1].ExcelDataInfo;//名称

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("{");
        sb.AppendLine($"\t\"table\": \"{tableName}{data}\",");
        sb.AppendLine($"\t\"{tableName}{info}\":");
        sb.AppendLine("\t[");
        for (int e = 0; e < excelDatas2.Count; e++)
        {
            List<string> excelDatasTemp3 = excelDatas2[e].ExcelDataInfo;//内容
            sb.AppendLine("\t\t{");
            for (int c = 0; c < excelDatasTemp3.Count; c++)
            {
                switch (excelDatasTemp1[c])
                {
                    case "List<string>":
                    case "List<int>":
                    case "List<float>":
                        if (c< excelDatasTemp3.Count-1)
                            sb.AppendLine($"\t\t\t\"{excelDatasTemp2[c]}\": \"[{excelDatasTemp3[c]}]\",");
                        else
                            sb.AppendLine($"\t\t\t\"{excelDatasTemp2[c]}\": \"[{excelDatasTemp3[c]}]\"");
                        break;
                    case "bool":
                    case "BOOL":
                    case "Bool":
                        if (c < excelDatasTemp3.Count - 1)
                            sb.AppendLine($"\t\t\t\"{excelDatasTemp2[c]}\": \"{excelDatasTemp3[c].ToLower()}\",");
                        else
                            sb.AppendLine($"\t\t\t\"{excelDatasTemp2[c]}\": \"{excelDatasTemp3[c].ToLower()}\"");
                        break;
                    case "string":
                    case "Sprite":
                        if (c < excelDatasTemp3.Count - 1)
                            sb.AppendLine($"\t\t\t\"{excelDatasTemp2[c]}\": \"{excelDatasTemp3[c]}\",");
                        else
                            sb.AppendLine($"\t\t\t\"{excelDatasTemp2[c]}\": \"{excelDatasTemp3[c]}\"");
                        break;
                    default:
                        if (c < excelDatasTemp3.Count - 1)
                            sb.AppendLine($"\t\t\t\"{excelDatasTemp2[c]}\": {excelDatasTemp3[c]},");
                        else
                            sb.AppendLine($"\t\t\t\"{excelDatasTemp2[c]}\": {excelDatasTemp3[c]}");
                        break;
                }
            }

            
            if (e < excelDatasTemp3.Count)
                sb.AppendLine("\t\t},");
            else
                sb.AppendLine("\t\t}");
        }
        sb.AppendLine("\t]");
        sb.AppendLine("}");

        return sb.ToString();
    }
}
