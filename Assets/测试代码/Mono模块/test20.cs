using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// mono模块,这个是没有继承MonoBehaviour的
/// </summary>
public class TestTest
{
    /// <summary>
    /// 携程使用
    /// </summary>
    public TestTest()
    {
        MonoMgr.GetInstance().StartCoroutine(Test123());
    }
    IEnumerator Test123()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("协程调用启动成功");
    }


    /// <summary>
    /// 通过mono使用Updata
    /// </summary>
    public void CurrentUpdate()
    {
        Debug.Log("通过监听mono在Updata调用成功");
    }
}

public class test20 : MonoBehaviour
{
    void Start()
    {
        //创建新对象
        TestTest t = new TestTest();
        //监听调用Updata方法
        MonoMgr.GetInstance().AddUpdateListener(t.CurrentUpdate);
    }
}
