using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIOpen : MonoBehaviour
{
    private void Start()
    {
        UIManager.GetInstance().ShowPanel<LoginPanl>("LoginPanl", E_UI_Layer.Bot, ShowPanel0ver);
    }


    private void ShowPanel0ver(LoginPanl panel)
    {
        panel.InitInfo();
        //Invoke("DelayHide",1);
    }

    private void DelayHide()
    {
        UIManager.GetInstance().HidePanel("LoginPanl");
    }

}
