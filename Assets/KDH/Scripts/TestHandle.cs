using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHandle : MonoBehaviour
{
    public DoorHandle_0 tDoor1;
    public DoorHandle_1 tDoor2;

    public bool dh1on;
    public bool dh2on;

    private void Update()
    {
        TestH();
    }

    void TestH()
    {
        dh1on = tDoor1.PlayerPos_0;
        dh2on = tDoor2.PlayerPos_1;
    }

}
