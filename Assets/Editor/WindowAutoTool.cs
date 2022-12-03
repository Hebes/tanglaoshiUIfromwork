using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;
using Tool.ExcelRead;

namespace Tool.ExcelChange
{

    public class WindowAutoTool : EditorWindow
    {
        /// <summary>点击加载路径</summary>
        private bool isSelectFile;
        /// <summary>加载的路径</summary>
        public static string LoadExcelPath = string.Empty;
        /// <summary>保存路径数据持久化</summary>
        private string LoadExcelPathKey = "LoadExcelPathKey";
        /// <summary>提示信息</summary>
        private string MessageInfo = "请点击一个xls或xlsx文件";
        private System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

        [MenuItem("Assets/自动化工具窗口窗口(Shift+A) #A")]
        [MenuItem("Tool/自动化工具窗口窗口(Shift+A) #A", false, 0)]
        public static void ExcelChangeWindow() => EditorWindow.GetWindow(typeof(WindowAutoTool), false, "Excel转换窗口").Show();


        private void OnGUI()
        {
            EditorGUILayout.BeginVertical(GUILayout.Width(position.width), GUILayout.Height(position.height));
            {
                GUILayout.Space(5f);
                EditorGUILayout.LabelField("加载单个Excel文件", EditorStyles.label);
                EditorGUILayout.BeginVertical("box");
                {
                    //****************************路径文件****************************
                    EditorGUILayout.BeginHorizontal();
                    {
                        //选择Excel文件 路径版
                        EditorGUILayout.LabelField("选择的Excel文件路径:", EditorStyles.label, GUILayout.Width(130));
                        LoadExcelPath = EditorGUILayout.TextField(PlayerPrefs.GetString(LoadExcelPathKey));
                        if (GUILayout.Button("Browse...", GUILayout.Width(80f))) { BrowseLoadFilePanel(); }

                        //选择加载文件 选择版
                        isSelectFile = EditorGUILayout.ToggleLeft("点击加载文件路径", isSelectFile, GUILayout.Width(130f));
                        if (isSelectFile)
                            ClickFileLoadPath();
                        else
                        {
                            if (PlayerPrefs.GetString(LoadExcelPathKey).Equals(MessageInfo))
                                PlayerPrefs.DeleteKey(LoadExcelPathKey);
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    //****************************Asset文件****************************
                    GUILayout.Space(5f);
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField("Asset文件：", EditorStyles.label, GUILayout.Width(70f));
                        //生成Asset C#数据脚本
                        if (GUILayout.Button("生成C#解析脚本", GUILayout.Width(110f)))
                        {
                            if (LoadExcelPath.Equals(string.Empty)) { if (EditorUtility.DisplayDialog("消息提示", "请选择路径", "确定")) { return; } }


                            //解析Excel
                            DataSet dataSet = ExcelToAsset.ReadExcel(LoadExcelPath);

                            for (int d = 0; d < dataSet.Tables.Count; d++)
                            {
                                //解析成列表数据
                                List<ExcelData> excelDatas = ExcelReadData.ParseExcelColumn(dataSet.Tables[d], out string tableName, 2, 5);
                                //C#文件内容
                                StringBuilder stringBuilder = ExcelToAsset.CreatAssetCshorp(tableName, excelDatas);
                                //保存文件
                                ExcelToAsset.SaveCSarp(ExcelToAsset.assetCSharpSavePath, tableName, stringBuilder.ToString());
                            }
                            //刷新编辑器
                            AssetDatabase.Refresh();
                        }

                        //生成AssetDat Asset
                        if (GUILayout.Button("生成AssetData", GUILayout.Width(110f)))
                        {
                            sw.Start();
                            //删除文件夹内所有的Asset文件
                            string[] filesName = Directory.GetFiles(ExcelToAsset.assetSavePath, "*.asset");
                            for (int i = 0; i < filesName?.Length; i++)
                            {
                                UnityEngine.Debug.Log("清除已存在的文件...");
                                File.Delete(filesName[i]);
                                UnityEngine.Debug.Log("清除成功!");
                            }
                            //解析Excel
                            DataSet dataSet = ExcelToAsset.ReadExcel(LoadExcelPath);
                            //解析成字典数据
                            for (int d = 0; d < dataSet.Tables.Count; d++)
                            {
                                //读取表中的一个工作表的内容
                                DataTable dataTable = dataSet.Tables[d];
                                //解析工作表的内容 5 是开始开始的行数
                                List<ExcelData> excelDatas = ExcelReadData.ParseExcelRow(dataTable, out string tableName, 5, dataTable.Rows.Count);//tableName表名

                                //方法一
                                //实例化需要赋值的类
                                Type charect = Type.GetType(ExcelToAsset.nameSpaceName + "." + tableName);//获取类
                                //反射创建列表
                                Type list = typeof(List<>).MakeGenericType(charect);//创建实力类列表
                                object charectList = Activator.CreateInstance(list); //实例化列表
                                MethodInfo addMethod = charectList.GetType().GetMethod("Add");//反射列表的添加方法
                                //添加数据
                                for (int e = 0; e < excelDatas.Count; e++)
                                {
                                    object instanceClass = Activator.CreateInstance(charect);//实例化类等同new
                                    Type instanceClassType = instanceClass.GetType();//以下是添加数据
                                    FieldInfo[] fieldInfos = instanceClassType.GetFields();
                                    for (int i = 0; i < excelDatas[e].ExcelDataInfo.Count; i++)
                                    {
                                        fieldInfos[i].SetValue(instanceClass, ExcelToAsset.GetTypeForExcel(excelDatas[e].ExcelDataInfo[i], fieldInfos[i].FieldType));
                                    }
                                    addMethod.Invoke(charectList, new object[] { instanceClass });//执行添加方法，并添加数据 charectList需要实例化才能执行方法
                                }
                                //执行生成方法
                                Type charectData = Type.GetType(ExcelToAsset.nameSpaceName + "." + tableName + "AssetData");//获取类
                                ScriptableObject.CreateInstance(ExcelToAsset.nameSpaceName + "." + tableName + "AssetData");
                                object charectDataNew = Activator.CreateInstance(charectData);  //实例化反射
                                MethodInfo methodInfo = charectData.GetMethod("CreatAsset");    //获取方法
                                methodInfo.Invoke(charectDataNew, new object[] { charectList });//传入参数
                                sw.Stop();
                                UnityEngine.Debug.Log($"用时total: {sw.ElapsedMilliseconds} ms");
                                #region 废弃代码
                                //方法二
                                //Type charect = Type.GetType("Asset." + tableName+ "AssetData");//获取类
                                //object instanceCharect = Activator.CreateInstance(charect);//实例化类等同new
                                //MethodInfo methodInfo = charect.GetMethod("Set" + tableName + "ListInfo");//获取方法
                                //object obj = methodInfo.Invoke(instanceCharect, new object[] { excelDatas });//传入参数
                                #endregion
                            }
                        }

                        //一键清除AssetDat Asset
                        if (GUILayout.Button("一键清除Asset", GUILayout.Width(110f)))
                        {
                            //删除文件夹内所有的Asset文件
                            sw.Start();
                            string[] filesName = Directory.GetFiles(ExcelToAsset.assetSavePath, "*.asset");
                            for (int i = 0; i < filesName.Length; i++)
                            {
                                UnityEngine.Debug.Log("清除已存在的文件...");
                                File.Delete(filesName[i]);
                                File.Delete(filesName[i]+".meta");
                                UnityEngine.Debug.Log("清除成功!");
                            }
                            AssetDatabase.Refresh();
                            sw.Stop();
                            UnityEngine.Debug.Log($"用时total: {sw.ElapsedMilliseconds} ms");
                        }

                        //测试解析Asset
                        if (GUILayout.Button("测试解析Asset", GUILayout.Width(110f)))
                        {
                            //加载为空的问题 https://tieba.baidu.com/p/2888725862
                            //UnityEngine.Object[] obj = AssetDatabase.LoadAllAssetsAtPath("Assets/CharectAssetData.asset");

                            //foreach (var item in obj)
                            //{
                            //    foreach (var item1 in (item as CharectAssetData).CharectList)
                            //    {
                            //        Debug.Log(item1.ID);
                            //    }
                            //}
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    //****************************Json文件****************************
                    GUILayout.Space(5f);//垂直布局下换一行
                    EditorGUILayout.BeginHorizontal();//水平布局
                    {
                        EditorGUILayout.LabelField("Json文件：", EditorStyles.label, GUILayout.Width(70f));

                        //Json的c#解析文件
                        if (GUILayout.Button("Json的c#解析文件", GUILayout.Width(110f)))
                        {
                            ExcelToJson.CreatJsonCSharp(LoadExcelPath);
                        }

                        //生成Json解析文件
                        if (GUILayout.Button("生成Json解析文件", GUILayout.Width(110f)))
                        {
                            ExcelToJson.CreatJsonText(LoadExcelPath);
                        }

                        //测试Json解析
                        if (GUILayout.Button("测试Json解析", GUILayout.Width(110f)))
                        {
                            TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/UIAutomateTool/Editor/OutPut/Json/CharectConfig.Txt");
                            ExcelToJson.ParseJson(textAsset);
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    //****************************CSV文件****************************
                    GUILayout.Space(5f);//垂直布局下换一行
                    EditorGUILayout.BeginHorizontal();//水平布局
                    {
                        EditorGUILayout.LabelField("CSV文件：", EditorStyles.label, GUILayout.Width(70f));
                        //生成Json解析文件
                        if (GUILayout.Button("生成CSV解析文件", GUILayout.Width(110f)))
                        {
                            UnityEngine.Debug.Log("暂时没写");
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndVertical();

        }


        /// <summary>
        /// 打开文件夹
        /// </summary>
        private void BrowseLoadFolderPanel()
        {
            string directory = EditorUtility.OpenFolderPanel("选择Execl文件", LoadExcelPath, string.Empty);
            if (!string.IsNullOrEmpty(directory)) { LoadExcelPath = directory; }
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        private void BrowseLoadFilePanel()
        {
            string directory = EditorUtility.OpenFilePanel("选择Execl文件", LoadExcelPath, "xls,xlsx,csv");
            if (!string.IsNullOrEmpty(directory))
            {
                LoadExcelPath = directory;
                PlayerPrefs.SetString(LoadExcelPathKey, directory);
            }
        }

        /// <summary>
        /// 点击文件加载路径,Unity专用
        /// </summary>
        private void ClickFileLoadPath()
        {
            if (Selection.activeObject != null)
            {
                SelectionChange();
                string path;
                path = AssetDatabase.GetAssetPath(Selection.activeObject);//选择的文件的路径 
                if (path.Contains("xls") || path.Contains("xlsx"))
                {
                    path = path.Split("Assets")[1];
                    LoadExcelPath = string.Format($"{Application.dataPath}{path}");
                    PlayerPrefs.SetString(LoadExcelPathKey, LoadExcelPath);
                }
                else
                {
                    PlayerPrefs.SetString(LoadExcelPathKey, MessageInfo);
                }
            }
        }

        /// <summary>
        /// 选择立刻刷新显示
        /// </summary>
        private void SelectionChange() => Repaint();
    }
}
