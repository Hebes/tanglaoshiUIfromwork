using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    void Start() => EventCenter.GetInstance().AddEventListener<Monster>("MonsterDead", MonsterDeadDo);
    void OnDestroy() => EventCenter.GetInstance().RemoveEventListener<Monster>("MonsterDead", MonsterDeadDo);
    /// <summary>
    ///怪物死时要做些什么
    /// </ summary>
    public void MonsterDeadDo(object info)
    {
        Debug.Log("玩家得奖励" + (info as Monster).name);
    }
}
