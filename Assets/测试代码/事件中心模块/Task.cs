using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{

    public void Start() => EventCenter.GetInstance().AddEventListener<Monster>("MonsterDead", TaskWaitMonsterDeadDo);
    public void OnDestroy() => EventCenter.GetInstance().RemoveEventListener<Monster>("MonsterDead", TaskWaitMonsterDeadDo);
    /// <summary>
    ///怪物死时要做些什么
    /// </ summary>
    public void TaskWaitMonsterDeadDo(Monster info)
    {
        Debug.Log("任务记录");
    }
}
