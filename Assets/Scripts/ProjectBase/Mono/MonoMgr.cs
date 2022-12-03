using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 1.可以提供给外部添加帧更新事件的方法
/// 2.可以提供给外部添加 协程的方
/// 3.变成单例模式
/// </summary>
public class MonoMgr : SingletonAutoMono<MonoMgr>
{
    public MonoMgr(MonoController controller)
    {
        DontDestroyOnLoad(gameObject);
    }

    private event UnityAction updateEvent;
    private event UnityAction fixUpdateEvent;
    private event UnityAction AwakeEvent;
    private event UnityAction StartEvent;

    protected override void Awake()
    {
        base.Awake();
        AwakeEvent?.Invoke();
    }
    private void Update() => updateEvent?.Invoke();
    private void FixedUpdate() => fixUpdateEvent?.Invoke();
    private void Start() => StartEvent?.Invoke();

    /// <summary>
    /// 给外部提供的 添加帧更新事件的函数
    /// </summary>
    /// <param name="fun"></param>
    public void AddUpdateListener(UnityAction fun)
    {
        updateEvent += fun;
    }

    /// <summary>
    /// 提供给外部 用于移除帧更新事件函数
    /// </summary>
    /// <param name="fun"></param>
    public void RemoveUpdateListener(UnityAction fun)
    {
        updateEvent -= fun;
    }

    /// <summary>
    /// 携程方法的使用
    /// </summary>
    /// <param name="routine"></param>
    /// <returns></returns>
    public Coroutine MonoMgrStartCoroutine(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }
    public Coroutine MonoMgrStartCoroutine(string methodName, [DefaultValue("null")] object value)
    {
        return StartCoroutine(methodName, value);
    }
    public Coroutine MonoMgrStartCoroutine(string methodName)
    {
        return StartCoroutine(methodName);
    }
}
