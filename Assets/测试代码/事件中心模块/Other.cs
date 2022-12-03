using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Other : MonoBehaviour
{
    void Start()
    {
        EventCenter.GetInstance().AddEventListener<Monster>("MonsterDead", otherWaitMonsterDeadDo);
        //不需要参数的事件
        EventCenter.GetInstance().AddEventListener("Win", Win);
        //触发
        //EventCenter . GetInstance(). EventTrigger<Monster>("MonsterDead", 怪物参数);
        EventCenter.GetInstance().EventTrigger("Win");
    }
    void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener<Monster>("MonsterDead", otherWaitMonsterDeadDo);
        EventCenter.GetInstance().RemoveEventListener("win", Win);
    }

    public void Win()
    {
        Debug.Log("只需要无参无返回值的");
    }

    /// <summary>
    ///怪物死时要做些什么
    /// </ summary>
    public void otherWaitMonsterDeadDo(Monster info)
    {
        Debug.Log("其它各个对象要做的事");
    }
}
