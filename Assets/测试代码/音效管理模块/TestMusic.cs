using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMusic : MonoBehaviour
{

    GUIStyle s;
    GUIStyle s1;
    float v = 1;

    AudioSource source;

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 100), "播放音乐"))
        {
            v = 0;
            MusicMgr.GetInstance().ChangeBKValue(v);
            MusicMgr.GetInstance().PlayBkMusic("Black");
        }
        if (GUI.Button(new Rect(0, 100, 100, 100), "暂停音乐"))
        {
            MusicMgr.GetInstance().PauseBKMusic();
        }
        if (GUI.Button(new Rect(0, 200, 100, 100), "停止音乐"))
        {
            MusicMgr.GetInstance().StopBKMusic();
        }

        v += Time.deltaTime / 100;
        MusicMgr.GetInstance().ChangeBKValue(v);


        if (GUI.Button(new Rect(100, 0,100,100), "播放音效"))
            MusicMgr.GetInstance().PlaySound("Black", false,(s)=>{
                source=s;
            });
        if (GUI.Button(new Rect(100, 100,100,100), "停止音效"))
            MusicMgr.GetInstance().StopSound(source);

    }
}
