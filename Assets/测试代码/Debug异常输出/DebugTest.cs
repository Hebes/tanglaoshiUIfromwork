using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTest : MonoBehaviour
{

    public GameObject gameObject1;
    void Start()
    {
        //Debug.Log("Log");
        //Debug.LogWarning("LogWarning");
        //Debug.LogError("LogError");
        gameObject1.name = "1";
    }
}
