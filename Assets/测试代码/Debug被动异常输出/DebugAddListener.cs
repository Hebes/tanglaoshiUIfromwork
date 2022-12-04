using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DebugAddListener : SingletonAutoMono<DebugAddListener>
{

    //private bool IsQuitWhenException=true;

    protected override void Awake()
    {
        base.Awake();
        Application.logMessageReceived += Handler;
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= Handler;
    }


    void Handler(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception || type == LogType.Assert)
        {

            UnityEngine.Debug.Log("接收到异常信息" + logString);
            //Assets/测试代码/Debug异常输出
            string path = $"{Application.dataPath}/测试代码/Debug异常输出/";
            Debug.Log(path);
            string logPath = Path.Combine(path, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")) + ".log";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (Directory.Exists(path))
            {
                File.AppendAllText(logPath, "[time]:" + DateTime.Now.ToString() + "\r\n");
                File.AppendAllText(logPath, "[type]:" + type.ToString() + "\r\n");
                File.AppendAllText(logPath, "[exception message]:" + logString + "\r\n");
                File.AppendAllText(logPath, "[stack Trace]:" + stackTrace + "\r\n");
            }
//            if (IsQuitWhenException)
//            {
//#if UNITY_EDITOR
//                UnityEditor.EditorApplication.isPlaying = false;
//#else
//                                Application.Quit();
//#endif
//            }
        }
    }
}
