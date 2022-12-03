using Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Tool.ExcelChange;
using UnityEditor;
using UnityEngine;

namespace Tool.ExcelRead
{
    /// <summary>
    /// Excel数据结构
    /// </summary>
    public class ExcelData
    {
        public List<string> ExcelDataInfo;
    }

    public class ExcelReadData
    {
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
        /// 单个表解析--按列循环读取
        /// </summary>
        /// <param name="dataTable">单个表的数据</param>
        /// <param name="startRowNum">开始的行数</param>
        /// <param name="endRowNum">结束的行数</param>
        /// <returns></returns>
        public static List<ExcelData> ParseExcelColumn(DataTable dataTable, out string tableName, int startRowNum = 2, int endRowNum = 5)
        {
            tableName = dataTable.Rows[1][1].ToString();//文件名
            List<ExcelData> excelDatas = new List<ExcelData>();
            int columnNum = dataTable.Columns.Count;
            for (int c = 0; c < columnNum; c++)//列数
            {
                excelDatas.Add(new ExcelData()
                {
                    ExcelDataInfo = new List<string>(),
                });
                for (int r = startRowNum; r < endRowNum; r++) //行数
                {
                    excelDatas[c].ExcelDataInfo.Add(dataTable.Rows[r][c].ToString());//https://blog.csdn.net/yanhuatangtang/article/details/74530991
                }
            }
            return excelDatas;
        }

        /// <summary>
        /// 单个表解析--按行循环读取
        /// </summary>
        /// <param name="dataTable">单个表的数据</param>
        /// <param name="tableName">开始的行数</param>
        /// <param name="startRowNum">开始的行数</param>
        /// <param name="endRowNum">结束的行数</param>
        /// <returns></returns>
        public static List<ExcelData> ParseExcelRow(DataTable dataTable, out string tableName, int startRowNum, int endRowNum = -1)
        {
            tableName = dataTable.Rows[1][1].ToString();//文件名
            List<ExcelData> excelDatas = new List<ExcelData>();

            int columnNum = dataTable.Columns.Count;//获取总列数

            if (endRowNum == -1)//如果不填写的话
                endRowNum =dataTable.Rows.Count;//获取总行数

            for (int r = startRowNum; r < endRowNum; r++)//行数
            {
                ExcelData excelRowData = new ExcelData()
                {
                    ExcelDataInfo = new List<string>(),
                };
                for (int c = 0; c < columnNum; c++)
                {
                    excelRowData.ExcelDataInfo.Add(dataTable.Rows[r][c].ToString());
                }
                excelDatas.Add(excelRowData);
            }
            return excelDatas;
        }
    }


}
