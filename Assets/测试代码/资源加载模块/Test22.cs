using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test22 : MonoBehaviour
{
    private void Awake()
    {
        ResMgr.GetInstance().LoadAsync<GameObject>("Test/Cube", (obj) =>
        {
            obj.transform.localScale = Vector3.one * 2;
            print("方块扩大了2,方块消失是方块自己挂载了脚本 对象池回收了");
        });
    }
}
