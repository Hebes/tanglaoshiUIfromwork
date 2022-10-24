using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例测试方法，继承SingletonAutoMono
/// </summary>
public class Test1 : SingletonAutoMono<Test1>
{
    /// <summary>
    /// 测试方法
    /// </summary>
    public void a()
    {
        print("测试单例方法"+1);
    }
}
