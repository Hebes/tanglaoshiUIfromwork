using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    private Vector3 dir;

    void Start()
    {
        EventCenter.GetInstance().AddEventListener<Vector2>("Joystick", CheckDirChange);
    }

    void Update()
    {
        this.transform.Translate(dir * Time.deltaTime, Space.World);
    }

    private void CheckDirChange(Vector2 dir)
    {
        this.dir.x = dir.x;
        this.dir.z = dir.y;
    }

}
