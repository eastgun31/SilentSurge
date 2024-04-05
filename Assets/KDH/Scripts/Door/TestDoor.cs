using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDoor : MonoBehaviour
{
    public bool isOpen = false;

    public float openangle = 90f;
    public float smooth = 3f;

    public TestDoorHandle tDoor1;
    public TestDoorHandle tDoor2;

    void Start()
    {
        
    }

    void Update()
    {
        DoorOpen();
    }

    public void DoorOpen()
    {
        if (isOpen == false && tDoor1.canOpen == true || tDoor2.canOpen == true)            // ���� ���� �ְ�, ������ �� �ϳ��� true�� ��
        {
            Quaternion targetRotation = Quaternion.Euler(0, -openangle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }
        else if (isOpen == true && tDoor1.canOpen == false || tDoor2.canOpen == false)      // ���� ���� �ְ�, ������ �� �ϳ��� false�� ��
        {
            Quaternion targetRotation = Quaternion.Euler(0, +openangle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }
    }

    public void DoorState()
    {
        if (this.transform.rotation.y > 1f || this.transform.rotation.y < -1f)
            isOpen = true;
    }

}
