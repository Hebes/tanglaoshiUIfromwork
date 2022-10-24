using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test3 : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PoolMgr.GetInstance().GetObj("Test/Cube", (o) =>
            {
                o.transform.localScale = Vector3.one * 3;
            });
        }
        if (Input.GetMouseButtonDown(1))
        {
            //PoolMgr.GetInstance().GetObj("Test/Capsule");
        }
    }
}
