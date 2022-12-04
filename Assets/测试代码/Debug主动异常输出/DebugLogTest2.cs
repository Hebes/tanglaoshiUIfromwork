using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LogUtils;

public class DebugLogTest2 : MonoBehaviour
{
    private void Start()
    {
        LogConfig cfg = new LogConfig()
        {
            eLoggerType = LoggerType.Unity,
            enableSave = true,
            //savePath = $"{Application.persistentDataPath}/测试代码/Debug主动异常输出/",//Assets/测试代码/Debug主动异常输出
            savePath = $"{Application.dataPath}/测试代码/Debug主动异常输出/",//Assets/测试代码/Debug主动异常输出
            saveName = "Debug主动异常输出.txt",
        };
        PELog.InitSettings(cfg);
        PELog.Log("进入NoneGameState");

        PELog.Log($"{"sdsdsd"}Hello word");
        PELog.Log(LogCoLor.DarkRed, $"{LogCoLor.DarkRed.ToString()}Hello word");
        PELog.Log(LogCoLor.Green, $"{LogCoLor.Green.ToString()}Hello word");
        PELog.Log(LogCoLor.Blue, $"{LogCoLor.Blue.ToString()}Hello word");
        PELog.Log(LogCoLor.Cyan, $"{LogCoLor.Cyan.ToString()}Hello word");
        PELog.Log(LogCoLor.Magenta, $"{LogCoLor.Magenta.ToString()}Hello word");
        PELog.Log(LogCoLor.DarkYellow, $"{LogCoLor.DarkYellow.ToString()}Hello word");
        PELog.Trace("打印堆栈sss", "qwe");
    }
}
