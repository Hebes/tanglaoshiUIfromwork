using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //主入口
        //初始化道具信息
        BagMgr.GetInstance().InitItemsInfo();
        //显示背包面板
        UIManager.GetInstance().ShowPanel<BagPanel>("BagPanel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
