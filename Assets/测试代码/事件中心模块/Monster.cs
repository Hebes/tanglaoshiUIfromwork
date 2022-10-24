using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//流程   怪物死亡--->玩家获得奖励---->任务记录----->其他
/// <summary>
/// 怪物
/// </summary>
public class Monster : MonoBehaviour
{

    public new string name = "123123";

    // Start is called before the first frame update
    void Start()
    {
        Dead();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Dead()
    {
        Debug.Log("怪物死亡");
        //其它对象想在怪物死亡时做点什么
        //比如
        //1.玩家得奖励
        //2.任务记录
        //3.其它(比如成就记录,比如副本继续创建怪物等等等)
        //加N个处理逻辑

        //触发事件|
        EventCenter.GetInstance().EventTrigger("MonsterDead",this);


    }
}
