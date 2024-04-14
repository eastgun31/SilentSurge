using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Parent : MonoBehaviour      // 문을 열고 닫는 스크립트
{
    public bool isOpen = false;

    private bool _init=false;

    [SerializeField] private GameObject[] doorHandle;

         
    public enum OpenDoor
    {
        not,
        up,
        down
    }

    public float openangle = 90f;
    public float smooth = 3f;

    public DoorHandle_0 tDoor1;
    public DoorHandle_1 tDoor2;

    public static OpenDoor op = OpenDoor.not;

    public bool PlayerPos_0 = false;
    public bool PlayerPos_1 = false;

    void Update()
    {
        //oDoor();
    }

    public void oDoor()
    {
        switch (op)
        {

            case OpenDoor.not:

                if (PlayerPos_0 && !PlayerPos_1 && Input.GetKeyDown(KeyCode.Space))
                {
                    transform.localRotation = Quaternion.Euler(0, -openangle, 0);
                    op = OpenDoor.up;
                }
                else if (!PlayerPos_0 && PlayerPos_1 && Input.GetKeyDown(KeyCode.Space))
                {
                    transform.localRotation = Quaternion.Euler(0, openangle, 0);
                    op = OpenDoor.down;
                }
                break;

            case OpenDoor.up:

                if (PlayerPos_0 && !PlayerPos_1 && Input.GetKeyDown(KeyCode.Space))
                {
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                    op = OpenDoor.not;
                }
                else if (!PlayerPos_0 && PlayerPos_1 && Input.GetKeyDown(KeyCode.Space))
                {
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                    op = OpenDoor.not;
                }
                break;
            case OpenDoor.down:
                if (PlayerPos_0 && !PlayerPos_1 && Input.GetKeyDown(KeyCode.Space))
                {
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                    op = OpenDoor.not;
                }
                else if (!PlayerPos_0 && PlayerPos_1 && Input.GetKeyDown(KeyCode.Space))
                {
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                    op = OpenDoor.not;
                }
                break;
        }
    }


}
