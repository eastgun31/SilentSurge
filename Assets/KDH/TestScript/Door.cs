using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour      // 문을 열고 닫는 스크립트
{
    public bool isOpen = false;

    public enum DoorState
    {
        not,
        up,
        down
    }

    public float openangle = 90f;
    public float smooth = 3f;

    public DoorHandle_1 tDoor2;

    public static DoorState op = DoorState.not;

    public bool PlayerPos_0 = false;
    public bool PlayerPos_1 = false;

    public int nDoor;                                   // 잠긴 문 강제 설정

    public void OpenDoor()
    {
        switch (op)
        {
            case DoorState.not:
                if (PlayerPos_0 && !PlayerPos_1)
                {
                    transform.localRotation = Quaternion.Euler(0, -openangle, 0);
                    op = DoorState.up;
                }
                else if (!PlayerPos_0 && PlayerPos_1)
                {
                    transform.localRotation = Quaternion.Euler(0, openangle, 0);
                    op = DoorState.down;
                }
                break;

            case DoorState.up:
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                    op = DoorState.not;
                break;

            case DoorState.down:
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                    op = DoorState.not;
                break;
        }
    }
}
