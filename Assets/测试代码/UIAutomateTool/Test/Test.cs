using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{

    public Button button1;//无BoxCollider
    public Button button2;//不启用BoxCollider
    public Button button3;//启用BoxCollider

    public GameObject gameObject1;//无BoxCollider
    public GameObject gameObject2;//不启用BoxCollider
    public GameObject gameObject3;//启用BoxCollider

    public Text text1;//无BoxCollider
    public Text text2;//不启用BoxCollider
    public Text text3;//启用BoxCollider

    //private int count = 10;

    private void Awake()
    {
        button1.onClick.AddListener(() =>
        {
            CreatObj("无BoxCollider", gameObject1, text1, 0);
        });
        button2.onClick.AddListener(() =>
        {
            CreatObj("不启用BoxCollider", gameObject1, text2, 1);
        });
        button3.onClick.AddListener(() =>
        {
            CreatObj("启用BoxCollider", gameObject1, text3, 2);
        });
    }

    /// <summary>
    ///实例化物体 
    /// </summary>
    private void CreatObj(string str, GameObject gameObject, Text text, int v)
    {

        Stopwatch sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < 1000; i++)
        {
            GameObject go = Instantiate(gameObject);
            go.name = i.ToString();

            switch (v)
            {
                case 0:
                    break;
                case 1:
                    go.AddComponent<BoxCollider>().enabled = false;
                    break;
                case 2:
                    go.AddComponent<BoxCollider>();
                    break;
            }

        }
        sw.Stop();
        UnityEngine.Debug.Log(string.Format("total: {0} ms", sw.ElapsedMilliseconds));

    }
}
