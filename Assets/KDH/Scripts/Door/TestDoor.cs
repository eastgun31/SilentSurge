using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDoor : MonoBehaviour
{
    public bool isOpen = false;


    enum OpenDoor
    {
        not,
        up,
        down
    }


    public float openangle = 90f;
    public float smooth = 3f;

    public TestDoorHandle_0 tDoor1;
    public TestDoorHandle_1 tDoor2;

    CapsuleCollider[] CC;

    OpenDoor op = OpenDoor.not;

    public bool PlayerPos_0 = false;
    public bool PlayerPos_1 = false;

    private void Awake()
    {
        CC = GetComponentsInChildren<CapsuleCollider>();
    }

    void Update()
    {
        Door();
    }

    public void Door()
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

    public void DoorState()
    {
        if (this.transform.rotation.y > 1f || this.transform.rotation.y < -1f)
            isOpen = true;
    }
}
