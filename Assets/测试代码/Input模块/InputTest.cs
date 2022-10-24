using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //开启输入检测
        InputMgr.GetInstance().StartOrEndCheck(true);

        //添加事件监听
        EventCenter.GetInstance().AddEventListener<KeyCode>("某键按下", CheckInputDown);
        EventCenter.GetInstance().AddEventListener<KeyCode>("某键抬起", CheckInputUp);
    }

    private void CheckInputDown(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.W:
                Debug.Log("停止前进");
                break;
            case KeyCode.A:
                Debug.Log("停止左转");
                break;
            case KeyCode.S:
                Debug.Log("停止后退");
                break;
            case KeyCode.D:
                Debug.Log("停止右转");
                break;
        }

    }
    private void CheckInputUp(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.W:
                Debug.Log("停止前进");
                break;
            case KeyCode.A:
                Debug.Log("停止左转");
                break;
            case KeyCode.S:
                Debug.Log("停止后退");
                break;
            case KeyCode.D:
                Debug.Log("停止右转");
                break;
        }
    }

}
