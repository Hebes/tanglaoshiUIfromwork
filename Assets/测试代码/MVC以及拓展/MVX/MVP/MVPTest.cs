using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVPTest : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            //显示主面板
            MainPresenter.ShowMe();
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            //隐藏主面板
            MainPresenter.HideMe();
        }
    }
}
