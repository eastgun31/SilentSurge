using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public float openangle = 90f;
    public float smooth = 3f;
    public int doorhandle;

    void Start()
    {
        
    }

    void Update()
    {
        if (isOpen == true && doorhandle == 0)
        {
            Quaternion targetRotation = Quaternion.Euler(0, -openangle, 0);
            transform.localRotation= Quaternion.Slerp(transform.localRotation, targetRotation, smooth*Time.deltaTime);
        }
        else if(isOpen == true && doorhandle != 0)
        {
            Quaternion targetRotation = Quaternion.Euler(0, +openangle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }
    }

    //public void DoorState()
    //{
    //    isOpen = !isOpen;
    //}
}
