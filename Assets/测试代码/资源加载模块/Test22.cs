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
            print("方块扩大了2");
        });
    }
}
