using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDoor : MonoBehaviour
{
    public bool isOpen = false;

    public float openangle = 90f;
    public float smooth = 3f;

    public bool canOpen = false;

    public GameObject handleA;
    public GameObject handleB;

    void Update()
    {
        
    }

    public void DoorOpen()
    {
        if (isOpen == true && canOpen == true)
        {
            Quaternion targetRotation = Quaternion.Euler(0, -openangle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }
        else if (isOpen == true && canOpen == true)
        {
            Quaternion targetRotation = Quaternion.Euler(0, +openangle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider handlea)
    {
        if(handlea.gameObject.CompareTag("Player"))
        {
            canOpen = true;
            Debug.Log("asdf");
        }
    }

}
