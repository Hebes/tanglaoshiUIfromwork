using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;


namespace Tool
{
    /// <summary>
    /// unity 编辑器右击创建txt
    /// https://gameinstitute.qq.com/community/detail/126575
    /// </summary>
    public class CreatCSharp : EditorWindow
    {
        [MenuItem("Assets/创建自定义C#")]
        public static void CreatCshipFile()
        {
            ShowUIWindow();
            //var selectPath = AssetDatabase.GetAssetPath(Selection.activeObject); 
            //Debug.Log(selectPath);
            //string[] vs = selectPath.Split("Assets");
            //File.WriteAllText(Application.dataPath + "/" + vs[1], CshiopFileContents().ToString(), Encoding.UTF8);
            //AssetDatabase.Refresh();
        }

        private string scriptContent;
        private string filePath;
        private Vector2 scroll = new Vector2();

        private static void ShowUIWindow()
        {
            //创建代码展示窗口
            //CreatCSharpWindow window = (CreatCSharpWindow)GetWindowWithRect(typeof(CreatCSharpWindow), new Rect(100, 50, 500, 600), true, "Window生成界面");
            CreatCSharpWindow window = (CreatCSharpWindow)GetWindow(typeof(CreatCSharpWindow), false, "C#代码生成窗口");
            window.Show();
        }

        private void OnGUI()
        {
            //绘制ScroView
            scroll = EditorGUILayout.BeginScrollView(scroll);// GUILayout.Height(600), GUILayout.Width(800)
            EditorGUILayout.TextArea(scriptContent);
            EditorGUILayout.EndScrollView();
            EditorGUILayout.Space();

            //绘制脚本生成路径
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextArea("脚本生成路径：" + filePath);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            //绘制按钮
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("生成脚本", GUILayout.Height(30)))
            {
                //按钮事件
                // ButtonClick();
            }
            EditorGUILayout.EndHorizontal();

        }

        private static StringBuilder CreatCSharpContents()
        {
            StringBuilder sb = new StringBuilder();
            //添加引用
            sb.AppendLine("/*---------------------------------");
            sb.AppendLine(" *Title:UI表现层脚本自动化生成工具");
            sb.AppendLine(" *Author:暗沉");
            sb.AppendLine(" *Date:" + System.DateTime.Now);
            sb.AppendLine(" *Description:UI 表现层，该层只负责界面的交互、表现相关的更新，不允许编写任何业务逻辑代码");
            sb.AppendLine(" *注意:以下文件是自动生成的，再次生成不会覆盖原有的代码，会在原有的代码上进行新增，可放心使用");
            sb.AppendLine("---------------------------------*/");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("System.Collections;");
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine();

            return sb;
        }
    }
}

