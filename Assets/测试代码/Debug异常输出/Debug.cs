using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Debug
{
    public static void Log(object message)
    {
        UnityEngine.Debug.Log("zhiyuan1:" + message);
    }
    public static void LogWarning(object message)
    {
        UnityEngine.Debug.LogWarning("zhiyuan2:" + message);
    }
    public static void LogError(object message)
    {
        UnityEngine.Debug.LogError("zhiyuan3:" + message);
    }
}
