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
            //savePath = $"{Application.persistentDataPath}/���Դ���/Debug�����쳣���/",//Assets/���Դ���/Debug�����쳣���
            savePath = $"{Application.dataPath}/���Դ���/Debug�����쳣���/",//Assets/���Դ���/Debug�����쳣���
            saveName = "Debug�����쳣���.txt",
        };
        PELog.InitSettings(cfg);
        PELog.Log("����NoneGameState");

        PELog.Log($"{"sdsdsd"}Hello word");
        PELog.Log(LogCoLor.DarkRed, $"{LogCoLor.DarkRed.ToString()}Hello word");
        PELog.Log(LogCoLor.Green, $"{LogCoLor.Green.ToString()}Hello word");
        PELog.Log(LogCoLor.Blue, $"{LogCoLor.Blue.ToString()}Hello word");
        PELog.Log(LogCoLor.Cyan, $"{LogCoLor.Cyan.ToString()}Hello word");
        PELog.Log(LogCoLor.Magenta, $"{LogCoLor.Magenta.ToString()}Hello word");
        PELog.Log(LogCoLor.DarkYellow, $"{LogCoLor.DarkYellow.ToString()}Hello word");
        PELog.Trace("��ӡ��ջsss", "qwe");
    }
}
